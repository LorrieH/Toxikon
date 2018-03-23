using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardPositionHolder : MonoBehaviour {

    public delegate void DiscardCardEvent(Card card);
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
        s_OnDrawCard += DrawCard;
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

    public void DiscardCard(Card card)
    {
        CardSelector.s_Instance.SelectedCard.CardData = CardManager.s_Instance.GetRandomCard();
        card.gameObject.SetActive(false);
        TurnManager.s_Instance.CurrentPlayer.PlayerData.Cards[m_IndexInHandPosition] = CardSelector.s_Instance.SelectedCard.CardData;
        card.SetCardInfo();
        card.transform.DOMove(m_CardDeckPosition.position, 0.1f);
        card.transform.DOScale(0.7f, 0.1f);
        m_SelectedCard = card;
        m_IndexInHandPosition = m_SelectedCard.IndexInHand;
        StartCoroutine(DrawCardRoutine());
    }

    public void DrawCard()
    {
        StartCoroutine(DrawCardRoutine());
    }

    IEnumerator DrawCardRoutine()
    {        
        CardSelector.s_Instance.CanSelectCard = false;
        TurnManager.s_Instance.CurrentPlayer.PlayerData.Cards[m_IndexInHandPosition] = CardSelector.s_Instance.SelectedCard.CardData;
        CardSelector.s_Instance.SelectedCard = null;
        yield return new WaitForSeconds(0.5f);
        m_SelectedCard.gameObject.SetActive(true);
        
        m_SelectedCard.transform.DOMove(m_ShowDrawnCardPosition.position, 0.5f);
        m_SelectedCard.transform.DOScale(1.5f, 0.5f);
        yield return new WaitForSeconds(1.2f);
        m_SelectedCard.transform.DOMove(m_CardDefaultPositions[m_IndexInHandPosition], 0.5f); //Moves card to the position of the card that was discarded
        m_SelectedCard.transform.DOScale(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        m_SelectedCard.transform.SetSiblingIndex(m_IndexInHandPosition); //Sets the appropriate layer for the card and places it correctly in the players hand
        yield return new WaitForSeconds(0.2f);
        Sequence removeHandSequence = DOTween.Sequence();
        //Lets the cards in the players hand drop out of the screen (Starting from the bottom)
        int CardToRemove = CardSelector.s_Instance.PlayerHandCards.Count -1;
        for (int i = 0; i < CardSelector.s_Instance.PlayerHandCards.Count; i++)
        {            
            removeHandSequence.Append(CardSelector.s_Instance.PlayerHandCards[CardToRemove].transform.DOMove(m_OutOfScreenPosition.position,0.25f));
            if (CardToRemove > 0)
            {
                CardToRemove--;
            }
        }
        yield return new WaitForSeconds(1f);
        TurnManager.s_OnTurnEnd();
    }

    public void ReturnCardsToScreen()
    {
        Sequence showHandSequence = DOTween.Sequence();
        for (int i = 0; i < CardSelector.s_Instance.PlayerHandCards.Count; i++)
        {
            showHandSequence.Append(CardSelector.s_Instance.PlayerHandCards[i].transform.DOMove(m_CardDefaultPositions[CardSelector.s_Instance.PlayerHandCards[i].IndexInHand],0.25f));
        }
    }

    private void OnDisable()
    {
        s_OnDiscardCard -= DiscardCard;
        s_OnDrawCard -= DrawCard;
    }
}