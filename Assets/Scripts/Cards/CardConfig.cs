using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardConfig
{
    public static List<CardData> s_PathCards = new List<CardData>
    {
        new CardData(CardTypes.PATH_CARD, "Straight Horizontal Path", "Straight Horizontal Path", null, null, new PathCardData(false, false, true, true, true)),
        new CardData(CardTypes.PATH_CARD, "Straight Vertical Path", "Straight Vertical Path", null, null, new PathCardData(true, true, false, false, true)),
    };

    public static List<CardData> s_ActionCards = new List<CardData>
    {
        new CardData(CardTypes.ROTATE_PATH_CARD, "Rotate Path", "Rotate a single path", null, null, null),
        new CardData(CardTypes.SWAP_PATH_CARD, "Swap Path", "Swap the position of 2 paths that are next to each other", null, null, null),
        new CardData(CardTypes.ROADBLOCK_CARD, "Roadblock", "Block off a single path", null, null, null)
    };
}
