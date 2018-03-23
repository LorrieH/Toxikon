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
        DrawCard();
    }

    IEnumerator DrawCardRoutine()
    {
        if(s_OnDrawCard != null) s_OnDrawCard();

        int CardToRemove = CardSelector.s_Instance.PlayerHandCards.Count - 1;
        Vector3 rotationVector = new Vector3(0, 0, 20);
        CardSelector.s_Instance.CanSelectCard = false;
        TurnManager.s_Instance.CurrentPlayer.PlayerData.Cards[m_IndexInHandPosition] = CardSelector.s_Instance.SelectedCard.CardData;
        CardSelector.s_Instance.SelectedCard = null;

        Sequence drawSequence = DOTween.Sequence();
        drawSequence.AppendInterval(0.5f);
        drawSequence.AppendCallback(() => m_SelectedCard.gameObject.SetActive(true));
        drawSequence.Append(m_SelectedCard.transform.DOMove(m_ShowDrawnCardPosition.position, 0.5f));
        drawSequence.Join(m_SelectedCard.transform.DOScale(1.5f, 0.5f));
        drawSequence.AppendInterval(0.5f);
        drawSequence.Append(m_SelectedCard.transform.DOMove(m_CardDefaultPositions[m_IndexInHandPosition], 0.5f));
        drawSequence.Join(m_SelectedCard.transform.DOScale(1, 0.5f));
        drawSequence.AppendCallback(() => m_SelectedCard.transform.SetSiblingIndex(m_IndexInHandPosition));
        drawSequence.AppendInterval(0.2f);
        for (int i = 0; i < CardSelector.s_Instance.PlayerHandCards.Count; i++)
        {
            drawSequence.Append(CardSelector.s_Instance.PlayerHandCards[CardToRemove].transform.DOMove(m_OutOfScreenPosition.position, 0.2f).SetEase(Ease.InSine));
            drawSequence.Join(CardSelector.s_Instance.PlayerHandCards[CardToRemove].transform.DORotate(rotationVector, 0.2f).SetEase(Ease.InSine));
            if (CardToRemove > 0)
            {
                CardToRemove--;
            }
        }
        drawSequence.AppendCallback(() => TurnManager.s_OnTurnEnd());
    }

    public void ReturnCardsToScreen()
    {
        Sequence showHandSequence = DOTween.Sequence();
        for (int i = 0; i < CardSelector.s_Instance.PlayerHandCards.Count; i++)
        {
            showHandSequence.Append(CardSelector.s_Instance.PlayerHandCards[i].transform.DOMove(m_CardDefaultPositions[CardSelector.s_Instance.PlayerHandCards[i].IndexInHand],0.25f).SetEase(Ease.OutSine));
            showHandSequence.Join(CardSelector.s_Instance.PlayerHandCards[i].transform.DORotate(Vector3.zero, 0.25f).SetEase(Ease.OutSine));
        }
    }

    private void OnDisable()
    {
        s_OnDiscardCard -= DiscardCard;
    }
}