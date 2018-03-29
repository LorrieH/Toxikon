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

    #region initialization

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

    #endregion

    #region Clicks

    private void TileClicked(Vector2 tilePosition)
    {
        m_ClickedTilePositions.Add(tilePosition);
        switch (CardSelector.s_Instance.SelectedCard.CardData.Type)
        {

            case CardTypes.PATH_CARD:
                TryPlacePath();
                break;
            case CardTypes.ROTATE_PATH_CARD:
                TryRotatePath(tilePosition);
                break;
            case CardTypes.MOVE_PATH_CARD:
                TryMovePath();
                break;
            case CardTypes.DESTROY_PATH_CARD:
                TryDestroyPath(tilePosition);
                break;
        }
    }

    private void ResetClicks()
    {
        m_ClickedTilePositions.Clear();
        HideIndicator();
    }

    #endregion

    #region Effect Calls

    private void TryPlacePath()
    {
        PathCardData pathData = CardSelector.s_Instance.SelectedCard.CardData.PathData;
        if (TileGrid.s_Instance.PlaceNewCard((int)m_ClickedTilePositions[0].x, (int)m_ClickedTilePositions[0].y, pathData.Up, pathData.Right, pathData.Down, pathData.Left, pathData.Middle))
            CardPositionHolder.s_Instance.DiscardCard(CardSelector.s_Instance.SelectedCard, true);
        else
            NotificationManager.s_Instance.EnqueueNotification("Cannot place a tile here!", 1.5f);

        m_ClickedTilePositions.Clear();
    }

    private void TryRotatePath(Vector2 tilePosition)
    {
        if (TileGrid.s_Instance.IsDestroyable((int)tilePosition.x, (int)tilePosition.y))
        {
            ActionFXManager.s_Instance.RotateTile(TileGrid.s_Instance.GetTileNode((int)tilePosition.x, (int)tilePosition.y), tilePosition);
            CardPositionHolder.s_Instance.DiscardCard(CardSelector.s_Instance.SelectedCard, false);
        }
    }

    private void TryMovePath()
    {
        switch (m_ClickedTilePositions.Count)
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
                        CardPositionHolder.s_Instance.DiscardCard(CardSelector.s_Instance.SelectedCard, false);
                }
                else
                {
                    ResetClicks();
                    NotificationManager.s_Instance.EnqueueNotification("Cannot move tile to this position", 1.5f);
                }
                HideIndicator();
                break;
        }
    }
    
    private void TryDestroyPath(Vector2 tilePosition)
    {
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
    }

    #endregion

    #region Indicator

    /// <summary>
    /// Shows an Indicator on the given position
    /// </summary>
    /// <param name="position">Position of the Indicator</param>
    public void ShowIndicator(Vector2 position)
    {
        ShowIndicator((int)position.x, (int)position.y);
    }

    /// <summary>
    /// Shows an Indicator on the given position
    /// </summary>
    /// <param name="x">X Position of the Indicator</param>
    /// <param name="y">Y Position of the Indicator</param>
    public void ShowIndicator(int x, int y)
    {
        DOTween.Kill("ShowIndicator", false);
        DOTween.Kill("MoveIndicator", false);
        m_Indicator.transform.localPosition = new Vector2(x, y + 0.2f);
        m_Indicator.transform.localScale = Vector2.zero;

        m_Indicator.transform.DOScale(1, 0.33f).SetEase(Ease.OutBack).SetId("ShowIndicator");
        m_Indicator.transform.DOLocalMoveY(y + 0.5f, 0.5f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).SetId("MoveIndicator");
    }

    /// <summary>
    /// Hides the Indicator
    /// </summary>
    public void HideIndicator()
    {
        m_Indicator.transform.DOScale(0, 0.33f).SetEase(Ease.InBack);
    }

    #endregion
}
