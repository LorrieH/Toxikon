using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Aspects
{
    Aspect_16_by_9,
    Aspect_16_by_10,
    Aspect_5_by_4,
    Aspect_4_by_3,
    Aspect_3_by_2,
    Aspect_18_5_by_9,
    Aspect_21_by_9,
    Aspect_17_by_10
}

[System.Serializable]
public struct CameraScaleAspect
{
    public Aspects Aspect;
    public float AspectValue;
    public float OrthographicSize;
}

[ExecuteInEditMode]
public class CameraScalerByAspect : MonoBehaviour
{
    [Tooltip("The offset of the Aspect Ratio check")]
    [SerializeField] private float m_Offset;
    [SerializeField] private List<CameraScaleAspect> m_CameraScaleAspects = new List<CameraScaleAspect>();

    private CameraScaleAspect m_CurrentAspect;

    private void Awake()
    {
        SetCameraSize();
    }

    private bool IsAspect(float aspectToCheck)
    {
        float aspect = Camera.main.aspect;
        if (aspect > (aspectToCheck - m_Offset) && aspect < (aspectToCheck + m_Offset))
            return true;
        else
            return false;
    }

    private void SetCameraSize()
    {
        float aspect = Camera.main.aspect;
        for (int i = 0; i < m_CameraScaleAspects.Count; i++)
        {
            if(IsAspect(m_CameraScaleAspects[i].AspectValue))
            {
                Camera.main.orthographicSize = m_CameraScaleAspects[i].OrthographicSize;
                m_CurrentAspect = m_CameraScaleAspects[i];
                return;
            }
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if(m_CurrentAspect.AspectValue != Camera.main.aspect)
            SetCameraSize();
    }
#endif
}
