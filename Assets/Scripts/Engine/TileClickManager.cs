using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClickManager : MonoBehaviour
{
    public static TileClickManager s_Instance;

    private List<Vector2> m_ClickedTilePositions = new List<Vector2>();
    public List<Vector2> ClickedTilePositions { get { return m_ClickedTilePositions; } set { m_ClickedTilePositions = value; } }

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
}
