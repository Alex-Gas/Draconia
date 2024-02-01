using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : UIManager
{
    private GameObject uiContainer;
    public GameObject menuUIpref;
    private GameObject menuUI;
    private MenuOperator menuOperator;

    public static bool isMenuOpen;


    private void Start()
    {
        uiContainer = GameObject.Find("UI");
    }

    protected override void LoadUI()
    {
        menuUI = Instantiate(menuUIpref, uiContainer.transform);
        menuOperator = menuUI.GetComponent<MenuOperator>();
        menuOperator.Setup(this);
    }


    protected override void OpenMenuScreen()
    {
        menuOperator.SetButtons();
    }


    public void ToggleMenu()
    {
        if (!isMenuOpen)
        {
            InitiateMenu();
        }
        else if (isMenuOpen)
        {
            CloseMenu();
        }
    }

    public void CloseMenu()
    {
        isUIopen = false;
        isMenuOpen = false;
        Destroy(menuUI);
        GameState.isPaused = false;
    }
}
