using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    private PlayerData m_PlayerData;
    public PlayerData PlayerData { get { return m_PlayerData; } set { m_PlayerData = value; } }
    private int m_SpriteNumber;
    [SerializeField] private Text m_PlayerName;
    [SerializeField] private Image m_PlayerAvatar;
    private Color m_PlayerColor;
    [Space(15f)]
    [SerializeField] private InputField m_InputField;

    private void Awake()
    {
        m_InputField.onEndEdit.AddListener(delegate { OnInputFieldValueChanged(); });
        m_SpriteNumber = 0;
        m_PlayerAvatar.sprite = PlayerSelection.s_Instance.AvailableSprites[m_SpriteNumber];
        m_PlayerColor = PlayerSelection.s_Instance.AvailableColors[m_SpriteNumber];
    }

    public void NextSprite()
    {
        m_SpriteNumber++;
        if(m_SpriteNumber >= PlayerSelection.s_Instance.AvailableSprites.Count)
            m_SpriteNumber = 0;

        m_PlayerAvatar.sprite = PlayerSelection.s_Instance.AvailableSprites[m_SpriteNumber];
        m_PlayerColor = PlayerSelection.s_Instance.AvailableColors[m_SpriteNumber];

    }

    public void PreviousSprite()
    {
        m_SpriteNumber--;
        if (m_SpriteNumber < 0)
            m_SpriteNumber = PlayerSelection.s_Instance.AvailableSprites.Count - 1;

        m_PlayerAvatar.sprite = PlayerSelection.s_Instance.AvailableSprites[m_SpriteNumber];
        m_PlayerColor = PlayerSelection.s_Instance.AvailableColors[m_SpriteNumber];
    }

    public void ConfirmInfo()
    {
        m_PlayerData.Name = m_PlayerName.text;
        m_PlayerData.AvatarImageName = m_PlayerAvatar.sprite.name;
        m_PlayerData.PlayerColor = m_PlayerColor;
        RemoveAvailableSprite();
        PlayerSelection.s_Instance.TransitionOutPlayerPanels();
        PlayerSelection.s_Instance.ChangeAmountOfChecks();
    }

    private void RemoveAvailableSprite()
    {
        PlayerSelection.s_Instance.AvailableSprites.RemoveAt(m_SpriteNumber);
        PlayerSelection.s_Instance.AvailableColors.RemoveAt(m_SpriteNumber);
    }

    private void OnInputFieldValueChanged()
    {
        if(m_InputField.text != string.Empty)
        {
            m_PlayerData.Name = m_InputField.text;
            m_PlayerName.text = m_PlayerData.Name;
        }

        m_InputField.text = string.Empty;
    }
}