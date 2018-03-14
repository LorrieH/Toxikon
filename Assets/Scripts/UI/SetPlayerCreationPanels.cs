using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerCreationPanels : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_PlayerPanels = new List<GameObject>();

    private void OnEnable()
    {
        SetPanels();
    }

    void SetPanels()
    {
        for (int i = 0; i < PlayersManager.s_Instance.PlayerObjects.Count; i++)
        {
            m_PlayerPanels[i].GetComponent<PlayerPanel>().Character = PlayersManager.s_Instance.PlayerObjects[i].GetComponent<PlayerCharacter>();

            m_PlayerPanels[i].SetActive(true);
        }
    }
}
