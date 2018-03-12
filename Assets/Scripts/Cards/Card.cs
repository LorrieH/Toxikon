using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum CardTypes
{
    PATH_CARD,
    ACTION_CARD
}

[System.Serializable]
public struct CardEditor
{
    public Text Name;
    public Text Description;
    public Image IconSmall;
    public Image IconLarge;
}

[System.Serializable]
public class CardData
{
    public CardData(CardTypes Type, string Name, string Description, Sprite Icon)
    {
        this.Type = Type;
        this.Name = Name;
        this.Description = Description;
        this.Icon = Icon;
    }

    public CardTypes Type;
    public string Name;
    public string Description;
    public Sprite Icon;
}

public class Card : MonoBehaviour
{
    protected CardData m_CardData;
    public CardData CardData { get { return m_CardData; } }
    [SerializeField]protected CardEditor m_CardEditor;

    private void Awake()
    {
        SetCardInfo();
    }

    protected void SetCardInfo()
    {
        m_CardEditor.Name.text = m_CardData.Name;
        m_CardEditor.Description.text = m_CardData.Description;
        m_CardEditor.IconSmall.sprite = m_CardData.Icon;
        m_CardEditor.IconLarge.sprite = m_CardData.Icon;
    }

    public void ToggleCardSelect()
    {
        CardSelector.s_OnToggleCardSelect(this, this.transform);
    }

    public virtual void UseCard()
    {
        //base of what should happen when a card gets used.
    }
}
