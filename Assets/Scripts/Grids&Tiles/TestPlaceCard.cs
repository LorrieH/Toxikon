using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlaceCard : MonoBehaviour
{
#region instance
    public static TestPlaceCard s_Instance;

    private void Awake()
    {
        Init();
    }

    //make instance
    private void Init()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(gameObject);
    }
    //debug class
    #endregion

    public bool debugModus;

    public Vector2 selectOne;
    public Vector2 selectTwo;

    public int selectedCards = 0;

    public bool Destroy;
    public bool SwapCards;

    public bool Up;
    public bool Right;
    public bool Down;
    public bool Left;
    public bool Middle;
}