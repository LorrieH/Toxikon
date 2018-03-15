﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private Text m_PlayerName;
    [SerializeField] private Image m_PlayerImage;

    private void OnEnable()
    {
        TurnManager.s_OnTurnStart += SetPlayerInfo;
    }
    
    void SetPlayerInfo()
    {
        PlayerData playerData = TurnManager.s_Instance.CurrentPlayer.PlayerData;

        m_PlayerName.text = playerData.Name;
        m_PlayerImage.sprite = playerData.Avatar;
    }

    private void OnDisable()
    {
        TurnManager.s_OnTurnStart -= SetPlayerInfo;
    }
}
