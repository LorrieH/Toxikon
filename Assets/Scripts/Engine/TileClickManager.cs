using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TileClickManager : MonoBehaviour
{
    public static TileClickManager s_Instance;

    [SerializeField] private SpriteRenderer m_Indicator;

    private List<Vector2> m_ClickedTilePositions = new List<Vector2>();
    public List<Vector2> ClickedTilePositions { get { return m_ClickedTilePositions; } set { m_ClickedTilePositions = value; } }

    private void Awake()
    {
        Init();
        ClickedOnTile.s_OnTileClicked += TileClicked;
        TurnManager.s_OnTurnStart += ResetClicks;
        CardSelector.s_OnSelectCard += ResetClicks;
    }

    private void OnDestroy()
    {
        ClickedOnTile.s_OnTileClicked -= TileClicked;
        TurnManager.s_OnTurnStart -= ResetClicks;
        CardSelector.s_OnSelectCard -= ResetClicks;
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
        HideIndicator();
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
                if (TileGrid.s_Instance.IsDestroyable((int)tilePosition.x, (int)tilePosition.y))
                {
                    ActionFXManager.s_Instance.RotateTile(TileGrid.s_Instance.GetTileNode((int)tilePosition.x, (int)tilePosition.y), tilePosition);
                    CardPositionHolder.s_Instance.DiscardCard(CardSelector.s_Instance.SelectedCard, false);
                }
                break;
            case CardTypes.MOVE_PATH_CARD:
                switch(m_ClickedTilePositions.Count)
                {
                    case 1:
                        ShowIndicator((int)m_ClickedTilePositions[0].x, (int)m_ClickedTilePositions[0].y);
                        NotificationManager.s_Instance.EnqueueNotification("Select position to move tile to", 1.5f);
                        break;
                    case 2:
                        if (TileGrid.s_Instance.CanMoveNode((int)m_ClickedTilePositions[0].x, (int)m_ClickedTilePositions[0].y, (int)m_ClickedTilePositions[1].x, (int)m_ClickedTilePositions[1].y))
                        {
                            ActionFXManager.s_Instance.MoveTile((int)m_ClickedTilePositions[0].x, (int)m_ClickedTilePositions[0].y, (int)m_ClickedTilePositions[1].x, (int)m_ClickedTilePositions[1].y);
                            
                            if (TileGrid.s_Instance.WinningPlayerData == null)
                            {
                                CardPositionHolder.s_Instance.DiscardCard(CardSelector.s_Instance.SelectedCard, false);
                            }
                        }
                        else
                        {
                            ResetClicks();
                            NotificationManager.s_Instance.EnqueueNotification("Cannot move tile to this position", 1.5f);
                        }
                        HideIndicator();
                        break;
                }
                break;
            case CardTypes.DESTROY_PATH_CARD:
                if (TileGrid.s_Instance.IsDestroyable((int)tilePosition.x, (int)tilePosition.y))
                {
                    ShowIndicator(tilePosition);
                    ActionFXManager.s_Instance.BreakTile((int)tilePosition.x, (int)tilePosition.y);
                    CardPositionHolder.s_Instance.DiscardCard(CardSelector.s_Instance.SelectedCard, false);
                }
                else
                {
                    NotificationManager.s_Instance.EnqueueNotification("Cannot destroy this tile", 1.5f);
                }
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
                //TurnManager.s_OnGameEnd(TurnManager.s_Instance.CurrentPlayer);
            }
            else
            {

            }
            CardPositionHolder.s_Instance.DiscardCard(CardSelector.s_Instance.SelectedCard, true);
        }
        else
        {
            NotificationManager.s_Instance.EnqueueNotification("Cannot place a tile here!", 1.5f);
        }
        m_ClickedTilePositions.Clear();
    }

    public void ShowIndicator(Vector2 position)
    {
        ShowIndicator((int)position.x, (int)position.y);
    }

    public void ShowIndicator(int x, int y)
    {
        DOTween.Kill("ShowIndicator", false);
        DOTween.Kill("MoveIndicator", false);
        m_Indicator.transform.localPosition = new Vector2(x, y + 0.2f);
        m_Indicator.transform.localScale = Vector2.zero;

        m_Indicator.transform.DOScale(1, 0.33f).SetEase(Ease.OutBack).SetId("ShowIndicator");
        m_Indicator.transform.DOLocalMoveY(y + 0.5f, 0.5f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).SetId("MoveIndicator");
    }

    public void HideIndicator()
    {
        //m_Indicator.transform.localScale = Vector2.one;
        m_Indicator.transform.DOScale(0, 0.33f).SetEase(Ease.InBack);
    }
}
