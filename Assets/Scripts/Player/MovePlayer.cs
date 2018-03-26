using System.Collections;
using UnityEngine;
using DG.Tweening;
using Spine.Unity;

public static class MovePlayer
{

    public static void Move(Vector3[] path, GameObject player)
    {
        SkeletonAnimation anim = player.GetComponent<SkeletonAnimation>();
        anim.state.SetAnimation(0, "Walk", true);
        player.transform.DOPath(path, path.Length * 0.2f).OnComplete(()=> setState(anim));
    }

    private static void setState(SkeletonAnimation anim)
    {
        anim.state.SetAnimation(0, "Win", false);
    }

    public static IEnumerator MoveEnumerator(Vector3[] path, GameObject player)
    {
        for (int i = path.Length-1; i > -1; i--)
        {
            yield return player.transform.DOMove(path[i], 0.2f).WaitForCompletion();
        }
    }
}
