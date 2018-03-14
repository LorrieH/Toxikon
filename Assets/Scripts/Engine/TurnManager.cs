using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager s_Instance;

    public delegate void TurnEnd();
    public static TurnEnd s_OnTurnEnd;

    public delegate void TurnStart();
    public static TurnStart s_OnTurnStart;

    public delegate void TurnAction();
    public static TurnAction s_OnTurnAction;

    private List<Player> m_Players = new List<Player>();
    public List<Player> Players { get { return m_Players; } }

    private Player m_CurrentPlayer;
    public Player CurrentPlayer { get { return m_CurrentPlayer; } }

    private int m_CurrentPlayerIndex;

    private void OnEnable()
    {
        s_OnTurnEnd += NextTurn;
    }

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

    private void Start()
    {
        m_Players = PlayersManager.s_Instance.PlayerScripts;
        SetFirstTurn();
        s_OnTurnStart();
    }

    public void SetFirstTurn()
    {
        int randomPlayer = Random.Range(0, m_Players.Count);
        m_CurrentPlayerIndex = randomPlayer;
        m_CurrentPlayer = m_Players[m_CurrentPlayerIndex];
    }

    public void NextTurn()
    {
        if(m_CurrentPlayerIndex == m_Players.Count - 1)
        {
            m_CurrentPlayerIndex = 0;
            m_CurrentPlayer = m_Players[m_CurrentPlayerIndex];
        }
        else
        {
            m_CurrentPlayerIndex++;
            m_CurrentPlayer = m_Players[m_CurrentPlayerIndex];
        }

        s_OnTurnStart();
    }

    private void OnDisable()
    {
        s_OnTurnEnd -= NextTurn;
    }
}
