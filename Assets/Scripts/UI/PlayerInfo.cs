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
        playerInfoSequence.Append(m_PlayerName.transform.DOMoveX(40,0.25f).SetEase(Ease.OutBounce));
        playerInfoSequence.Append(m_PlayerImage.transform.DOMoveX(40, 0.25f).SetEase(Ease.OutBounce));
    }

    void RemovePlayerInfo()
    {
        Sequence playerInfoSequence = DOTween.Sequence();
        playerInfoSequence.Append(m_PlayerName.transform.DOMoveX(- 400, 0.25f).SetEase(Ease.InBounce));
        playerInfoSequence.Append(m_PlayerImage.transform.DOMoveX(- 250, 0.25f).SetEase(Ease.InBounce));
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