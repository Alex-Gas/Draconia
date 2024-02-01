using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : UIManager 
{
    private Dictionary<int, DialogOption> dialogOptionsDict;
    private Dictionary<int, string> playerLinesDict;
    private Dictionary<int, string> npcLinesDict;
    public GameObject dialogUIpref;
    private GameObject dialogUI;
    private GameObject uiContainer;
    private DialogOperator dialogOperator;
    public static bool isDialogOpen = false;

    private void Start()
    {
        dialogOptionsDict = new Dictionary<int, DialogOption>();
        playerLinesDict = new Dictionary<int, string>();
        npcLinesDict = new Dictionary<int, string>();

        uiContainer = GameObject.Find("UI");
        NPCLibrary.PrepareNPCs();
        DialogLibrary.PrepareDialog();
    }

    protected override void LoadUI()
    {
        dialogUI = Instantiate(dialogUIpref, uiContainer.transform);
        dialogOperator = dialogUI.GetComponent<DialogOperator>();
        dialogOperator.Setup(this);
    }

    protected override void LoadDialog(string npcID)
    {
        // A list of dalogue options class
        dialogOptionsDict = DialogLibrary.GetDialogOptions(npcID);
        // A dict of dialog option lines said by the player
        playerLinesDict = DialogLibrary.GetPlayerLines(npcID);
        // A dic of dialog lines said by the NPC
        npcLinesDict = DialogLibrary.GetNPCLines(npcID);
    }


    protected override void StartDialog(string npcName)
    {
        foreach(KeyValuePair<int, DialogOption> option in DialogLibrary.GetDialogOptions(npcName))
        {
            if (option.Value.IsDialogInitial() == true)
            {
                CycleDialog(option.Value.GetDialogID());
                break;
            }
        }
    }



    public void CycleDialog(int dialogID)
    {
        DialogOption dialogOption = dialogOptionsDict[dialogID];

        if (dialogOption.IsDialogEnd())
        {
            if (dialogOption.IsDialogTransition() == true)
            {
                GameState.isPaused = false;
                dialogOption.ExecuteActions();
            }
            else 
            {
                dialogOption.ExecuteActions();
                GameState.isPaused = false;
            }

            CloseDialog();
        }
        else
        {
            if (dialogOption.IsPowerCheck())
            {
                // if dialogue option is a power check one of the actions must set a check result variable. 
                dialogOption.ExecuteActions();
                // after check result variable is set which indicates what choice the player made the dialog gets cycled with that option.
                CycleDialog(dialogOption.GetCheckResult());
            }

            else
            {
                Debug.Log("Dialog ID: " + dialogOption.GetDialogID());
                Debug.Log("NPC Line: " + dialogOption.GetDialogLineID());
                string numbersAsString = string.Join(", ", dialogOption.GetAvailableDialogIDs());
                Debug.Log("ID's: " + numbersAsString);
                numbersAsString = string.Join(", ", dialogOption.GetAvailableDialogLines());
                Debug.Log("Player Lines: " + numbersAsString);

                dialogOption.ExecuteActions();

                ShowNPCLine(dialogOption.GetDialogLineID());
                ShowButtons(dialogOption.GetAvailableDialogIDs(), dialogOption.GetAvailableDialogLines());
            }
        }
    }

    private void ShowNPCLine(int npcDialogueID)
    {
        string text = npcLinesDict[npcDialogueID];
        dialogOperator.SetNPCText(text);
    }

    private void ShowButtons(List<int> dialogOptionsIDs, List<int> dialogLinesIDs)
    {
        dialogOperator.DisableButtons();

        List<string> playerLinesList = new List<string>();

        foreach (int dialogID in dialogLinesIDs)
        {
            playerLinesList.Add(playerLinesDict[dialogID]);
        }

        dialogOperator.SetButtons(dialogOptionsIDs, playerLinesList);
    }


    private void CloseDialog()
    {
        isUIopen = false;
        isDialogOpen = false;
        Destroy(dialogUI);
    }
}
