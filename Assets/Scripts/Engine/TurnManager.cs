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
        SetFirstTurn();
        s_OnTurnStart();
    }

    public void SetFirstTurn()
    {
        int randomPlayer = Random.Range(0, PlayersManager.s_Instance.Players.Count);
        m_CurrentPlayerIndex = randomPlayer;
        m_CurrentPlayer = PlayersManager.s_Instance.Players[m_CurrentPlayerIndex];
    }

    public void NextTurn()
    {
        if(m_CurrentPlayerIndex == PlayersManager.s_Instance.Players.Count - 1)
        {
            m_CurrentPlayerIndex = 0;
            m_CurrentPlayer = PlayersManager.s_Instance.Players[m_CurrentPlayerIndex];
        }
        else
        {
            m_CurrentPlayerIndex++;
            m_CurrentPlayer = PlayersManager.s_Instance.Players[m_CurrentPlayerIndex];
        }

        s_OnTurnStart();
    }

    private void OnDisable()
    {
        s_OnTurnEnd -= NextTurn;
    }
}
