using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileArt : MonoBehaviour
{
    [SerializeField] private Sprite m_EmptyTex;
    [SerializeField] private TileArtClass[] m_TileArt;

    [SerializeField] private GameObject[] m_Bridges;

    // Use this for initialization
    void Awake()
    {
        TileArtLib.s_EmptyTex = m_EmptyTex;
        TileArtLib.s_TileArtArray = m_TileArt;

        TileArtLib.s_Bridges = m_Bridges;
    }
}