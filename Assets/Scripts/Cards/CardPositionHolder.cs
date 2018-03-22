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
        {
            s_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
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
        //TurnManager.s_Instance.CurrentPlayer.PlayerData.Cards[m_IndexInHandPosition] = CardManager.s_Instance.GetRandomCard();
        TurnManager.s_Instance.CurrentPlayer.PlayerData.Cards[m_IndexInHandPosition] = CardSelector.s_Instance.SelectedCard.CardData;
        card.SetCardInfo();
        //CardSelector.s_Instance.SelectedCard = null;
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
        yield return new WaitForSeconds(0.5f);
        m_SelectedCard.gameObject.SetActive(true);
        TurnManager.s_Instance.CurrentPlayer.PlayerData.Cards[m_IndexInHandPosition] = CardSelector.s_Instance.SelectedCard.CardData;
        //CardSelector.s_Instance.SelectedCard.SetCardInfo();
        CardSelector.s_Instance.SelectedCard = null;
        m_SelectedCard.transform.DOMove(m_ShowDrawnCardPosition.position, 0.5f);
        m_SelectedCard.transform.DOScale(1.5f, 0.5f);
        yield return new WaitForSeconds(1.2f);
        m_SelectedCard.transform.DOMove(m_CardDefaultPositions[m_IndexInHandPosition], 0.5f);
        m_SelectedCard.transform.DOScale(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        m_SelectedCard.transform.SetSiblingIndex(m_IndexInHandPosition);
        TurnManager.s_OnTurnEnd();

    }

    private void OnDisable()
    {
        s_OnDiscardCard -= DiscardCard;
        s_OnDrawCard -= DrawCard;
    }
}