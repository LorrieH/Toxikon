using UnityEngine;

public class SetTileArt : MonoBehaviour
{
    [SerializeField] private Sprite m_EmptyTex;
    [SerializeField] private TileArtClass[] m_TileArt;
    [SerializeField] private GameObject[] m_Bridges;
    
    void Awake()
    {
        TileArtLib.s_EmptyTex = m_EmptyTex;
        TileArtLib.s_TileArtArray = m_TileArt;

        TileArtLib.s_Bridges = m_Bridges;
    }
}