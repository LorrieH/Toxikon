using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraScreenShake : MonoBehaviour
{
    public static CameraScreenShake s_Instance;

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
    public void ShakeScreen(float duration = 1f)
    {
        Camera.main.transform.DOShakeRotation(duration, 0.2f, 25);
        Camera.main.transform.DOShakePosition(duration, 0.2f, 25);
    }
}
