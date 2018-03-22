using UnityEngine;
using UnityEngine.UI;

public class NextTurnNotification : MonoBehaviour {

    [SerializeField] private Image _nextPlayerImage;
    [SerializeField] private Text _nextPlayerTurnText;
	
	void OnEnable () {
        if (TurnManager.s_Instance.CurrentPlayerIndex < PlayersManager.s_Instance.Players.Count)
        {
            _nextPlayerTurnText.text = PlayersManager.s_Instance.Players[TurnManager.s_Instance.CurrentPlayerIndex + 1].PlayerData.Name;
        }
        else
        {
            _nextPlayerTurnText.text = PlayersManager.s_Instance.Players[0].PlayerData.Name;
        }
	}
}