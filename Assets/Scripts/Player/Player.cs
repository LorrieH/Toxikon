﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    public string Name;
    public string AvatarImageName;
    public List<CardData> Cards;
}

public class Player : MonoBehaviour
{
    private PlayerData m_PlayerData;
    public PlayerData PlayerData{ get { return m_PlayerData; } set { m_PlayerData = value; } }

    private void Awake()
    {
        TurnManager.s_OnFirstTurnStart += GetStartingHand;
    }

    private void OnDestroy()
    {
        TurnManager.s_OnFirstTurnStart -= GetStartingHand;
    }

    private void GetStartingHand()
    {
        m_PlayerData.Cards = CardManager.s_Instance.GetRandomHand();
    }
}
