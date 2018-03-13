using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardSelector : MonoBehaviour {

    public delegate void ToggleCardSelectEvent(CardData selectedCard, Transform cardTransform, int cardInHandIndex);
    public static ToggleCardSelectEvent s_OnToggleCardSelect;
    [SerializeField] private Transform m_CardHolder;
    [SerializeField] private Transform m_SelectedCardHolder;
    private CardData m_SelectedCard;

    [Space(20f)]
    [Header("Cards")]
    [SerializeField] private List<Card> m_PlayerHandCards = new List<Card>();

    private void Awake()
    {
        for (int i = 0; i < m_PlayerHandCards.Count; i++)
        {
            m_PlayerHandCards[i].CardData = CardManager.s_Instance.GetRandomCard();
            m_PlayerHandCards[i].SetCardInfo();
        }
    }

    private void OnEnable()
    {
        s_OnToggleCardSelect += ToggleCardSelect;
    }

    private void ToggleCardSelect(CardData selectedCard, Transform cardTransform, int cardInHandIndex)
    {
        SelectCard(selectedCard, cardTransform, cardInHandIndex);
        return;
    }

    private void SelectCard(CardData selectedCard, Transform cardTransform, int cardInHandIndex)
    {
        
        //Returns all cards back to the hand first
        for (int i = 0; i < m_PlayerHandCards.Count; i++)
        {
            Sequence CardDeselectSequence = DOTween.Sequence();

            CardDeselectSequence.Append(m_PlayerHandCards[i].transform.DOScale(1, 0.2f));
            m_PlayerHandCards[i].transform.SetParent(m_CardHolder);
            m_PlayerHandCards[i].transform.SetSiblingIndex(cardInHandIndex);
            m_SelectedCard = null;

        }

        if (m_SelectedCard != selectedCard)
        {
            Sequence CardSelectSequence = DOTween.Sequence();
            cardTransform.SetParent(m_SelectedCardHolder);
            CardSelectSequence.Append(cardTransform.DOMove(m_SelectedCardHolder.position, 0.3f));
            CardSelectSequence.Append(cardTransform.DOScale(1.3f, 0.2f));
            m_SelectedCard = selectedCard;
        }
        else if (m_SelectedCard == selectedCard)
        {
            Sequence CardDeselectSequence = DOTween.Sequence();

            CardDeselectSequence.Append(cardTransform.DOScale(1, 0.2f));
            cardTransform.SetParent(m_CardHolder);
            cardTransform.SetSiblingIndex(cardInHandIndex);
            m_SelectedCard = null;
        }
    }


    private void DeselectCard(Transform cardTransform,Vector2 defaultPosition,int cardInHandIndex)
    {
        Sequence CardDeselectSequence = DOTween.Sequence();

        CardDeselectSequence.Append(cardTransform.DOScale(1, 0.2f));
        cardTransform.SetParent(m_CardHolder);
        cardTransform.SetSiblingIndex(cardInHandIndex);
        m_SelectedCard = null;
    }

    private void OnDisable()
    {
        s_OnToggleCardSelect -= ToggleCardSelect;
    }
}