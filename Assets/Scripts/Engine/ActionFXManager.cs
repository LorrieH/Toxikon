using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFXManager : MonoBehaviour
{
    public static ActionFXManager s_Instance;

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

    public void SwapTiles(TileNode tile1, TileNode tile2)
    {

    }

    public void DestroyTile(TileNode tile)
    {

    }

    public void RotateTile(TileNode tile)
    {

    }
}
