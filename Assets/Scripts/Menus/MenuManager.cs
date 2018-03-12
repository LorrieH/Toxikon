using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager s_Instance;

    [SerializeField] private List<Menu> m_Menus = new List<Menu>();
    private Menu m_CurrentOpenMenu;

    public bool IsAnyMenuOpen
    {
        get { return (m_CurrentOpenMenu != null) ? true : false; }
    }

    private void Awake()
    {
        Init();
        MenuManager.s_Instance.ShowMenu<SettingsMenu>();
    }

    private void Init()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowMenu<T>() where T: Menu
    {
        
    }

    public void ShowMenu(string menuName)
    {
        Debug.Log("menuName parameter: " + menuName);
        for (int i = 0; i < m_Menus.Count; i++)
        {
            Debug.Log("Menu name: " + m_Menus[i].name);
            if (m_Menus[i].name == menuName)
            {
                Debug.Log("Opening menu: " + m_Menus[i].name);
                ShowMenu(m_Menus[i]);
            }
        }
    }

    public void ShowMenu(Menu menu)
    {
        if (IsAnyMenuOpen)
        {
            m_CurrentOpenMenu.Hide();
            m_CurrentOpenMenu = null;
        }
        menu.Show();
        m_CurrentOpenMenu = menu;
    }
}
