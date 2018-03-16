using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardConfig
{
    public static List<CardData> s_PathCards = new List<CardData>
    {
        new CardData(CardTypes.PATH_CARD, "Crossroad", "Creates a Crossroad Path", null, null, new PathCardData(true,true,true,true,true)),
        new CardData(CardTypes.PATH_CARD, "Straight Horizontal Path", "Straight Horizontal Path", null, null, new PathCardData(false, false, true, true, true)),
        new CardData(CardTypes.PATH_CARD, "Straight Vertical Path", "Straight Vertical Path", null, null, new PathCardData(true, true, false, false, true)),
        new CardData(CardTypes.PATH_CARD, "Left Up Corner Path", "Creates a Left Up Corner Path", null, null, new PathCardData(true, false, true, false, true)),
        new CardData(CardTypes.PATH_CARD, "Left Down Corner Path", "Creates a Left Down Corner Path", null, null, new PathCardData(false, true, true, false, true)),
        new CardData(CardTypes.PATH_CARD, "Right Up Corner Path", "Creates a Right  Up Corner Path", null, null, new PathCardData(true, false, false, true, true)),
        new CardData(CardTypes.PATH_CARD, "Right Down Corner Path", "Creates a Right Up Corner Path", null, null, new PathCardData(false, true, false, true, true)),
        new CardData(CardTypes.PATH_CARD, "T-Split Up", "Creates a T Split Up Path", null, null, new PathCardData(true,false,true,true,true)),
        new CardData(CardTypes.PATH_CARD, "T-Split Down", "Creates a T Split Down Path", null, null, new PathCardData(false,true,true,true,true)),
        new CardData(CardTypes.PATH_CARD, "T-Split Left", "Creates a T Split Left Path", null, null, new PathCardData(true,true,true,false,true)),
        new CardData(CardTypes.PATH_CARD, "T-Split Right", "Creates a T Split Right Path", null, null, new PathCardData(true,true,false,true,true)),
    };

    public static List<CardData> s_ActionCards = new List<CardData>
    {
        //new CardData(CardTypes.ROTATE_PATH_CARD, "Rotate Path", "Rotate a single path", null, null, null),
        //new CardData(CardTypes.SWAP_PATH_CARD, "Swap Path", "Swap the position of 2 paths that are next to each other", null, null, null),
        //new CardData(CardTypes.ROADBLOCK_CARD, "Roadblock", "Block off a single path", null, null, null)
    };
}
