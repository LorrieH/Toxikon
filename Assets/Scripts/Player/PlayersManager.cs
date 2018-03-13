using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public static PlayersManager s_Instance;

    private List<GameObject> m_PlayerObjects = new List<GameObject>();
    public List<GameObject> PlayerObjects { get { return m_PlayerObjects; } }

    private List<Player> m_PlayerScripts = new List<Player>();
    public List<Player> PlayerScripts { get { return m_PlayerScripts; } }

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

    public void AddPlayer(GameObject playerToAdd)
    {
        m_PlayerObjects.Add(playerToAdd);
    }
}
