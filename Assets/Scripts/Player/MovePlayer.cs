using System.Collections;
using UnityEngine;
using DG.Tweening;
using Spine.Unity;

public static class MovePlayer
{
    /// <summary>
    /// Moves a player along a path and plays the proper animation
    /// </summary>
    /// <param name="path">Path to follow</param>
    /// <param name="player">Object to move</param>
    public static void Move(Vector3[] path, GameObject player)
    {
        SkeletonAnimation anim = player.GetComponent<SkeletonAnimation>();
        anim.state.SetAnimation(0, "Walk", true);
        player.transform.DOPath(path, path.Length * 0.2f).SetEase(Ease.Linear).OnComplete(()=> setState(anim));
    }

    private static void setState(SkeletonAnimation anim)
    { 
        anim.state.SetAnimation(0, "Win", false).Complete += delegate
        {
            MenuManager.s_Instance.ShowMenu(MenuNames.VICTORY_POPUP);
        };
    }

    /// <summary>
    /// Moves a player over a path
    /// </summary>
    /// <param name="path">A path made up of Vector 3 positions</param>
    /// <param name="player">The gameobject linked to the player</param>
    /// <returns></returns>
    public static IEnumerator MoveEnumerator(Vector3[] path, GameObject player)
    {
        for (int i = path.Length-1; i > -1; i--)
        {
            yield return player.transform.DOMove(path[i], 0.2f).WaitForCompletion();
        }
    }
}
