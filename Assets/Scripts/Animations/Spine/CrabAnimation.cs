using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAnimation : SpineAnimation
{
    public enum States
    {
        Idle,
        TileTop,
        Up,
        Dive
    }

    [SerializeField] private List<Vector2> m_RandomPositions = new List<Vector2>();
    public List<Vector2> RandomPositions { get { return m_RandomPositions; } set { m_RandomPositions = value; } }

    public Vector2 RandomPosition
    {
        get { return m_RandomPositions[UnityEngine.Random.Range(0, m_RandomPositions.Count)]; }
    }
}
