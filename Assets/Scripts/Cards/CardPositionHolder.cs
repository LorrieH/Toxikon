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

    public List<Vector2> CardDefaultPositions { get { return CardDefaultPositions; }}

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
        card.gameObject.SetActive(false);
        card.transform.DOMove(m_CardDeckPosition.position, 0.1f);
        card.transform.DOScale(0.7f, 0.1f);
        m_SelectedCard = card;
        m_IndexInHandPosition = m_SelectedCard.IndexInHand;
        TurnManager.s_OnTurnEnd();
    }

    public void DrawCard()
    {
        StartCoroutine(DrawCardRoutine());
    }

    IEnumerator DrawCardRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        m_SelectedCard.CardData = CardManager.s_Instance.GetRandomCard();        
        m_SelectedCard.gameObject.SetActive(true);
        m_SelectedCard.transform.DOMove(m_CardDefaultPositions[m_IndexInHandPosition], 0.5f);
        m_SelectedCard.transform.DOScale(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        m_SelectedCard.transform.SetSiblingIndex(m_IndexInHandPosition);
    }

    private void OnDisable()
    {
        s_OnDiscardCard -= DiscardCard;
        s_OnDrawCard -= DrawCard;
    }
}