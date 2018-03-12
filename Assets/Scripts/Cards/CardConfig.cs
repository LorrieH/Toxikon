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
        new CardData(CardTypes.ROTATE_PATH_CARD, "Rotate Path", "Rotate a single path", null),
        new CardData(CardTypes.SWAP_PATH_CARD, "Swap Path", "Swap the position of 2 paths that are next to each other", null),
        new CardData(CardTypes.ROADBLOCK_CARD, "Roadblock", "Block off a single path", null)
    };
}
