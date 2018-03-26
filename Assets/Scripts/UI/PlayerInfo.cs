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
        TurnManager.s_OnTurnEnd += RemovePlayerInfo;
        TurnManager.s_OnTurnStart += MovePlayerInfo;
        TurnManager.s_OnTurnStart += SetPlayerInfo;
    }

    void MovePlayerInfo()
    {
        Sequence playerInfoSequence = DOTween.Sequence();
        playerInfoSequence.Append(m_PlayerImage.rectTransform.DOAnchorPosX(15, 0.5f).SetEase(Ease.OutExpo));
        playerInfoSequence.Join(m_PlayerName.rectTransform.DOAnchorPosX(15, 0.5f).SetEase(Ease.OutExpo).SetDelay(0.025f));
    }

    void RemovePlayerInfo()
    {
        Sequence playerInfoSequence = DOTween.Sequence();
        playerInfoSequence.Append(m_PlayerName.rectTransform.DOAnchorPosX(-400, 0.5f).SetEase(Ease.InBack));
        playerInfoSequence.Join(m_PlayerImage.rectTransform.DOAnchorPosX(-400, 0.5f).SetEase(Ease.InBack).SetDelay(0.025f));

    }
    
    void SetPlayerInfo()
    {
        PlayerData playerData = TurnManager.s_Instance.CurrentPlayer.PlayerData;

        string spritePath = "Characters/" + playerData.AvatarImageName;

        m_PlayerName.text = playerData.Name;
        m_PlayerImage.sprite = Resources.Load<Sprite>(spritePath);
        m_PlayerImage.SetNativeSize();
    }

    private void OnDisable()
    {
        TurnManager.s_OnTurnStart -= SetPlayerInfo;
    }
}