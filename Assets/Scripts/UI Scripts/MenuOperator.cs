using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuOperator : MonoBehaviour
{
    private MenuManager menuManager;
    public GameObject exitButton;
    public GameObject resumeButton;

    public void Setup(MenuManager menuManager)
    {
        this.menuManager = menuManager;
    }

    public void SetButtons()
    {
        SetExitButton();
        SetResumeButton();
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

    public void SetResumeButton()
    {
        UnityEngine.UI.Button btncomp = resumeButton.GetComponent<UnityEngine.UI.Button>();
        btncomp.onClick.RemoveAllListeners();
        btncomp.onClick.AddListener(() => menuManager.CloseMenu());

        resumeButton.SetActive(true);
    }
}
