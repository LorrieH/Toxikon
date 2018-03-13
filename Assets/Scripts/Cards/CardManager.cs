using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CardManager : MonoBehaviour
{
    public static CardManager s_Instance;

    private readonly int m_MaxCardsInHand = 5;
    public int MaxCardsInHand{ get { return m_MaxCardsInHand; } }
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
        m_CardDatas.OrderBy(a => Guid.NewGuid()).ToList();
    }

    public CardData GetRandomPathCard()
    {
        int randomIndex = UnityEngine.Random.Range(0, CardConfig.s_PathCards.Count);
        return CardConfig.s_PathCards[randomIndex];
    }

    public CardData GetRandomActionCard()
    {
        int randomIndex = UnityEngine.Random.Range(0, CardConfig.s_ActionCards.Count);
        return CardConfig.s_ActionCards[randomIndex];
    }

    public CardData GetRandomCard()
    {
        return m_CardDatas[GetRandomCardIndex()];
    }

    public List<CardData> GetRandomHand()
    {
        //TODO: Return random hand
        return null;
    }

    public int GetRandomCardIndex()
    {
        return UnityEngine.Random.Range(0, m_CardDatas.Count);
    }
}
