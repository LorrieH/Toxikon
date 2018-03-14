using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    private PlayerCharacter m_Character;
    public PlayerCharacter Character { get { return m_Character; } set { m_Character = value; } }

    [SerializeField] private Image PlayerIcon;

    public void SetCharacterName(string name)
    {
        m_Character.CharacterName = name;
    }

    private void Awake()
    {
        PlayerIcon.sprite = m_Character.CharacterSprite;
    }
}