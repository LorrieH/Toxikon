using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    BREAK_TILE,
    OCTOPUS,
    ROTATE_TILE,
    CRAB,
    PIRANHA
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

    public static Action s_OnTileAnimationCompleted;

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

    private int RandomGridPos()
    {
        return UnityEngine.Random.Range(1, 10);
    }

    public void MoveTile(int movingX, int movingY, int targetX, int targetY)
    {
        StartCoroutine(MoveTileAnimation(new Vector2(movingX, movingY), new Vector2(targetX, targetY)));
    }

    private IEnumerator MoveTileAnimation(Vector2 movingTilePosition, Vector2 targetTilePosition)
    {
        CrabAnimation crab = GetAnimationByType(AnimationType.CRAB) as CrabAnimation;
        TileBools bools = TileGrid.s_Instance.GetTileNode((int)movingTilePosition.x, (int)movingTilePosition.y).Bools;

        crab.SetAnimation(CrabAnimation.States.Dive.ToString(), false);
        yield return new WaitForSeconds(2f);
        crab.transform.position = movingTilePosition;
        crab.SetAnimation(CrabAnimation.States.TileTop.ToString(), false);
        CameraScreenShake.s_Instance.ShakeScreen(1f);
        TileGrid.s_Instance.DestroyNode((int)movingTilePosition.x, (int)movingTilePosition.y);
        crab.AddAnimation(CrabAnimation.States.Idle.ToString(), true);
        yield return new WaitForSeconds(1.3f);
        crab.SetAnimation(CrabAnimation.States.Dive.ToString(), false);
        yield return new WaitForSeconds(2f);
        TileGrid.s_Instance.PlaceNewCard((int)targetTilePosition.x, (int)targetTilePosition.y, bools.Up, bools.Right, bools.Down, bools.Left, bools.Middle);
        crab.transform.position = crab.RandomPosition;
        crab.SetAnimation(CrabAnimation.States.Up.ToString(), false);
        crab.AddAnimation(CrabAnimation.States.Idle.ToString(), true);
        TurnManager.s_OnTurnEnd();
    }

    #region Break Tile

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
        TileBreakAnimation tileBreak = GetAnimationByType(AnimationType.BREAK_TILE) as TileBreakAnimation;
        OctopusAnimation octopus = GetAnimationByType(AnimationType.OCTOPUS) as OctopusAnimation;

        octopus.SetAnimation(OctopusAnimation.States.Down.ToString(), false);
        yield return new WaitForSeconds(2f);
        TileClickManager.s_Instance.HideIndicator();
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
        TurnManager.s_OnTurnEnd();
    }

    #endregion

    public IEnumerator RotateTileAnimation(TileNode tile, Vector2 tilePosition)
    {
        PiranhaAnimation piranha = GetAnimationByType(AnimationType.PIRANHA) as PiranhaAnimation;

        piranha.SetAnimation(PiranhaAnimation.States.Submerge.ToString(), false);
        yield return new WaitForSeconds(0.4f);
        piranha.transform.position = tilePosition;
        piranha.SetAnimation(PiranhaAnimation.States.Emerge.ToString(), false);
        piranha.AddAnimation(PiranhaAnimation.States.Idle.ToString(), false);
        yield return new WaitForSeconds(4.067f);
        piranha.SetAnimation(PiranhaAnimation.States.Spin_Start.ToString(), false);
        piranha.AddAnimation(PiranhaAnimation.States.Spin_Repeatable.ToString(), false);
        piranha.AddAnimation(PiranhaAnimation.States.Spin_End.ToString(), false);
        yield return new WaitForSeconds(1.3f);
        piranha.SetAnimation(PiranhaAnimation.States.Submerge.ToString(), false);
        yield return new WaitForSeconds(0.4f);
        piranha.SetAnimation(PiranhaAnimation.States.Emerge.ToString(), false);
        piranha.AddAnimation(PiranhaAnimation.States.Idle.ToString(), true);
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
