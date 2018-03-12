using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public static PlayersManager s_Instance;

    private List<GameObject> m_PlayerObjects = new List<GameObject>();
    private List<Player> m_Players = new List<Player>();
    public List<GameObject> PlayerObjects { get { return m_PlayerObjects; } }
    public List<Player> Players { get { return m_Players; } }

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void AddPlayerObject(GameObject playerObjectToAdd)
    {
        m_PlayerObjects.Add(playerObjectToAdd);
    }

    public void AddPlayer(Player playerToAdd)
    {
        m_Players.Add(playerToAdd);
    }
}
