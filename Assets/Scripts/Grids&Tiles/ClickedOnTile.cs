using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickedOnTile : MonoBehaviour
{

    public int TilePosX { get; set; }
    public int TilePosY { get; set; }

    void OnMouseDown()
    {
        if (CardSelector.s_Instance.SelectedCard == null) return;

        switch (CardSelector.s_Instance.SelectedCard.CardData.Type) {

            case CardTypes.PATH_CARD:
                PathCardData pathData = CardSelector.s_Instance.SelectedCard.CardData.PathData;
                if (TileGrid.s_Instance.PlaceNewCard(TilePosX, TilePosY, pathData.Up, pathData.Right, pathData.Down, pathData.Left, pathData.Middle))
                {
                    if (TileGrid.s_Instance.CompleteRoad(3))
                    {
                        Debug.Log("i found da wea");
                    }
                    else
                    {
                        Debug.Log("no wea");
                    }
                    CardPositionHolder.s_OnDiscardCard(CardSelector.s_Instance.SelectedCard);                    
                }
                break;
            case CardTypes.ROTATE_PATH_CARD:

                break;
            case CardTypes.SWAP_PATH_CARD:
                break;
            case CardTypes.DESTROY_CARD:
                if(TileGrid.s_Instance.DestroyNode(TilePosX, TilePosY))
                {
                    Debug.Log("Succesfully destroyed node");
                    if(TileGrid.s_Instance.CompleteRoad(3))
                    {
                        Debug.Log("i found da wea");
                    }
                    else
                    {
                        Debug.Log("no wea");
                    }
                    CardPositionHolder.s_OnDiscardCard(CardSelector.s_Instance.SelectedCard);
                }
                break;
        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickedOnTile : MonoBehaviour
{

    //click

    public TileGrid Grid { get; set; }
    public TestPlaceCard TestPlacer;

    public int m_TilePosX { get; set; }
    public int m_TilePosY { get; set; }

    // Use this for initialization
    void Start()
    {
        TestPlacer = GameObject.Find("testCard").GetComponent<TestPlaceCard>();
    }

    void OnMouseDown()
    {
        if (TestPlacer.SwapCards)
        {
            TestPlacer.selectedCards++;
            if (TestPlacer.selectedCards == 1)
            {
                Debug.Log("set one");
                TestPlacer.selectOne.x = m_TilePosX;
                TestPlacer.selectOne.y = m_TilePosY;
                TestPlacer.selectedCards++;
            }
            else if (TestPlacer.selectedCards >= 2)
            {
                Debug.Log("set two");
                TestPlacer.selectTwo.x = m_TilePosX;
                TestPlacer.selectTwo.y = m_TilePosY;
                if (Grid.MoveNode((int)TestPlacer.selectOne.x, (int)TestPlacer.selectOne.y, (int)TestPlacer.selectTwo.x, (int)TestPlacer.selectTwo.y))
                {
                    Debug.Log("swip swap");
                }
                else
                {
                    Debug.Log("no swap");
                }

                TestPlacer.selectedCards = 0;
            }
        }
        else if (TestPlacer.Destroy)
        {
            if (Grid.DestroyNode(m_TilePosX, m_TilePosY))
            {
                Debug.Log("Destroy D");
            }
            else
            {
                Debug.Log("YOU HAVE NO POWER HERE");
            }
        }
        else
        {
            if (Grid.PlaceNewCard(m_TilePosX, m_TilePosY, TestPlacer.Up, TestPlacer.Right, TestPlacer.Down, TestPlacer.Left, TestPlacer.Middle))
            {
                Debug.Log("placed");
                if (Grid.CompleteRoad(1))
                {
                    Debug.Log("there is a road");
                }
                else
                {
                    Debug.Log("there is no road");
                }
            }
            else
            {
                Debug.Log("cant be placed here");
            }
        }
    }
}
*/

