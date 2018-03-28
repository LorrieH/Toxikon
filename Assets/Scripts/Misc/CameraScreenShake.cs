using UnityEngine;
using DG.Tweening;

public class CameraScreenShake : MonoBehaviour
{
    public static CameraScreenShake s_Instance;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Creates a instance of this object, if there is an instance already delete the new one
    /// </summary>
    private void Init()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Creates a screen shake effect
    /// </summary>
    /// <param name="duration">Duration of the shake, default is set to 1 second</param>
    public void ShakeScreen(float duration = 1f)
    {
        Camera.main.transform.DOShakeRotation(duration, 0.2f, 25);
        Camera.main.transform.DOShakePosition(duration, 0.2f, 25);
    }
}
