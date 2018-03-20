using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode
{
    #region variables
    //path openings
    public TileBools Bools { get; set; }

    public bool IsFilled { get; set; }
    public bool CanBeFilled { get; set; }
    public bool IsDestructable { get; set; }
    public bool IsEdgeStone { get; set; }
    public bool IsChecked { get; set; }
    public bool IsEndpoint { get; set; }
    public bool IsStartPoint { get; set; }


    //rotation with increments op 90 degrees
    public byte TurnsRight { get; set; }

    public Vector2 GridPos { get; set; }

    public Sprite TileTexture { get; set; }
    public GameObject TileObject { get; set; }

    public TileNode RoadParent { get; set; }
    public Neighbours AllNeighbours { get; set; }
    public Neighbours AccesableNeighbours { get; set; }
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
                //....

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
            if (AllNeighbours.left.IsFilled && AllNeighbours.left.Bools.Right && !AllNeighbours.right.IsEdgeStone)
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
                        Debug.Log("set a texture");
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
        if (IsEndpoint)
        {
            TileObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (IsStartPoint)
        {
            TileObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (IsChecked)
        {
            TileObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
    #endregion

    #region return functions
    /// <summary>
    /// this returns a true or false according to if the tile will connect to its neighbours
    /// </summary>
    /// <returns></returns>
    public bool CheckPlacement(bool CardUp, bool CardRight, bool CardDown, bool CardLeft)
    {
        bool canConnectUp = false;
        bool canConnectRight = false;
        bool canConnectDown = false;
        bool canConnectLeft = false;

        //is up compatible
        if (!AllNeighbours.up.IsFilled)
        {
            canConnectUp = true;
        }
        else if (CardUp && AllNeighbours.up.Bools.Down)
        {
            canConnectUp = true;
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
        }
        else if (!CardLeft && !AllNeighbours.left.Bools.Right)
        {
            canConnectLeft = true;
        }
        return (canConnectUp && canConnectRight && canConnectDown && canConnectLeft);
    }
    #endregion
}

public struct Neighbours
{
    public TileNode up;
    public TileNode right;
    public TileNode down;
    public TileNode left;
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode
{
    #region variables
    //path openings
    public TileBools Bools { get; set; }

    public bool IsFilled        { get; set; }
    public bool CanBeFilled     { get; set; }
    public bool IsDestructable  { get; set; }
    public bool IsEdgeStone     { get; set; }
    public bool IsChecked       { get; set; }

    //rotation with increments op 90 degrees
    public byte TurnsRight  { get; set; }

    public Vector2 GridPos  { get; set; }

    public Sprite TileTexture { get; set; }
    public GameObject TileObject { get; set; }

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
        Neighbours tempNeighbour;
        tempNeighbour.up = null;
        tempNeighbour.right = null;
        tempNeighbour.down = null;
        tempNeighbour.left = null;
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
            if (AllNeighbours.left.IsFilled && AllNeighbours.left.Bools.Right && !AllNeighbours.right.IsEdgeStone)
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
            for (int i = 0; i < TileArtLib.s_TileArtArray.Length; i++)
            {
                if(TileArtLib.s_TileArtArray[i].bools == Bools)
                {
                    Debug.Log("set a texture");
                    TileTexture = TileArtLib.s_TileArtArray[i].TileArt;
                    SpriteRenderer tileRenderer = TileObject.GetComponent<SpriteRenderer>();
                    tileRenderer.sprite = TileTexture;
                    break;
                }
            }
        }
    }
    #endregion

    #region return functions
    /// <summary>
    /// this returns a true or false according to if the tile will connect to its neighbours
    /// </summary>
    /// <returns></returns>
    public bool CheckPlacement(bool CardUp, bool CardRight, bool CardDown, bool CardLeft)
    {
        bool canConnectUp = false;
        bool canConnectRight = false;
        bool canConnectDown = false;
        bool canConnectLeft = false;

        //is up compatible
        if (!AllNeighbours.up.IsFilled)
        {
            canConnectUp = true;
        }
        else if (CardUp && AllNeighbours.up.Bools.Down)
        {
            canConnectUp = true;
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
        }
        else if (!CardLeft && !AllNeighbours.left.Bools.Right)
        {
            canConnectLeft = true;
        }
        return (canConnectUp && canConnectRight && canConnectDown && canConnectLeft);
    }
    #endregion
}

public struct Neighbours
{
    public TileNode up;
    public TileNode right;
    public TileNode down;
    public TileNode left;
}*/
