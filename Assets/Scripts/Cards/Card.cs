using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum CardTypes
{
    PATH_CARD,
    ROADBLOCK_CARD,
    SWAP_PATH_CARD,
    ROTATE_PATH_CARD
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
public class PathCardData
{
    public PathCardData(bool Up, bool Down, bool Left, bool Right, bool Middle)
    {
        this.Up = Up;
        this.Down = Down;
        this.Left = Left;
        this.Right = Right;
        this.Middle = Middle;
    }

    public bool Up { get; set; }
    public bool Down { get; set; }
    public bool Left { get; set; }
    public bool Right { get; set; }
    public bool Middle { get; set; }
}

[System.Serializable]
public class CardData
{
    public CardData(CardTypes Type, string Name, string Description, Sprite SmallIcon, Sprite LargeIcon, PathCardData PathData)
    {
        this.Type = Type;
        this.Name = Name;
        this.Description = Description;
        this.SmallIcon = SmallIcon;
        this.LargeIcon = LargeIcon;
        this.PathData = PathData;
    }

    public CardTypes Type;
    public string Name;
    public string Description;
    public Sprite SmallIcon;
    public Sprite LargeIcon;
    public PathCardData PathData;
}

public class Card : MonoBehaviour
{
    protected CardData m_CardData;
    public CardData CardData { get { return m_CardData; } set { m_CardData = value; } }
    [SerializeField]protected CardEditor m_CardEditor;

    private int m_IndexInHand;

    public void SetCardInfo()
    {
        //Shows the cards info
        m_IndexInHand = transform.GetSiblingIndex();
        m_CardEditor.Name.text = m_CardData.Name;
        m_CardEditor.Description.text = m_CardData.Description;
        m_CardEditor.IconSmall.sprite = m_CardData.SmallIcon;
        m_CardEditor.IconLarge.sprite = m_CardData.LargeIcon;
    }

    public void ToggleCardSelect()
    {
        CardSelector.s_OnToggleCardSelect(m_CardData, this.transform,m_IndexInHand);
    }

    public void UseCard()
    {
        //what should happen when a card gets used.
        switch (CardData.Type)
        {
            case CardTypes.PATH_CARD:

                break;
        }
    }
}
