using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartOperator : MonoBehaviour
{
    private StartManager startManager;
    public GameObject startButton;
    public GameObject exitButton;

    public void Setup(StartManager startManager)
    {
        this.startManager = startManager;
    }

    public void SetStartButton()
    {
        UnityEngine.UI.Button btncomp = startButton.GetComponent<UnityEngine.UI.Button>();
        btncomp.onClick.RemoveAllListeners();
        btncomp.onClick.AddListener(() =>
        {
            startManager.CloseStart();
            startManager.StartIntro();
        });
        startButton.SetActive(true);
    }


    public void SetExitButton()
    {
        UnityEngine.UI.Button btncomp = exitButton.GetComponent<UnityEngine.UI.Button>();
        btncomp.onClick.RemoveAllListeners();
        btncomp.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        exitButton.SetActive(true);
    }

}
