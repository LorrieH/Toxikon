﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CardPositionHolder : MonoBehaviour {

    public delegate void DiscardCardEvent(Card card, bool endTurn);
    public delegate void DrawCardEvent();
    public static DiscardCardEvent s_OnDiscardCard;
    public static DrawCardEvent s_OnDrawCard;

    public static CardPositionHolder s_Instance;

    [SerializeField] private List<Transform> m_CardPositions;
    [SerializeField]private List<Vector2> m_CardDefaultPositions;
    [SerializeField] private Transform m_CardDeckPosition;
    [SerializeField] private Transform m_ShowDrawnCardPosition;
    [SerializeField] private Transform m_OutOfScreenPosition;

    public List<Vector2> CardDefaultPositions { get { return m_CardDefaultPositions; } }

    private Card m_SelectedCard;
    private int m_IndexInHandPosition;

    private void OnEnable()
    {
        s_OnDiscardCard += DiscardCard;
        ActionFXManager.s_OnActionFXCompleted += HandleCards;
        //TurnManager.s_OnTurnStart += ReturnCardsToScreen;
    }

    private void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(gameObject);
    }

    void Start () {
        for (int i = 0; i < m_CardPositions.Count; i++)
        {
            m_CardDefaultPositions.Add(m_CardPositions[i].position);
        }
	}

    private void HandleCards()
    {
        DrawCard(true);
    }

    public void DiscardCard(Card card, bool endTurn)
    {
        StartCoroutine(DiscardCardAnimated(card, endTurn));
        return;
        CardSelector.s_Instance.SelectedCard.CardData = CardManager.s_Instance.GetRandomCard();
        card.gameObject.SetActive(false);
        TurnManager.s_Instance.CurrentPlayer.PlayerData.Cards[m_IndexInHandPosition] = CardSelector.s_Instance.SelectedCard.CardData;
        card.SetCardInfo();
        card.transform.DOMove(m_CardDeckPosition.position, 0.1f);
        card.transform.DOScale(0.7f, 0.1f);
        m_SelectedCard = card;
        m_IndexInHandPosition = m_SelectedCard.IndexInHand;

        if(endTurn)
            DrawCard(endTurn);
    }

    private IEnumerator DiscardCardAnimated(Card card, bool endTurn)
    {
        card.Burn();
        yield return new WaitForSeconds(0.5f);
        card.SetAlpha(0);
        yield return new WaitForSeconds(0.7f);
        CardSelector.s_Instance.SelectedCard.CardData = CardManager.s_Instance.GetRandomCard();
        TurnManager.s_Instance.CurrentPlayer.PlayerData.Cards[m_IndexInHandPosition] = CardSelector.s_Instance.SelectedCard.CardData;
        card.SetCardInfo();
        card.transform.DOMove(m_CardDeckPosition.position, 0.1f);
        card.transform.DOScale(0.7f, 0.1f);
        m_SelectedCard = card;
        m_IndexInHandPosition = m_SelectedCard.IndexInHand;

        if (endTurn)
            DrawCard(endTurn);
    }

    private void DrawCard(bool endTurn)
    {
        if(s_OnDrawCard != null) s_OnDrawCard();

        //Card references
        int CardToRemove = CardSelector.s_Instance.PlayerHandCards.Count - 1;
        RectTransform drawnCard = m_SelectedCard.transform as RectTransform;
        //Tween positions
        Vector2 cardDefaultPosition = m_SelectedCard.DefaultPosition;
        Vector2 centerOfScreen = new Vector2(Screen.width/2, Screen.height/2);
        Vector3 rotationVector = new Vector3(0, 0, 20);

        CardSelector.s_Instance.CanSelectCard = false;
        TurnManager.s_Instance.CurrentPlayer.PlayerData.Cards[m_IndexInHandPosition] = CardSelector.s_Instance.SelectedCard.CardData;
        CardSelector.s_Instance.SelectedCard = null;

        //Tween sequence
        Sequence drawSequence = DOTween.Sequence();
        drawSequence.AppendInterval(0.5f);
        drawSequence.AppendCallback(() => m_SelectedCard.SetAlpha(1));
        drawSequence.Append(drawnCard.DOMove(centerOfScreen, 0.5f));
        drawSequence.Join(drawnCard.DOScale(1.5f, 0.5f));
        drawSequence.AppendCallback(() => m_SelectedCard.Shine());
        drawSequence.AppendInterval(1f);
        drawSequence.AppendCallback(() => drawnCard.SetSiblingIndex(m_IndexInHandPosition));
        drawSequence.Append(drawnCard.DOAnchorPos(cardDefaultPosition, 0.5f));
        drawSequence.Join(drawnCard.DOScale(1, 0.5f));
        drawSequence.AppendInterval(0.2f);
        for (int i = 0; i < CardSelector.s_Instance.PlayerHandCards.Count; i++)
        {
            RectTransform cardTransform = CardSelector.s_Instance.PlayerHandCards[CardToRemove].transform as RectTransform;
            Card card = CardSelector.s_Instance.PlayerHandCards[CardToRemove];
            Vector2 newCardPos = new Vector2(card.DefaultPosition.x + 350, -Screen.height * 2 );
            drawSequence.Append(cardTransform.DOAnchorPos(newCardPos, 0.2f).SetEase(Ease.InSine));
            drawSequence.Join(cardTransform.DORotate(rotationVector, 0.2f).SetEase(Ease.InSine));
            if (CardToRemove > 0)
            {
                CardToRemove--;
            }
        }

        if(endTurn)
            drawSequence.AppendCallback(() => TurnManager.s_OnTurnEnd());
    }

    public void ReturnCardsToScreen()
    {
        Sequence showHandSequence = DOTween.Sequence();
        for (int i = 0; i < CardSelector.s_Instance.PlayerHandCards.Count; i++)
        {
            RectTransform cardTransform = CardSelector.s_Instance.PlayerHandCards[i].transform as RectTransform;
            Card card = CardSelector.s_Instance.PlayerHandCards[i];
            showHandSequence.Append(cardTransform.DOAnchorPos(card.DefaultPosition,0.25f).SetEase(Ease.OutSine));
            showHandSequence.Join(cardTransform.DORotate(Vector3.zero, 0.25f).SetEase(Ease.OutSine));
        }
    }

    private void OnDisable()
    {
        s_OnDiscardCard -= DiscardCard;
        ActionFXManager.s_OnActionFXCompleted -= HandleCards;
    }
}