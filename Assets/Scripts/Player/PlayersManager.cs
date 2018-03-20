using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public static PlayersManager s_Instance;

    private List<Player> m_Players = new List<Player>();
    public List<Player> Players { get { return m_Players; } }

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void AddPlayer(Player player)
    {
        m_Players.Add(player);
    }
}
