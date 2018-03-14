using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileArtLib
{
    public static Sprite s_TestTex;
    public static TileArtClass[] s_TileArtArray;
    /*
    //corners with middle part open
    public static Texture s_CornerRightUp;
    public static Texture s_CornerleftUp;
    public static Texture s_CornerRightDown;
    public static Texture s_CornerLeftDown;

    //corners with midle part closed
    public static Texture s_CornerRightUpClosed;
    public static Texture s_CornerleftUpClosed;
    public static Texture s_CornerRightDownClosed;
    public static Texture s_CornerLeftDownClosed;

    //crossroad
    public static Texture s_Crossroad;

    //crossroad with the middle closed
    public static Texture s_CrossroadClosed;

    //threeways
    public static Texture s_RightUpLeft;
    public static Texture s_RightDownLeft;
    public static Texture s_UpLeftDown;
    public static Texture s_UpRightDown;

    //threeways with the middle closed
    public static Texture s_RightUpLeftClosed;
    public static Texture s_RightDownLeftClosed;
    public static Texture s_UpLeftDownClosed;
    public static Texture s_UpRightDownClosed;

    //straightLine
    public static Texture s_RightLeft;
    public static Texture s_UpDown;

    //straight lines with the middle closed
    public static Texture s_RightLeftClosed;
    public static Texture s_UpDownClosed;

    //single openings leading nowhere
    public static Texture s_Up;
    public static Texture s_Right;
    public static Texture s_Down;
    public static Texture s_Left;

    //Just a filled tile
    public static Texture s_Filled;

    //an empty Tile
    public static Texture s_Empty;
    */
}

[System.Serializable]
public class TileArtClass
{
    public TileBools bools;
    public Sprite TileArt;
}

[System.Serializable]
public struct TileBools
{
    public bool Up;
    public bool Right;
    public bool Down;
    public bool Left;
    public bool Middle;

    public static bool operator ==(TileBools c1, TileBools c2)
    {
        return c1.Equals(c2);
    }

    public static bool operator !=(TileBools c1, TileBools c2)
    {
        return !c1.Equals(c2);
    }
}