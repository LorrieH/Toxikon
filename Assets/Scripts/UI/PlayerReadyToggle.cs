using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReadyToggle : MonoBehaviour
{
    private Toggle m_Toggle;

    private void Start()
    {
        m_Toggle = GetComponent<Toggle>();

        m_Toggle.onValueChanged.AddListener(delegate { ToggleChangedValue(); });
    }

    void ToggleChangedValue()
    {
        if (m_Toggle.isOn)
            ReadyCheck.s_Instance.ChangeAmountOfChecks(1);
        else
            ReadyCheck.s_Instance.ChangeAmountOfChecks(-1);

        Debug.Log("Checks needed: " + ReadyCheck.s_Instance.ChecksNeeded + " Current checks: " + ReadyCheck.s_Instance.CurrentChecks);
    }
}
