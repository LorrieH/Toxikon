using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CardManager : MonoBehaviour
{
    public  static CardManager s_Instance;
    private readonly int m_MaxCardsInHand = 5;
    public  int MaxCardsInHand{ get { return m_MaxCardsInHand; } }
    private List<CardData> m_CardDatas = new List<CardData>();
    public List<CardData> CardDatas { get { return m_CardDatas; } }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Creates a instance of this object, if there is an instance already delete the new one
    /// </summary>
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

    /// <summary>
    /// Gets all the cards from the card config script
    /// </summary>
    /// <returns></returns>
    private List<CardData> GetAllCardsFromConfig()
    {
        List<CardData> cards = new List<CardData>();
        cards.AddRange(CardConfig.s_PathCards);
        cards.AddRange(CardConfig.s_ActionCards);

        return cards;
    }

    /// <summary>
    /// Shuffles the cards
    /// </summary>
    private void ShuffleCards()
    {
        m_CardDatas.OrderBy(a => Guid.NewGuid()).ToList();
    }

    /// <summary>
    /// Returns a random card from the card config
    /// </summary>
    /// <returns></returns>
    public CardData GetRandomCard()
    {
        return m_CardDatas[GetRandomCardIndex()];
    }

    /// <summary>
    /// Returns a random hand of cards
    /// </summary>
    /// <returns></returns>
    public List<CardData> GetRandomHand()
    {
        List<CardData> tempCardData = new List<CardData>();

        for (int i = 0; i < m_MaxCardsInHand; i++)
            tempCardData.Add(GetRandomCard());

        return tempCardData;
    }

    /// <summary>
    /// Returns a random number between 0 and the max amount of card datas
    /// </summary>
    /// <returns></returns>
    public int GetRandomCardIndex()
    {
        return UnityEngine.Random.Range(0, m_CardDatas.Count);
    }
}