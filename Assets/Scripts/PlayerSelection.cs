using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public void AddPlayers(int amountOfPlayers)
    {
        Transform parent = GameObject.Find("PlayerManager").transform;

        for (int i = 0; i < amountOfPlayers; i++)
        {
            GameObject playerObjectToAdd = new GameObject();
            Player playerScriptToAdd = playerObjectToAdd.AddComponent<Player>();
            playerObjectToAdd.name = "Player " + (i + 1);
            playerObjectToAdd.transform.SetParent(parent);

            PlayersManager.s_Instance.PlayerObjects.Add(playerObjectToAdd);
            PlayersManager.s_Instance.PlayerScripts.Add(playerScriptToAdd);
        }

        Debug.Log("Player Objects:" + PlayersManager.s_Instance.PlayerObjects.Count + " Player Scripts: " + PlayersManager.s_Instance.PlayerScripts.Count);
        Sceneloader.s_Instance.LoadScene("Lorenzo");
    }
}
