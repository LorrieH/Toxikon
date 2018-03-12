using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayers : MonoBehaviour
{
    public void AddIngamePlayers(int amountOfPlayer)
    {
        for (int i = 0; i < amountOfPlayer; i++)
        {
            GameObject playerObjectToAdd = new GameObject();
            playerObjectToAdd.AddComponent<Player>();
            Player playerToAdd = GetComponent<Player>();
            playerObjectToAdd.name = "Player " + (i + 1);
            PlayersManager.s_Instance.AddPlayerObject(playerObjectToAdd);
        }

        Debug.Log(PlayersManager.s_Instance.Players.Count);
    }
}
