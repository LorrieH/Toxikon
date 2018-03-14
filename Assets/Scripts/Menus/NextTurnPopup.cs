using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnPopup : MonoBehaviour
{
    private void OnEnable()
    {
        TurnManager.s_OnTurnAction += ShowPopup;
    }

    void ShowPopup()
    {

    }

    private void OnDisable()
    {
        TurnManager.s_OnTurnAction -= ShowPopup; 
    }
}
