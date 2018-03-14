using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField]private List<Sprite> m_PossibleCharacterSprites = new List<Sprite>();

    public void AddPlayers(int amountOfPlayers)
    {
        Transform parent = PlayersManager.s_Instance.transform;

        for (int i = 0; i < amountOfPlayers; i++)
        {
            GameObject playerObjectToAdd = new GameObject();

            Player playerScriptToAdd = playerObjectToAdd.AddComponent<Player>();

            PlayerCharacter playerCharacterToAdd = playerObjectToAdd.AddComponent<PlayerCharacter>();
            SetRandomSprite(playerCharacterToAdd);

            playerObjectToAdd.name = "Player " + (i + 1);
            playerObjectToAdd.transform.SetParent(parent);

            PlayersManager.s_Instance.PlayerObjects.Add(playerObjectToAdd);
            PlayersManager.s_Instance.PlayerScripts.Add(playerScriptToAdd);
        }

        Debug.Log("Player Objects:" + PlayersManager.s_Instance.PlayerObjects.Count + " Player Scripts: " + PlayersManager.s_Instance.PlayerScripts.Count);
    }

    void SetRandomSprite(PlayerCharacter character)
    {
        int randomSprite = Random.Range(0, m_PossibleCharacterSprites.Count);
        character.CharacterSprite = m_PossibleCharacterSprites[randomSprite];

        m_PossibleCharacterSprites.RemoveAt(randomSprite);
    }
}
