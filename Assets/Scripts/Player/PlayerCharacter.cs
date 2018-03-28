using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private Sprite m_CharacterSprite;
    public Sprite CharacterSprite { get { return m_CharacterSprite; } set { m_CharacterSprite = value; } }

    private string m_CharacterName;
    public string CharacterName { get { return m_CharacterName; } set { m_CharacterName = value; } }
}