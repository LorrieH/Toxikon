using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileArtLib
{
    public static Sprite            s_EmptyTex;
    public static TileArtClass[]    s_TileArtArray;

    public static GameObject[]      s_Bridges;
}

[System.Serializable]
public class TileArtClass
{
    public TileBools    bools;
    public Sprite       TileArt;
}
