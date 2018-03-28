using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

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
    [SerializeField] private List<string> m_AvailableSkins = new List<string>();

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

    public List<string> AvailableSkins
    {
        get { return m_AvailableSkins; }
        set { m_AvailableSkins = value; }
    }

    private int m_PlayersReady;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Creates a instance of this object, if there is an instance already delete the new one
    /// </summary>
    private void Init()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Creates gameobjects and attaches the "Player" behaviour to them.
    /// Then they get added to the player manager's player list
    /// </summary>
    /// <param name="amountOfPlayers"></param>
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
        m_PlayerPanels[m_PlayersReady].gameObject.SetActive(true);
    }

    /// <summary>
    /// Checks the amount of ready players
    /// </summary>
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

    /// <summary>
    /// Starts the game
    /// </summary>
    public void OnClickStartGame()
    {
        UpdatePlayerDataFromPanels();
        Sceneloader.s_Instance.LoadScene("GameScene");
    }

    /// <summary>
    /// Transfers the data of created players to players in the PlayerManager
    /// </summary>
    private void UpdatePlayerDataFromPanels()
    {
        for (int i = 0; i < PlayersManager.s_Instance.Players.Count; i++)
            PlayersManager.s_Instance.Players[i].PlayerData = m_PlayerPanels[i].PlayerData;
    }
}
