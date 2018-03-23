using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardSelector : MonoBehaviour
{

    public delegate void ToggleCardSelectEvent(Card selectedCard, int cardInHandIndex);
    public static ToggleCardSelectEvent s_OnToggleCardSelect;

    public static CardSelector s_Instance;

    [SerializeField] private Transform m_SelectedCardHolder;

    [Space(20f)]
    [Header("Cards")]
    [SerializeField]
    private List<Card> m_PlayerHandCards = new List<Card>();

    private Card m_SelectedCard;
    private bool m_CanSelectCard = true;

    public List<Card> PlayerHandCards { get { return m_PlayerHandCards; }}

    public Card SelectedCard {
        get { return m_SelectedCard; }
        set { m_SelectedCard = value; }
    }
    public bool CanSelectCard {
        get { return m_CanSelectCard; }
        set { m_CanSelectCard = value; }
    }

    private void Init()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(gameObject);
    }

    private void Awake()
    {
        Init();
        TurnManager.s_OnTurnStart += ShowCurrentPlayerHand;
    }

    private void OnDestroy()
    {
        TurnManager.s_OnTurnStart -= ShowCurrentPlayerHand;
    }

    private void OnEnable()
    {
        s_OnToggleCardSelect += ToggleCardSelect;
    }

    private void ToggleCardSelect(Card selectedCard, int cardInHandIndex)
    {
        SelectCard(selectedCard, cardInHandIndex);
        return;
    }

    private void SelectCard(Card selectedCard, int cardInHandIndex)
    {
        if (m_CanSelectCard)
        {
            //Returns all cards back to the hand first
            for (int i = 0; i < m_PlayerHandCards.Count; i++)
            {
                m_PlayerHandCards[i].transform.DOMove(CardPositionHolder.s_Instance.CardDefaultPositions[m_PlayerHandCards[i].IndexInHand], 0.2f).SetEase(Ease.OutExpo);
                m_PlayerHandCards[i].transform.SetSiblingIndex(m_PlayerHandCards[i].IndexInHand);
                m_PlayerHandCards[i].transform.DOScale(1, 0.2f);
            }

            StartCoroutine(CardSelectDelay(0.2f));

            if (m_SelectedCard == selectedCard)
            {
                selectedCard.transform.DOMove(CardPositionHolder.s_Instance.CardDefaultPositions[selectedCard.IndexInHand], 0.2f).SetEase(Ease.OutExpo);
                selectedCard.transform.SetSiblingIndex(selectedCard.IndexInHand);
                selectedCard.transform.DOScale(1, 0.2f);
                m_SelectedCard = null;
            }
            else
            {
                //If the selected card was not the selected card already, move to selected position and select it
                Sequence CardSelectSequence = DOTween.Sequence();
                CardSelectSequence.Append(selectedCard.transform.DOMove(m_SelectedCardHolder.position, 0.35f));
                CardSelectSequence.Join(selectedCard.transform.DOScale(1.3f, 0.4f));
                selectedCard.transform.SetSiblingIndex(6); //Puts the selected card on top of the layering hierarchy
                m_SelectedCard = selectedCard;
            }
        }
    }

    IEnumerator CardSelectDelay(float countDown)
    {
        m_CanSelectCard = false;
        yield return new WaitForSeconds(countDown);
        m_CanSelectCard = true;
    }

    private void ShowCurrentPlayerHand()
    {
        for (int i = 0; i < m_PlayerHandCards.Count; i++)
        {
            m_PlayerHandCards[i].CardData = TurnManager.s_Instance.CurrentPlayer.PlayerData.Cards[i];
            m_PlayerHandCards[i].SetCardInfo();
        }
    }

    private void OnDisable()
    {
        s_OnToggleCardSelect -= ToggleCardSelect;
    }
}