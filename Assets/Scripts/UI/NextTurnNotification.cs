using UnityEngine;
using UnityEngine.UI;

public class NextTurnNotification : MonoBehaviour {

    [SerializeField] private Image _nextPlayerImage;
    [SerializeField] private Text _nextPlayerTurnText;
    private PlayerData m_NextPlayer;
	
	void OnEnable () {
        if(TurnManager.s_Instance.CurrentPlayerIndex == PlayersManager.s_Instance.Players.Count - 1)
        {
            ShowNextPlayer(PlayersManager.s_Instance.Players[0].PlayerData);
        }
        else
        {
            ShowNextPlayer(PlayersManager.s_Instance.Players[TurnManager.s_Instance.CurrentPlayerIndex + 1].PlayerData);
        }
	}

    void ShowNextPlayer(PlayerData nextPlayer)
    {
        Sprite spriteToLoad = Resources.Load<Sprite>("Characters/" + nextPlayer.AvatarImageName);
        _nextPlayerImage.sprite = Resources.Load<Sprite>("Characters/" + nextPlayer.AvatarImageName);
        _nextPlayerTurnText.text = nextPlayer.Name + "'s turn is up next!";
    }
}