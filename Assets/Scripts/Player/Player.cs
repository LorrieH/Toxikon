using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand
{
    public List<CardData> Cards;

    public void AddCardToHand()
    {
        if (CanAddCardToHand())
            Cards.Add(CardManager.s_Instance.GetRandomCard());
    }

    public void AddCardToHand(CardData card)
    {
        if (CanAddCardToHand())
            Cards.Add(card);
    }

    public void RemoveCardFromHand(CardData card)
    {
        if (Cards.Contains(card))
            Cards.Remove(card);
        else
            Debug.LogWarning("[Player.cs] Could not remove card from hand. Card does not exist in hand.");
    }

    private bool CanAddCardToHand()
    {
        return (Cards.Count < CardManager.s_Instance.MaxCardsInHand) ? true : false;
    }
}

public class Player : MonoBehaviour
{
    private PlayerHand m_PlayerHand;
    public PlayerHand PlayerHand
    {
        get { return m_PlayerHand; }
        set { m_PlayerHand = value; }
    }

    private void Awake()
    {

    }
}
