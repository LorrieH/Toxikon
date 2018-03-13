using UnityEngine;

public class AnimationPlayer : MonoBehaviour {

    private Animator _animator;

	public void PlayTriggerAnimation(string animationString)
    {
        _animator.SetTrigger(animationString);
    }

    public void SetAnimationBool(string animationString, bool boolValue)
    {
        _animator.SetBool(animationString, boolValue);
    }
}