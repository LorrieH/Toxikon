using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickedOnTile : MonoBehaviour {

    public TileGrid Grid { get; set; }
    public TestPlaceCard TestPlacer;

    public int m_TilePosX { get; set; }
    public int m_TilePosY { get; set; }

    // Use this for initialization
    void Start () {
        TestPlacer = GameObject.Find("testCard").GetComponent<TestPlaceCard>();
	}

    void OnMouseDown()
    {
        switch (CardSelector.s_Instance.SelectedCard.Type) {

            case CardTypes.PATH_CARD:
                PathCardData pathData = CardSelector.s_Instance.SelectedCard.PathData;
                Grid.PlaceNewCard(m_TilePosX, m_TilePosY, pathData.Up, pathData.Right, pathData.Down, pathData.Left, pathData.Middle);
                break;
            case CardTypes.ROTATE_PATH_CARD:

                break;
            case CardTypes.SWAP_PATH_CARD:
                break;
        }
        /*(if(Grid.PlaceNewCard(m_TilePosX, m_TilePosY, TestPlacer.Up, TestPlacer.Right, TestPlacer.Down, TestPlacer.Left, TestPlacer.Middle))
        {
            Debug.Log("placed");
        }
        else
        {
            Debug.Log("cant be placed here");
        }*/
    }
}
