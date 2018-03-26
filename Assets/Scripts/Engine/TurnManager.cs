using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager s_Instance;

    public delegate void FirstTurnStart();
    public static FirstTurnStart s_OnFirstTurnStart;

    public delegate void TurnEnd();
    public static TurnEnd s_OnTurnEnd;

    public delegate void TurnStart();
    public static TurnStart s_OnTurnStart;

    public delegate void TurnAction();
    public static TurnAction s_OnTurnAction;

    public delegate void GameEnd(Player playerThatWon);
    public static GameEnd s_OnGameEnd;


    [SerializeField] private GameObject m_TurnEndNotification;

    private Player m_CurrentPlayer;
    public Player CurrentPlayer { get { return m_CurrentPlayer; } }

    private int m_CurrentPlayerIndex;
    public int CurrentPlayerIndex
    {
        get { return m_CurrentPlayerIndex; }
        set { m_CurrentPlayerIndex = value; }
    }

    private void OnEnable()
    {
        s_OnTurnEnd += FinishTurn;
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
        if(s_OnFirstTurnStart != null) s_OnFirstTurnStart();
        if(s_OnTurnStart != null) s_OnTurnStart();
    }

    public void SetFirstTurn()
    {
        int randomPlayer = Random.Range(0, PlayersManager.s_Instance.Players.Count);
        m_CurrentPlayerIndex = randomPlayer;
        m_CurrentPlayer = PlayersManager.s_Instance.Players[m_CurrentPlayerIndex];
    }

    void FinishTurn()
    {
        m_TurnEndNotification.SetActive(true);
    }

    public void NextTurn()
    {
        if (m_CurrentPlayerIndex == PlayersManager.s_Instance.Players.Count - 1)
        {
            m_CurrentPlayerIndex = 0;
            m_CurrentPlayer = PlayersManager.s_Instance.Players[m_CurrentPlayerIndex];
        }
        else
        {
            m_CurrentPlayerIndex++;
            m_CurrentPlayer = PlayersManager.s_Instance.Players[m_CurrentPlayerIndex];
        }
        //CardPositionHolder.s_Instance.ReturnCardsToScreen();
        s_OnTurnStart();
        CardSelector.s_Instance.CanSelectCard = true;
    }

    private void OnDisable()
    {
        s_OnTurnEnd -= FinishTurn;
    }
}