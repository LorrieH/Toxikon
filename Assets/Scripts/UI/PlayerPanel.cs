using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    private PlayerData m_PlayerData;
    public PlayerData PlayerData { get { return m_PlayerData; } set { m_PlayerData = value; } }

    [SerializeField] private Text m_PlayerName;
    [SerializeField] private Image m_PlayerAvatar;
    [Space(15f)]
    [SerializeField] private InputField m_InputField;
    [SerializeField] private Toggle m_Toggle;

    private void Awake()
    {
        m_InputField.onEndEdit.AddListener(delegate { OnInputFieldValueChanged(); });
        m_Toggle.onValueChanged.AddListener(delegate { OnToggleValueChanged(); });

        m_PlayerAvatar.sprite = PlayerSelection.s_Instance.GetRandomCharacter();
    }

    private void OnToggleValueChanged()
    {
        m_PlayerData.Name = m_PlayerName.text;
        m_PlayerData.AvatarImageName = m_PlayerAvatar.sprite.name;
        PlayerSelection.s_Instance.TransitionOutPlayerPanels();
        PlayerSelection.s_Instance.ChangeAmountOfChecks(m_Toggle.isOn ? 1 : -1);
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