using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class EndOperator : MonoBehaviour
{
    private EndManager endManager;
    public GameObject exitButton;
    public TextMeshProUGUI textMesh;
    public TextMeshProUGUI textMesh2;
    private string ending;

    public void Setup(EndManager endManager)
    {
        this.endManager = endManager;

        if (GameState.isWorstEnding) { ending = "the Worst Ending. "; }
        else if (GameState.isEvilEnding) { ending = "an Evil Ending. "; }
        else if(GameState.isBadEnding) { ending = "a Bad Ending. "; }
        else if(GameState.isNeutralEnding) { ending = "a Neutral Ending. "; }
        else if(GameState.isGoodEnding) { ending = "a Good Ending! "; }
        else if(GameState.isPerfectEnding) { ending = "a Secret Ending!"; }
        else { ending = "unassigned"; }

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

    public void SetText()
    {
        textMesh.text = "Thank you for playing this short demo of Draconia! You have achieved " + ending + " Congratulations! There are 5 other possible endings. Can you get them all?.";
        textMesh2.text = "Made By: Alex Gasowski. Art by me.";
    }

}
