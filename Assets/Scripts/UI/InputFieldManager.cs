using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour
{
    [SerializeField] private Text m_PlayerName;
    private InputField m_IF;
    private Coroutine m_FocusRoutine;

    private void OnEnable()
    {
        m_IF = GetComponent<InputField>();
        m_IF.onValueChanged.AddListener(delegate { OnValueChange(); });
        m_FocusRoutine = StartCoroutine(CheckFocusRoutine());
    }

    IEnumerator CheckFocusRoutine()
    {
        if (!m_IF.isFocused)
        {
            m_IF.text = string.Empty;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CheckFocusRoutine());
    }

    void OnValueChange()
    {
        if (m_IF.isFocused)
        {
            m_PlayerName.text = m_IF.text;
        }
    }
}