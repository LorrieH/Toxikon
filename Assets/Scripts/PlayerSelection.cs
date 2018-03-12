using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public void AddPlayers(int amountOfPlayers)
    {
        for (int i = 0; i < amountOfPlayers; i++)
        {
            GameObject playerToAdd = new GameObject();
            playerToAdd.name = "Player " + (i + 1);
            playerToAdd.AddComponent<Player>();
        }
    }
}
