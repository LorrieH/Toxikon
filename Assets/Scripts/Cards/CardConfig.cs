using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardConfig
{
    public static List<CardData> s_PathCards = new List<CardData>
    {
        new CardData(CardTypes.PATH_CARD, "Straight Path", "Miss me with that gay shit", null),
        new CardData(CardTypes.PATH_CARD, "Straight Path", "Miss me with that gay shit", null),
        new CardData(CardTypes.PATH_CARD, "Straight Path", "Miss me with that gay shit", null),
        new CardData(CardTypes.PATH_CARD, "Straight Path", "Miss me with that gay shit", null),
        new CardData(CardTypes.PATH_CARD, "Straight Path", "Miss me with that gay shit", null)
    };

    public static List<CardData> s_ActionCards = new List<CardData>
    {
        new CardData(CardTypes.ACTION_CARD, "Allahu Akbar", "Blows up a path", null)
    };
}
