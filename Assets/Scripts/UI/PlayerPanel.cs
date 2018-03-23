using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
   
    [SerializeField] private Text m_PlayerName;
    [SerializeField] private Image m_PlayerAvatar;
    private Color m_PlayerColor;
    [Space(15f)]
    [SerializeField] private InputField m_InputField;

    private PlayerData m_PlayerData;
    private int m_ColorNumber;

    public PlayerData PlayerData
    {
        get { return m_PlayerData; }
        set { m_PlayerData = value; }
    }

    private void Awake()
    {
        m_InputField.onEndEdit.AddListener(delegate { OnInputFieldValueChanged(); });
        m_ColorNumber = 0;
        m_PlayerAvatar.sprite = PlayerSelection.s_Instance.AvailableSprites[m_ColorNumber];
        m_PlayerColor = PlayerSelection.s_Instance.AvailableColors[m_ColorNumber];
    }

    public void NextSprite()
    {
        m_ColorNumber++;
        if(m_ColorNumber >= PlayerSelection.s_Instance.AvailableSprites.Count)
            m_ColorNumber = 0;

        m_PlayerAvatar.sprite = PlayerSelection.s_Instance.AvailableSprites[m_ColorNumber];
        m_PlayerColor = PlayerSelection.s_Instance.AvailableColors[m_ColorNumber];

    }

    public void PreviousSprite()
    {
        m_ColorNumber--;
        if (m_ColorNumber < 0)
            m_ColorNumber = PlayerSelection.s_Instance.AvailableSprites.Count - 1;

        m_PlayerAvatar.sprite = PlayerSelection.s_Instance.AvailableSprites[m_ColorNumber];
        m_PlayerColor = PlayerSelection.s_Instance.AvailableColors[m_ColorNumber];
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
        PlayerSelection.s_Instance.AvailableSprites.RemoveAt(m_ColorNumber);
        PlayerSelection.s_Instance.AvailableColors.RemoveAt(m_ColorNumber);
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