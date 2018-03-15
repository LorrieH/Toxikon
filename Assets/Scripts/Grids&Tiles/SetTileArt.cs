using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileArt : MonoBehaviour {
    [SerializeField] private Sprite m_testTex;
    [SerializeField] private TileArtClass[] m_TileArt;
    
    void Start()
    {
        TileArtLib.s_TestTex = m_testTex;
        TileArtLib.s_TileArtArray = m_TileArt;
    }
}