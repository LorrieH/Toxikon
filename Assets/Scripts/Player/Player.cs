using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData
{
    public string Name;
    public Sprite Avatar;
    public string AvatarImageName;
    public PlayerHand Hand;
}

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
    private PlayerData m_PlayerData;
    public PlayerData PlayerData{ get { return m_PlayerData; } set { m_PlayerData = value; } }
    public PlayerHand PlayerHand { get { return m_PlayerData.Hand; } set { m_PlayerData.Hand = value; } }

    private void Awake()
    {

    }

    public void SetName(string name)
    {
        m_PlayerData.Name = name;
    }

    public void SetCharacter(Sprite avatar)
    {
        m_PlayerData.Avatar = avatar;
    }
}
