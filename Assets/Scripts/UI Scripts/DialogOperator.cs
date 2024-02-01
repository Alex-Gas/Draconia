using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogOperator : MonoBehaviour
{
    
    public GameObject textPanel;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    private List<GameObject> buttonList;
    private DialogManager dialogManager;


    public void Setup(DialogManager dialogManager)
    {
        buttonList = new List<GameObject>()
        {
            button1, button2, button3, button4, button5
        };

        this.dialogManager = dialogManager;
    }

    public void DisableButtons()
    { 
        foreach (GameObject button in buttonList)
        {
            UnityEngine.UI.Button buttonComp = button.GetComponent<UnityEngine.UI.Button>();
            buttonComp.interactable = false; 
            buttonComp.interactable = true; 
            button.SetActive(false);
        }
    }

    public void SetNPCText(string text)
    {
        SetText(textPanel, text);
    }

    public void SetButtons(List<int> dialogOptionsIDs, List<string> playerLinesList)
    {
        if (dialogOptionsIDs.Count > 0 && playerLinesList.Count > 0)
        {
            if (dialogOptionsIDs.Count == playerLinesList.Count)
            {
                for (int i = 0; i < dialogOptionsIDs.Count; i++)
                {
                    SetButton(i, dialogOptionsIDs[i], playerLinesList[i]);
                }
            }

            else
            {
                throw new Exception("Amount of dialog options doesn't match amount of dialogue lines");
            }
        }
    }

    private void SetButton(int i, int id, string line)
    {
        GameObject button = buttonList[i];

        UnityEngine.UI.Button btncomp = button.GetComponent<UnityEngine.UI.Button>();
        btncomp.onClick.RemoveAllListeners();
        btncomp.onClick.AddListener(() => dialogManager.CycleDialog(id));

        SetText(button, line);

        button.SetActive(true);
    }

    private void SetText(GameObject obj, string text)
    {
        GameObject textEle = obj.transform.Find("Text").gameObject;
        TextMeshProUGUI textMesh = textEle.GetComponent<TextMeshProUGUI>();
        textMesh.text = text;
    }


}
