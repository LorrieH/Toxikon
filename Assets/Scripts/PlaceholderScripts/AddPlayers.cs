using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayers : MonoBehaviour
{
    public void AddIngamePlayers(int amountOfPlayer)
    {
        for (int i = 0; i < amountOfPlayer; i++)
        {
            GameObject playerToAdd = new GameObject();
            playerToAdd.name = "Player " + (i + 1);
            PlayersManager.s_Instance.AddPlayer(playerToAdd);
        }

        Debug.Log(PlayersManager.s_Instance.Players.Count);
    }
}
