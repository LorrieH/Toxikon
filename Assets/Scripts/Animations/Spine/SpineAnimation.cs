using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(SkeletonAnimation))]
public class SpineAnimation : MonoBehaviour
{
    private SkeletonAnimation m_SkeletonAnimation;
    public SkeletonAnimation SkeletonAnimation { get { return m_SkeletonAnimation; } set { m_SkeletonAnimation = value; } }

    private void Awake()
    {
        m_SkeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    public void SetAnimation(string animationName, bool loop)
    {
        m_SkeletonAnimation.state.SetAnimation(0, animationName, loop);
    }

    public void AddAnimation(string animationName, bool loop, float delay = 0)
    {
        m_SkeletonAnimation.state.AddAnimation(0, animationName, loop, delay);
    }
}
