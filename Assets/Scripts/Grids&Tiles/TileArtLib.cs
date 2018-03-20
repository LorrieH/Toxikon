using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileArtLib
{
    public static Sprite s_TestTex;
    public static TileArtClass[] s_TileArtArray;
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