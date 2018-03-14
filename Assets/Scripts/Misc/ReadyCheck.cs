using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyCheck : MonoBehaviour
{
    public static  ReadyCheck s_Instance;

    [SerializeField] private Button m_StartButton;

    private int m_ChecksNeeded;
    public int ChecksNeeded { get { return m_ChecksNeeded; } }

    private int m_CurrentChecks;
    public int CurrentChecks { get { return m_CurrentChecks; } set { m_CurrentChecks = value; } }

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

    private void OnEnable()
    {
        m_ChecksNeeded = PlayersManager.s_Instance.PlayerObjects.Count;
    }

    public void ChangeAmountOfChecks(int amount)
    {
        m_CurrentChecks += amount;

        if (m_CurrentChecks >= m_ChecksNeeded)
            m_StartButton.interactable = true;
        else
            m_StartButton.interactable = false;
    }
}
