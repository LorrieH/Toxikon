using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardConfig
{
    public static List<CardData> s_PathCards = new List<CardData>
    {
        new CardData(CardTypes.PATH_CARD, "",CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.CROSSROAD), new PathCardData(true,true,true,true,true)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.STRAIGHT_HORIZONTAL), new PathCardData(false, false, true, true, true)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.STRAIGHT_VERTICAL), new PathCardData(true, true, false, false, true)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.CORNER_UP_LEFT), new PathCardData(true, false, true, false, true)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.CORNER_DOWN_LEFT), new PathCardData(false, true, true, false, true)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.CORNER_UP_RIGHT), new PathCardData(true, false, false, true, true)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.CORNER_DOWN_RIGHT), new PathCardData(false, true, false, true, true)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.T_SPLIT_UP), new PathCardData(true,false,true,true,true)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.T_SPLIT_DOWN), new PathCardData(false,true,true,true,true)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.T_SPLIT_LEFT), new PathCardData(true,true,true,false,true)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.T_SPLIT_RIGHT), new PathCardData(true,true,false,true,true)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.BLOCK_UP), new PathCardData(true,false,false,false,false)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.BLOCK_DOWN), new PathCardData(false,true,false,false,false)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.BLOCK_LEFT), new PathCardData(false,false,true,false,false)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.BLOCK_RIGHT), new PathCardData(false,false,false,true,false)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.BLOCK_LEFT_RIGHT), new PathCardData(false,false,true,true,false)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.BLOCK_UP_DOWN), new PathCardData(true,true,false,false,false)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.BLOCK_RIGHT_LEFT_DOWN), new PathCardData(false,true,true,true,false)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.BLOCK_UP_RIGHT_LEFT), new PathCardData(true,false,true,true,false)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.BLOCK_UP_LEFT_DOWN), new PathCardData(true,false,true,false,false)),
        new CardData(CardTypes.PATH_CARD, "", CardImageLoader.s_CardSprite(CardStrings.PATH,CardStrings.BLOCK_UP_RIGHT_DOWN), new PathCardData(true,true,false,true,false))
    };

    public static List<CardData> s_ActionCards = new List<CardData>
    {
        new CardData(CardTypes.DESTROY_PATH_CARD, "Destroy", CardImageLoader.s_CardSprite(CardStrings.ACTION, CardStrings.DESTROY_PATH), null),
        new CardData(CardTypes.MOVE_PATH_CARD, "Move", CardImageLoader.s_CardSprite(CardStrings.ACTION, CardStrings.MOVE_PATH), null)
        //new CardData(CardTypes.ROTATE_PATH_CARD, "Rotate", "Rotate a single path", null, null, null),
        //new CardData(CardTypes.SWAP_PATH_CARD, "Swap", "Swap the position of 2 paths that are next to each other", null, null, null),
        //new CardData(CardTypes.ROADBLOCK_CARD, "Roadblock", "Block off a single path", null, null, null)
    };
}