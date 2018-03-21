using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    BREAK_TILE,
    OCTOPUS,
    ROTATE_TILE
}

[System.Serializable]
public struct ActionData
{
    public AnimationType AnimationType;
    public SpineAnimation Animation;
}

public class ActionFXManager : MonoBehaviour
{
    public static ActionFXManager s_Instance;
    [SerializeField] private List<ActionData> m_Actions = new List<ActionData>();
    public List<ActionData> Actions { get { return m_Actions; } set { m_Actions = value; } }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(gameObject);
    }

    public void MoveTile(TileNode tile1, TileNode tile2)
    {

    }

    public void BreakTile(TileNode tile)
    {
        StartCoroutine(BreakTileAnimation(tile.GridPos));
    }

    public void BreakTile(int x, int y)
    {
        StartCoroutine(BreakTileAnimation(new Vector2(x, y)));
    }

    private IEnumerator BreakTileAnimation(Vector2 tilePosition)
    {
        SpineAnimation tileBreak = GetAnimationByType(AnimationType.BREAK_TILE);
        SpineAnimation octopus = GetAnimationByType(AnimationType.OCTOPUS);

        octopus.SetAnimation(OctopusAnimation.States.Down.ToString(), false);
        yield return new WaitForSeconds(2f);
        tileBreak.gameObject.transform.position = tilePosition;
        tileBreak.SetAnimation(TileBreakAnimation.States.animation.ToString(), false);
        tileBreak.SkeletonAnimation.state.End += delegate (Spine.TrackEntry entry) {
            if(entry.Animation.Name == TileBreakAnimation.States.animation.ToString())
            {
                Debug.Log(entry.Animation.Name + " has ended");
            }
        };
        yield return new WaitForSeconds(.5f); // WAIT FOR EVENT
        TileGrid.s_Instance.DestroyNode((int)tilePosition.x, (int)tilePosition.y);
        yield return new WaitForSeconds(1.5f);
        octopus.SetAnimation(OctopusAnimation.States.Up.ToString(), false);
        octopus.AddAnimation(OctopusAnimation.States.Idle.ToString(), true);
        CardPositionHolder.s_OnDiscardCard(CardSelector.s_Instance.SelectedCard);

    }

    public void RotateTile(TileNode tile)
    {

    }

    public SpineAnimation GetAnimationByType(AnimationType type)
    {
        try
        {
            return m_Actions.Find(item => item.AnimationType == type).Animation;
        }
        catch
        {
            return null;
        }
        
    }
}
