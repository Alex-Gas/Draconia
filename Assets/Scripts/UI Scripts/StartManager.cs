using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : UIManager
{
    private GameObject uiContainer;
    public GameObject startUIpref;
    private GameObject startUI;
    private StartOperator startOperator;
    private CutsceneManager cutsceneManager;


    private void Start()
    {
        uiContainer = GameObject.Find("UI");
        cutsceneManager = GetComponent<CutsceneManager>();
    }

    protected override void LoadUI()
    {
        startUI = Instantiate(startUIpref, uiContainer.transform);
        startOperator = startUI.GetComponent<StartOperator>();
        startOperator.Setup(this);
    }


    protected override void OpenStartScreen()
    {
        startOperator.SetStartButton();
        startOperator.SetExitButton();
    }


    public void CloseStart()
    {
        Destroy(startUI);
    }

    public void StartIntro()
    {
        GameState.isPaused = false;
        isUIopen = false;


        cutsceneManager.InitiateCutscene("intro", 1f, 1f, 4f);
    }
}
