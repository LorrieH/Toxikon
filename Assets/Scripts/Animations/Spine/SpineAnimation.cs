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

    #region Public Functions

    /// <summary>
    /// Overwrites or sets the animation of the Spine Skeleton Animation
    /// </summary>
    /// <param name="animationName">Name of the animation</param>
    /// <param name="loop">Loop the animation?</param>
    public void SetAnimation(string animationName, bool loop)
    {
        m_SkeletonAnimation.state.SetAnimation(0, animationName, loop);
    }

    /// <summary>
    /// Adds an animation to the Skeleton Animations Queue
    /// </summary>
    /// <param name="animationName">Name of the animation</param>
    /// <param name="loop">Loop the animation?</param>
    /// <param name="delay">Delay between current and this animation</param>
    public void AddAnimation(string animationName, bool loop, float delay = 0)
    {
        m_SkeletonAnimation.state.AddAnimation(0, animationName, loop, delay);
    }

    #endregion
}
