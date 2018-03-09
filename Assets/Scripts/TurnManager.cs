using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public delegate void TurnEnd();
    public static TurnEnd s_OnTurnEnd;

    public delegate void TurnAction();
    public static TurnAction s_OnTurnAction;

    private List<GameObject> m_Players = new List<GameObject>();
    private int m_CurrentPlayerIndex;
    private GameObject m_CurrentPlayer;

    private void OnEnable()
    {
        s_OnTurnEnd += NextTurn;
    }

    private void Awake()
    {
        m_Players = PlayersManager.s_Instance.Players;
    }

    void SetFirstTurn()
    {
        int randomPlayer = Random.Range(0, m_Players.Count - 1);
        m_CurrentPlayerIndex = randomPlayer;
        m_CurrentPlayer = m_Players[m_CurrentPlayerIndex];
    }

    void NextTurn()
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
    }

    private void OnDisable()
    {
        s_OnTurnEnd -= NextTurn;
    }
}
