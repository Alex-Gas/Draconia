using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EndOperator : MonoBehaviour
{
    private EndManager endManager;
    public GameObject exitButton;

    public void Setup(EndManager endManager)
    {
        this.endManager = endManager;
    }

    public void SetExitButton()
    {
        UnityEngine.UI.Button btncomp = exitButton.GetComponent<UnityEngine.UI.Button>();
        btncomp.onClick.RemoveAllListeners();
        btncomp.onClick.AddListener(() =>
        {
            if (EditorApplication.isPlaying == true)
            {
                EditorApplication.isPlaying = false;
            }
            else
            {
                Application.Quit();
            }
        });
        exitButton.SetActive(true);
    }

}
