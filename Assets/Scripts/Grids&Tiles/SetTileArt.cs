using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileArt : MonoBehaviour {
    [SerializeField] private Sprite m_EmptyTex;
    [SerializeField] private TileArtClass[] m_TileArt;

    [SerializeField] private GameObject[] m_Bridges;

    /*
    //single openings leading nowhere
    [SerializeField] private Texture m_Up;
    [SerializeField] private Texture m_Right;
    [SerializeField] private Texture m_Down;
    [SerializeField] private Texture m_Left;

    //corners with middle part open
    [SerializeField]private Texture m_CornerRightUp;
    [SerializeField]private Texture m_CornerleftUp;
    [SerializeField]private Texture m_CornerRightDown;
    [SerializeField]private Texture m_CornerLeftDown;

    //corners with midle part closed
    [SerializeField]private Texture m_CornerRightUpClosed;
    [SerializeField]private Texture m_CornerleftUpClosed;
    [SerializeField]private Texture m_CornerRightDownClosed;
    [SerializeField]private Texture m_CornerLeftDownClosed;

    //crossroad
    [SerializeField]private Texture m_Crossroad;

    //crossroad with the middle closed
    [SerializeField]private Texture m_CrossroadClosed;

    //threeways
    [SerializeField]private Texture m_RightUpLeft;
    [SerializeField]private Texture m_RightDownLeft;
    [SerializeField]private Texture m_UpLeftDown;
    [SerializeField]private Texture m_UpRightDown;

    //threeways with the middle closed
    [SerializeField]private Texture m_RightUpLeftClosed;
    [SerializeField]private Texture m_RightDownLeftClosed;
    [SerializeField]private Texture m_UpLeftDownClosed;
    [SerializeField]private Texture m_UpRightDownClosed;

    //straightLine
    [SerializeField]private Texture m_RightLeft;
    [SerializeField]private Texture m_UpDown;

    //straight lines with the middle closed
    [SerializeField]private Texture m_RightLeftClosed;
    [SerializeField]private Texture m_UpDownClosed;

    //Just a filled tile
    [SerializeField]private Texture m_Filled;

    //an empty Tile
    [SerializeField]private Texture m_Empty;
    */

    // Use this for initialization
    void Awake()
    {
        TileArtLib.s_EmptyTex = m_EmptyTex;
        TileArtLib.s_TileArtArray = m_TileArt;

        TileArtLib.s_Bridges = m_Bridges;
        /*
        //single openings leading nowhere
        TileArtLib.s_Up     = m_Up;
        TileArtLib.s_Right  = m_Right;
        TileArtLib.s_Down   = m_Down;
        TileArtLib.s_Left   = m_Left;

        //corners with middle part open
        TileArtLib.s_CornerRightUp      = m_CornerRightUp;
        TileArtLib.s_CornerleftUp       = m_CornerleftUp;
        TileArtLib.s_CornerRightDown    = m_CornerRightDown;
        TileArtLib.s_CornerLeftDown     = m_CornerLeftDown;

        //corners with midle part closed
        TileArtLib.s_CornerRightUpClosed    = m_CornerRightUpClosed;
        TileArtLib.s_CornerleftUpClosed     = m_CornerleftUpClosed;
        TileArtLib.s_CornerRightDownClosed  = m_CornerRightDownClosed;
        TileArtLib.s_CornerLeftDownClosed   = m_CornerLeftDownClosed;

        //crossroad
        TileArtLib.s_Crossroad = m_Crossroad;

        //crossroad with the middle closed
        TileArtLib.s_CrossroadClosed = m_CrossroadClosed;

        //threeways
        TileArtLib.s_RightUpLeft    = m_RightUpLeft;
        TileArtLib.s_RightDownLeft  = m_RightDownLeft;
        TileArtLib.s_UpLeftDown     = m_UpLeftDown;
        TileArtLib.s_UpRightDown    = m_UpRightDown;

        //threeways with the middle closed
        TileArtLib.s_RightUpLeftClosed      = m_CornerRightUpClosed;
        TileArtLib.s_RightDownLeftClosed    = m_RightDownLeftClosed;
        TileArtLib.s_UpLeftDownClosed       = m_UpLeftDownClosed;
        TileArtLib.s_UpRightDownClosed      = m_UpRightDownClosed;

        //straightLine
        TileArtLib.s_RightLeft  = m_RightLeft;
        TileArtLib.s_UpDown     = m_UpDown;

        //straight lines with the middle closed
        TileArtLib.s_RightLeftClosed    = m_RightDownLeftClosed;
        TileArtLib.s_UpDownClosed       = m_UpDownClosed;

        //Just a filled tile
        TileArtLib.s_Filled = m_Filled;

        //an empty Tile
        TileArtLib.s_Empty = m_Empty;
        */
    }
}
