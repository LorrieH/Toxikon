using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode
{

    #region variables
    //path openings
    public bool Up      { get; set; }
    public bool Down    { get; set; }
    public bool Left    { get; set; }
    public bool Right   { get; set; }
    public bool Middle  { get; set; }

    public bool IsFilled        { get; set; }
    public bool CanBeFilled     { get; set; }
    public bool IsDestructable  { get; set; }
    public bool IsEdgeStone     { get; set; }
    public bool IsChecked       { get; set; }

    //rotation with increments op 90 degrees
    public byte TurnsRight  { get; set; }

    public Vector2 GridPos  { get; set; }

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
        if (Up)
        {
            if (AllNeighbours.up.IsFilled && AllNeighbours.up.Down && !AllNeighbours.up.IsEdgeStone)
            {
                tempNeighbour.up = AllNeighbours.up;
            }
        }
        if (Right)
        {
            if (AllNeighbours.right.IsFilled && AllNeighbours.right.Left && !AllNeighbours.right.IsEdgeStone)
            {
                tempNeighbour.right = AllNeighbours.right;
            }
        }
        if (Down)
        {
            if (AllNeighbours.down.IsFilled && AllNeighbours.down.Up && !AllNeighbours.down.IsEdgeStone)
            {
                tempNeighbour.down = AllNeighbours.down;
            }
        }
        if (Left)
        {
            if (AllNeighbours.left.IsFilled && AllNeighbours.left.Right && !AllNeighbours.right.IsEdgeStone)
            {
                tempNeighbour.left = AllNeighbours.left;
            }
        }
        AccesableNeighbours = tempNeighbour;
    }

    public void SetNeighbours(TileNode up, TileNode right, TileNode down, TileNode left)
    {
        Neighbours tempNeighbour;
        tempNeighbour.up = up;
        tempNeighbour.right = right;
        tempNeighbour.down = down;
        tempNeighbour.left = left;

        AllNeighbours = tempNeighbour;
    }
    #endregion

    #region return functions
    /// <summary>
    /// this returns a true or false according to if the tile will connect to its neighbours
    /// </summary>
    /// <returns></returns>
    public bool CheckPlacement(TileNode CardValues)
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
        else if (CardValues.Up && AllNeighbours.up.Down)
        {
            canConnectUp = true;
        }
        else if (!CardValues.Up && !AllNeighbours.up.Down)
        {
            canConnectUp = true;
        }

        //is right compatible
        if (!AllNeighbours.right.IsFilled)
        {
            canConnectRight = true;
        }
        else if (CardValues.Right && AllNeighbours.right.Left)
        {
            canConnectRight = true;
        }
        else if (!CardValues.Right && !AllNeighbours.right.Left)
        {
            canConnectRight = true;
        }

        //is down compatible
        if (!AllNeighbours.down.IsFilled)
        {
            canConnectDown = true;
        }
        else if (CardValues.Down && AllNeighbours.down.Up)
        {
            canConnectDown = true;
        }
        else if (!CardValues.Down && !AllNeighbours.down.Up)
        {
            canConnectDown = true;
        }

        //is left compatible
        if (!AllNeighbours.left.IsFilled)
        {
            canConnectLeft = true;
        }
        else if (CardValues.Left && AllNeighbours.left.Right)
        {
            canConnectLeft = true;
        }
        else if (!CardValues.Left && !AllNeighbours.left.Right)
        {
            canConnectLeft = true;
        }

        return (canConnectUp == canConnectRight == canConnectDown == canConnectLeft == true);
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