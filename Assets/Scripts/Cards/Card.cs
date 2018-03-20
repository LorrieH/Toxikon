using UnityEngine;
using UnityEngine.UI;

public enum CardTypes
{
    PATH_CARD,
    ROADBLOCK_CARD,
    SWAP_PATH_CARD,
    ROTATE_PATH_CARD,
    DESTROY_CARD
}

[System.Serializable]
public struct CardEditor
{
    public Text Name;
    public Text Description;
    public Image CardImage;
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
    public CardData(CardTypes Type, string Name, string Description, Sprite CardSprite, PathCardData PathData)
    {
        this.Type = Type;
        this.Name = Name;
        this.Description = Description;
        this.CardSprite = CardSprite;
        this.PathData = PathData;
    }

    public CardTypes Type;
    public string Name;
    public string Description;
    public Sprite CardSprite;
    public PathCardData PathData;
}

public class Card : MonoBehaviour
{
    protected CardData m_CardData;
    public CardData CardData { get { return m_CardData; } set { m_CardData = value; } }
    [SerializeField]protected CardEditor m_CardEditor;

    private int m_IndexInHand;
    public int IndexInHand { get { return m_IndexInHand; } }

    private void Awake()
    {
        m_IndexInHand = transform.GetSiblingIndex(); // Sets the hierarchy layer
    }

    public void SetCardInfo()
    {
        //Shows the cards info
        m_CardEditor.Name.text = m_CardData.Name;
        m_CardEditor.Description.text = m_CardData.Description;
        m_CardEditor.CardImage.sprite = m_CardData.CardSprite;
    }

    public void ToggleCardSelect()
    {
        CardSelector.s_OnToggleCardSelect(this,m_IndexInHand);
    }

    public void UseCard()
    {
        //what should happen when a card gets used.
    }
}
