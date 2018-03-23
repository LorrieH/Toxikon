using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerSelection : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private List<PlayerPanel> m_PlayerPanels = new List<PlayerPanel>();
    [SerializeField] private Button m_StartButton;
    public static PlayerSelection s_Instance;

    private bool m_CanEdit;
    public bool CanEdit { get { return m_CanEdit; } set { m_CanEdit = value; } }

    [SerializeField]private List<Sprite> m_AvailableSprites = new List<Sprite>();
    [SerializeField] private List<Color> m_AvailableColors = new List<Color>();
    public List<Sprite> AvailableSprites
    {
        get { return m_AvailableSprites; }
        set { m_AvailableSprites = value; }
    }

    public List<Color> AvailableColors
    {
        get { return m_AvailableColors; }
        set { m_AvailableColors = value; }
    }

    private int m_PlayersReady;

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

    public void AddPlayers(int amountOfPlayers)
    {
        for (int i = 0; i < amountOfPlayers; i++)
        {
            GameObject playerObj = new GameObject();

            Player player = playerObj.AddComponent<Player>();

            player.name = "Player " + (i + 1);
            player.transform.SetParent(PlayersManager.s_Instance.transform);

            PlayersManager.s_Instance.AddPlayer(player);
        }

        ResetReadyPlayerCount();
        m_PlayerPanels[m_PlayersReady].gameObject.SetActive(true);
    }

    private void ResetReadyPlayerCount()
    {
        m_PlayersReady = 0;
    }

    private bool PlayersReady()
    {
        if (m_PlayersReady == PlayersManager.s_Instance.Players.Count -1){ return true; }
        else { return false; }
    }

    public void ChangeAmountOfChecks()
    {
        m_PlayersReady++;

        if (m_PlayersReady >= PlayersManager.s_Instance.Players.Count)
        {
            m_StartButton.gameObject.SetActive(true);
            m_StartButton.interactable = true;
        }
        else
        {
            m_PlayerPanels[m_PlayersReady - 1].gameObject.SetActive(false);
            m_PlayerPanels[m_PlayersReady].gameObject.SetActive(true);
        }
    }

    public void OnClickStartGame()
    {
        UpdatePlayerDataFromPanels();
        Sceneloader.s_Instance.LoadScene("GameScene");
    }

    private void UpdatePlayerDataFromPanels()
    {
        for (int i = 0; i < PlayersManager.s_Instance.Players.Count; i++)
            PlayersManager.s_Instance.Players[i].PlayerData = m_PlayerPanels[i].PlayerData;
    }
}
