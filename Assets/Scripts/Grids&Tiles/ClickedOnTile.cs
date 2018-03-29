using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickedOnTile : MonoBehaviour
{
    public delegate void TileClicked(Vector2 tilePosition);
    public static TileClicked s_OnTileClicked;

    TestPlaceCard TestPlacer = TestPlaceCard.s_Instance;
    TileGrid grid = TileGrid.s_Instance;

    public int TilePosX { get; set; }
    public int TilePosY { get; set; }

    void OnMouseDown()
    {
        if (!MenuManager.s_IsPaused)
        {
            if (CardSelector.s_Instance.SelectedCard == null) return;

            if (!CardSelector.s_Instance.CanSelectCard) return;

            if (s_OnTileClicked != null) s_OnTileClicked(new Vector2(TilePosX, TilePosY));
        }   
    }
}