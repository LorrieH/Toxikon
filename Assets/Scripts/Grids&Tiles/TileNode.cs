using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode
{
    #region variables
    //path openings
    public TileBools Bools      { get; set; }

    public bool IsFilled        { get; set; }
    public bool CanBeFilled     { get; set; }
    public bool IsDestructable  { get; set; }
    public bool IsEdgeStone     { get; set; }
    public bool IsChecked       { get; set; }
    public bool IsEndpoint      { get; set; }
    public bool IsStartPoint    { get; set; }
    public bool IsInRoad        { get; set; }

    public bool DebugModus      { get; set; }


    //rotation with increments op 90 degrees
    public byte TurnsRight      { get; set; }

    public Vector2 GridPos      { get; set; }

    public Sprite TileTexture       { get; set; }
    public GameObject TileObject    { get; set; }

    public TileNode RoadParent              { get; set; }
    public Neighbours AllNeighbours         { get; set; }
    public Neighbours AccesableNeighbours   { get; set; }
    #endregion

    #region functions
    /// <summary>
    /// This function fils the AccesableNeighbours variable.
    /// </summary>
    public void SetAccesableNeighbours()
    {
        Neighbours tempNeighbour = new Neighbours();
        if (Bools.Up)
        {
            if (AllNeighbours.up.IsFilled && AllNeighbours.up.Bools.Down && !AllNeighbours.up.IsEdgeStone)
            {
                tempNeighbour.up = AllNeighbours.up;
            }
        }
        if (Bools.Right)
        {
            if (AllNeighbours.right.IsFilled && AllNeighbours.right.Bools.Left && !AllNeighbours.right.IsEdgeStone)
            {
                tempNeighbour.right = AllNeighbours.right;
            }
        }
        if (Bools.Down)
        {
            if (AllNeighbours.down.IsFilled && AllNeighbours.down.Bools.Up && !AllNeighbours.down.IsEdgeStone)
            {
                tempNeighbour.down = AllNeighbours.down;
            }
        }
        if (Bools.Left)
        {
            if (AllNeighbours.left.IsFilled && AllNeighbours.left.Bools.Right && !AllNeighbours.left.IsEdgeStone)
            {
                tempNeighbour.left = AllNeighbours.left;
            }
        }
        AccesableNeighbours = tempNeighbour;
    }

    public void UpdateArt()
    {
        if (!IsEdgeStone)
        {
            if (IsFilled)
            {
                for (int i = 0; i < TileArtLib.s_TileArtArray.Length; i++)
                {
                    if (TileArtLib.s_TileArtArray[i].bools == Bools)
                    {
                        ///Debug.Log("set a texture");
                        TileTexture = TileArtLib.s_TileArtArray[i].TileArt;
                        TileObject.GetComponent<SpriteRenderer>().sprite = TileTexture;
                        break;
                    }
                }
            }
            else
            {
                TileObject.GetComponent<SpriteRenderer>().sprite = TileArtLib.s_EmptyTex;
            }
        }
        if (DebugModus)
        {
            if (IsEndpoint)
            {
                TileObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (IsStartPoint)
            {
                TileObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (IsInRoad)
            {
                TileObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (IsChecked)
            {
                TileObject.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
    }

    public void AddToRoad(TileGrid T)
    {
        if(!IsStartPoint)
        {
            IsInRoad = true;
            T.NodeRoad.Add(RoadParent.TileObject.transform.position);
            RoadParent.AddToRoad(T);
            UpdateArt();
        }
    }

    public void GetChecked(TileGrid T, TileNode parent)
    {
        SetAccesableNeighbours();
        RoadParent = parent;
        IsChecked = true;
        T.CheckedNodes.Add(this);
        if (!T.RoadCompleted)
        {
            if (IsEndpoint)
            {
                T.NodeRoad.Add(this.TileObject.transform.position);
                IsInRoad = true;
                AddToRoad(T);
                T.RoadCompleted = true;
            }
            else if(!T.RoadCompleted)
            {
                if (AccesableNeighbours.up != null && !AccesableNeighbours.up.IsChecked && AccesableNeighbours.up.Bools.Middle || AccesableNeighbours.up != null && !AccesableNeighbours.up.IsChecked && AccesableNeighbours.up.IsEndpoint)
                {
                    AccesableNeighbours.up.GetChecked(T, this);
                }
                if (AccesableNeighbours.right != null && !AccesableNeighbours.right.IsChecked && AccesableNeighbours.right.Bools.Middle || AccesableNeighbours.right != null && !AccesableNeighbours.right.IsChecked && AccesableNeighbours.right.IsEndpoint)
                {
                    AccesableNeighbours.right.GetChecked(T, this);
                }
                if (AccesableNeighbours.down != null && !AccesableNeighbours.down.IsChecked && AccesableNeighbours.down.Bools.Middle || AccesableNeighbours.down != null && !AccesableNeighbours.down.IsChecked && AccesableNeighbours.down.IsEndpoint)
                {
                    AccesableNeighbours.down.GetChecked(T, this);
                }
                if (AccesableNeighbours.left != null && !AccesableNeighbours.left.IsChecked && AccesableNeighbours.left.Bools.Middle || AccesableNeighbours.left != null && !AccesableNeighbours.left.IsChecked && AccesableNeighbours.left.IsEndpoint)
                {
                    AccesableNeighbours.left.GetChecked(T, this);
                }
            }
        }
        UpdateArt();
    }
    #endregion

    #region return functions
    /// <summary>
    /// this returns a true or false according to if the tile will connect to its neighbours
    /// </summary>
    /// <returns></returns>
    public bool CheckPlacement(bool CardUp, bool CardRight, bool CardDown, bool CardLeft, bool ignoreConnect, bool ignoreAll)
    {
        bool canConnectUp = false;
        bool canConnectRight = false;
        bool canConnectDown = false;
        bool canConnectLeft = false;
        bool hasFilledNeighbour = false;

        //is up compatible
        if (!AllNeighbours.up.IsFilled)
        {
            canConnectUp = true;
        }
        else if (CardUp && AllNeighbours.up.Bools.Down)
        {
            canConnectUp = true;
            hasFilledNeighbour = true;
        }
        else if (!CardUp && !AllNeighbours.up.Bools.Down)
        {
            canConnectUp = true;
        }

        //is right compatible
        if (!AllNeighbours.right.IsFilled)
        {
            canConnectRight = true;
        }
        else if (CardRight && AllNeighbours.right.Bools.Left)
        {
            canConnectRight = true;
            hasFilledNeighbour = true;
        }
        else if (!CardRight && !AllNeighbours.right.Bools.Left)
        {
            canConnectRight = true;
        }

        //is down compatible
        if (!AllNeighbours.down.IsFilled)
        {
            canConnectDown = true;
        }
        else if (CardDown && AllNeighbours.down.Bools.Up)
        {
            canConnectDown = true;
            hasFilledNeighbour = true;
        }
        else if (!CardDown && !AllNeighbours.down.Bools.Up)
        {
            canConnectDown = true;
        }

        //is left compatible
        if (!AllNeighbours.left.IsFilled)
        {
            canConnectLeft = true;
        }
        else if (CardLeft && AllNeighbours.left.Bools.Right)
        {
            canConnectLeft = true;
            hasFilledNeighbour = true;
        }
        else if (!CardLeft && !AllNeighbours.left.Bools.Right)
        {
            canConnectLeft = true;
        }
        if (ignoreConnect)
        {
            hasFilledNeighbour = true;
        }
        if (ignoreAll)
        {
            return true;
        }
        else
        {
            return (canConnectUp && canConnectRight && canConnectDown && canConnectLeft && hasFilledNeighbour);
        }
    }
    #endregion
}
