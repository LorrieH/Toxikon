using UnityEngine;

public class Menu : MonoBehaviour
{
    private bool m_IsMenuOpen;
    public bool IsMenuOpen { get { return m_IsMenuOpen; } set { m_IsMenuOpen = value; } }

    public virtual void Show()
    {
        m_IsMenuOpen = true;
        SoundManager.s_Instance.PlaySound(SoundNames.BUTTON_CLICK);
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        m_IsMenuOpen = false;
        gameObject.SetActive(false);

        if (MenuManager.s_OnMenuClosed != null) MenuManager.s_OnMenuClosed(this);
    }
}
