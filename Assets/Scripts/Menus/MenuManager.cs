﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MenuNames
{
    public const string SETTINGS_MENU = "Settings Menu";
    public const string VICTORY_POPUP = "Victory Popup";
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager s_Instance;

    public delegate void MenuOpened(Menu menu);
    public static MenuOpened s_OnMenuOpened;

    public delegate void MenuClosed(Menu menu);
    public static MenuClosed s_OnMenuClosed;

    [SerializeField] private List<Menu> m_Menus = new List<Menu>();
    private Menu m_CurrentOpenMenu;

    public bool IsAnyMenuOpen
    {
        get { return (m_CurrentOpenMenu != null) ? true : false; }
    }

    private void Awake()
    {
        Init();
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

    public void ShowMenu(string menuName)
    {
        for (int i = 0; i < m_Menus.Count; i++)
        {
            if (m_Menus[i].name == menuName)
                ShowMenu(m_Menus[i]);
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

        if (s_OnMenuOpened != null) s_OnMenuOpened(menu);
    }
}