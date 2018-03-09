using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTypes
{
    PATH_CARD,
    ACTION_CARD
}

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

    private void Awake()
    {
        m_CardData = new CardData(CardTypes.ACTION_CARD, "Allahu Akbar", "Blows up a path.", null);
    }

    public virtual void UseCard()
    {
        //base of what should happen when a card gets used.
    }
}
