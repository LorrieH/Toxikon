using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClickManager : MonoBehaviour
{
    public static TileClickManager s_Instance;

    private List<Vector2> m_ClickedTilePositions = new List<Vector2>();
    public List<Vector2> ClickedTilePositions { get { return m_ClickedTilePositions; } set { m_ClickedTilePositions = value; } }

    private void Awake()
    {
        Init();
        ClickedOnTile.s_OnTileClicked += TileClicked;
        TurnManager.s_OnTurnStart += ResetClicks;
    }

    private void OnDestroy()
    {
        ClickedOnTile.s_OnTileClicked -= TileClicked;
        TurnManager.s_OnTurnStart -= ResetClicks;
    }

    private void Init()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(gameObject);
    }

    private void ResetClicks()
    {
        m_ClickedTilePositions.Clear();
    }

    private void TileClicked(Vector2 tilePosition)
    {
        m_ClickedTilePositions.Add(tilePosition);
        switch (CardSelector.s_Instance.SelectedCard.CardData.Type)
        {

            case CardTypes.PATH_CARD:
                PlacePath();
                break;
            case CardTypes.ROTATE_PATH_CARD:

                break;
            case CardTypes.MOVE_PATH_CARD:

                break;
            case CardTypes.DESTROY_PATH_CARD:
                ActionFXManager.s_Instance.BreakTile((int)tilePosition.x, (int)tilePosition.y);
                CardPositionHolder.s_OnDiscardCard(CardSelector.s_Instance.SelectedCard);
                break;
        }
    }

    private void PlacePath()
    {
        PathCardData pathData = CardSelector.s_Instance.SelectedCard.CardData.PathData;
        if (TileGrid.s_Instance.PlaceNewCard((int)m_ClickedTilePositions[0].x, (int)m_ClickedTilePositions[0].y, pathData.Up, pathData.Right, pathData.Down, pathData.Left, pathData.Middle))
        {
            if (TileGrid.s_Instance.CompleteRoad(TurnManager.s_Instance.CurrentPlayerIndex))
            {
                Debug.Log("a road for " +TurnManager.s_Instance.CurrentPlayerIndex);
                TurnManager.s_OnGameEnd(TurnManager.s_Instance.CurrentPlayer);
            }
            else
            {
                Debug.Log("no road for" + TurnManager.s_Instance.CurrentPlayerIndex);
            }
            CardPositionHolder.s_OnDiscardCard(CardSelector.s_Instance.SelectedCard);
        }
        else
        {
            NotificationManager.s_Instance.EnqueueNotification("Cannot place a tile here!", 1.5f);
        }
        m_ClickedTilePositions.Clear();
    }
}
