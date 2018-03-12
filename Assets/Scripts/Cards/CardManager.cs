using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CardManager : MonoBehaviour
{
    public static CardManager s_Instance;

    [SerializeField] private Transform m_CardHolder;

    private readonly int m_MaxCardsInHand = 5;
    public int MaxCardsInHand{ get { return m_MaxCardsInHand; } }
    private List<GameObject> m_Cards = new List<GameObject>();
    private List<CardData> m_CardDatas = new List<CardData>();
    

    public List<CardData> CardDatas
    {
        get { return m_CardDatas; }
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        m_CardDatas = GetAllCardsFromConfig();
        ShuffleCards();
    }

    private List<CardData> GetAllCardsFromConfig()
    {
        List<CardData> cards = new List<CardData>();
        cards.AddRange(CardConfig.s_PathCards);
        cards.AddRange(CardConfig.s_ActionCards);

        return cards;
    }

    private void ShuffleCards()
    {
        m_Cards.OrderBy(a => Guid.NewGuid()).ToList();
    }

    public CardData GetRandomPathCard()
    {
        int randomIndex = UnityEngine.Random.Range(0, CardConfig.s_PathCards.Count - 1);
        return CardConfig.s_PathCards[randomIndex];
    }

    public CardData GetRandomActionCard()
    {
        int randomIndex = UnityEngine.Random.Range(0, CardConfig.s_ActionCards.Count - 1);
        return CardConfig.s_ActionCards[randomIndex];
    }

    public CardData GetRandomCard()
    {
        int randomIndex = UnityEngine.Random.Range(0, m_Cards.Count - 1);
        return m_CardDatas[randomIndex];
    }

    public List<CardData> GetRandomHand()
    {
        //TODO: Return random hand
        return null;
    }
}
