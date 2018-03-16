using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardSelector : MonoBehaviour {

    public delegate void ToggleCardSelectEvent(CardData selectedCard, Transform cardTransform, int cardInHandIndex);
    public static ToggleCardSelectEvent s_OnToggleCardSelect;

    public static CardSelector s_Instance;
    [SerializeField] private Transform m_CardHolder;
    [SerializeField] private Transform m_SelectedCardHolder;
    private CardData m_SelectedCard;
    private bool m_CanSelectCard = true;

    public CardData SelectedCard { get { return m_SelectedCard; } }

    [Space(20f)]
    [Header("Cards")]
    [SerializeField] private List<Card> m_PlayerHandCards = new List<Card>();


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

    private void Start()
    {
        /*
        if(CardManager.s_Instance == null)
        {
            Debug.LogError("CardManager is null, could not get random card data");
            return;
        }

        for (int i = 0; i < m_PlayerHandCards.Count; i++)
        {
            m_PlayerHandCards[i].CardData = CardManager.s_Instance.GetRandomCard();
            m_PlayerHandCards[i].SetCardInfo();
        }
        */
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
        if (m_CanSelectCard)
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

        
            StartCoroutine(CardSelectDelay(0.5f));
            if (m_SelectedCard != selectedCard)
            {
                //If the selected card was not the selected card already, move to selected position and select it
                Sequence CardSelectSequence = DOTween.Sequence();
                cardTransform.SetParent(m_SelectedCardHolder);
                cardTransform.DOMove(m_SelectedCardHolder.position, 0.5f);
                cardTransform.DOScale(1.3f, 0.4f);
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