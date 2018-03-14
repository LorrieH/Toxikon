using System.Collections;
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
        PlayerCharacter pc = TurnManager.s_Instance.CurrentPlayer.GetComponent<PlayerCharacter>();
        m_PlayerName.text = pc.CharacterName;
        m_PlayerImage.sprite = pc.CharacterSprite;
    }

    private void OnDisable()
    {
        TurnManager.s_OnTurnStart -= SetPlayerInfo;
    }
}
