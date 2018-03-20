using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerSelection : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private List<PlayerPanel> m_PlayerPanels = new List<PlayerPanel>();
    [Header("Sprites (Remove soon)")]
    [SerializeField]private List<Sprite> m_PossibleCharacterSprites = new List<Sprite>();
    [SerializeField] private Button m_StartButton;
    public static PlayerSelection s_Instance;
    private Sequence m_ScalePlayerPanel;

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
        TransitionInPlayerPanels();
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

    private void TransitionInPlayerPanels()
    {
        m_ScalePlayerPanel = DOTween.Sequence();

        RectTransform rt = m_PlayerPanels[m_PlayersReady].GetComponent<RectTransform>();

        rt.anchoredPosition = new Vector2(1920, 0);

        m_PlayerPanels[m_PlayersReady].gameObject.SetActive(true);

        m_ScalePlayerPanel.AppendInterval(0.1f);
        m_ScalePlayerPanel.Append(rt.DOAnchorPosX(0, 1f).SetEase(Ease.OutExpo));
    }

    public void TransitionOutPlayerPanels()
    {
        if (!PlayersReady())
        {
            m_PlayerPanels[m_PlayersReady].gameObject.SetActive(true);
            RectTransform rt = m_PlayerPanels[m_PlayersReady].GetComponent<RectTransform>();

            rt.DOAnchorPosX(-1920, 1f).SetEase(Ease.InBack).OnComplete(TransitionInPlayerPanels);
        }
    }

    public Sprite GetRandomCharacter()
    {
        Sprite character;
        int randomSprite = Random.Range(0, m_PossibleCharacterSprites.Count);
        character = m_PossibleCharacterSprites[randomSprite];
        m_PossibleCharacterSprites.RemoveAt(randomSprite);

        return character;
    }

    public void ChangeAmountOfChecks(int amount)
    {
        m_PlayersReady += amount;

        if (m_PlayersReady >= PlayersManager.s_Instance.Players.Count)
        {
            m_StartButton.gameObject.SetActive(true);
            m_StartButton.interactable = true;
        }
        else
            m_StartButton.interactable = false;
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
