using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

        string spritePath = "Characters/" + playerData.AvatarImageName;

        m_PlayerName.text = playerData.Name;
        m_PlayerImage.sprite = Resources.Load<Sprite>(spritePath);
        m_PlayerImage.SetNativeSize();
    }

    void AnimatePlayerInfo()
    {
        Sequence moveInfoSequence = DOTween.Sequence();

    }

    private void OnDisable()
    {
        TurnManager.s_OnTurnStart -= SetPlayerInfo;
    }
}
