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
    private string m_PlayerSkin;

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
        m_PlayerSkin = PlayerSelection.s_Instance.AvailableSkins[m_ColorNumber];
    }

    /// <summary>
    /// Shows the next sprite in the available sprites list
    /// </summary>
    public void NextSprite()
    {
        m_ColorNumber++;
        if(m_ColorNumber >= PlayerSelection.s_Instance.AvailableSprites.Count)
            m_ColorNumber = 0;

        m_PlayerAvatar.sprite = PlayerSelection.s_Instance.AvailableSprites[m_ColorNumber];
        m_PlayerColor = PlayerSelection.s_Instance.AvailableColors[m_ColorNumber];
        m_PlayerSkin = PlayerSelection.s_Instance.AvailableSkins[m_ColorNumber];

    }

    /// <summary>
    /// Shows the previous sprite in the available sprites list
    /// </summary>
    public void PreviousSprite()
    {
        m_ColorNumber--;
        if (m_ColorNumber < 0)
            m_ColorNumber = PlayerSelection.s_Instance.AvailableSprites.Count - 1;

        m_PlayerAvatar.sprite = PlayerSelection.s_Instance.AvailableSprites[m_ColorNumber];
        m_PlayerColor = PlayerSelection.s_Instance.AvailableColors[m_ColorNumber];
        m_PlayerSkin = PlayerSelection.s_Instance.AvailableSkins[m_ColorNumber];
    }

    /// <summary>
    /// Confirms the players choices
    /// </summary>
    public void ConfirmInfo()
    {
        if(m_PlayerName.text != string.Empty)
        {
            m_PlayerData.Name = m_PlayerName.text;
            m_PlayerData.AvatarImageName = m_PlayerAvatar.sprite.name;
            m_PlayerData.PlayerColor = m_PlayerColor;
            m_PlayerData.SkinName = m_PlayerSkin;
            RemoveAvailableSprite();
            PlayerSelection.s_Instance.ChangeAmountOfChecks();
        }
        else
        {
            NotificationManager.s_Instance.EnqueueNotification("Please enter a name", 1f, Color.red);       
        }
        
    }

    /// <summary>
    /// Remove a chosen sprite from the available sprites list
    /// </summary>
    private void RemoveAvailableSprite()
    {
        PlayerSelection.s_Instance.AvailableSprites.RemoveAt(m_ColorNumber);
        PlayerSelection.s_Instance.AvailableColors.RemoveAt(m_ColorNumber);
        PlayerSelection.s_Instance.AvailableSkins.RemoveAt(m_ColorNumber);
    }

    /// <summary>
    /// Shows the entered name if it has been changed
    /// </summary>
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