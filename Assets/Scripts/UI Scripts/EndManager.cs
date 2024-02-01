using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : UIManager
{
    private GameObject uiContainer;
    public GameObject endUIpref;
    private GameObject endUI;
    private EndOperator endOperator;


    private void Start()
    {
        uiContainer = GameObject.Find("UI");
    }

    protected override void LoadUI()
    {
        endUI = Instantiate(endUIpref, uiContainer.transform);
        endOperator = endUI.GetComponent<EndOperator>();
        endOperator.Setup(this);
    }

    protected override void OpenEndScreen()
    {
        endOperator.SetExitButton();
        endOperator.SetText();
    }
}
