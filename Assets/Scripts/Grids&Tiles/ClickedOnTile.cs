using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickedOnTile : MonoBehaviour {

    //click

    public TileGrid Grid { get; set; }

    public int TilePosX { get; set; }
    public int TilePosY { get; set; }

    void OnMouseDown()
    {
        if (CardSelector.s_Instance.SelectedCard == null) return;

        switch (CardSelector.s_Instance.SelectedCard.CardData.Type) {

            case CardTypes.PATH_CARD:
                PathCardData pathData = CardSelector.s_Instance.SelectedCard.CardData.PathData;
                if (Grid.PlaceNewCard(TilePosX, TilePosY, pathData.Up, pathData.Right, pathData.Down, pathData.Left, pathData.Middle))
                {
                    CardPositionHolder.s_OnDiscardCard(CardSelector.s_Instance.SelectedCard);
                }
                break;
            case CardTypes.ROTATE_PATH_CARD:

                break;
            case CardTypes.SWAP_PATH_CARD:
                break;
        }
    }
}