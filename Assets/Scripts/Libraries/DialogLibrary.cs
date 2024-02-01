using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public static class DialogLibrary
{
    private static Dictionary<int, DialogOption> dialogOptionsDict;
    private static Dictionary<string, Dictionary<int, DialogOption>> dialogDict;
    private static XmlDocument playerLinesDoc;
    private static XmlDocument npcLinesDoc;
    private static TransitionManager transitionManager;
    private static CutsceneManager cutsceneManager;
    private static GameObject hiddenCache;

    public static void PrepareDialog()
    {
        CreateDialogDictionary();
        LoadDialogLineDocuments();
        LoadReferences();
    }

    private static void LoadReferences()
    {
        transitionManager = GameObject.Find("UI").GetComponent<TransitionManager>();
        cutsceneManager = GameObject.Find("UI").GetComponent<CutsceneManager>();
        hiddenCache = GameObject.Find("HiddenCache");
        hiddenCache.SetActive(false);
    }

    public static Dictionary<int, DialogOption> GetDialogOptions(string npcID) 
    { 
        return dialogDict[npcID]; 
    }

    public static Dictionary<int, string> GetPlayerLines(string npcID) 
    {
        return GetPlayerLinesOfName(npcID); 
    }

    public static Dictionary<int, string> GetNPCLines(string npcID) 
    {
        return GetNPCLinesOfName(npcID); 
    }

    public static void PassInitialFlag(string npcName, int dialogID)
    {
        // setting all initial flags (there should always be one) to false
        foreach (KeyValuePair<int, DialogOption> entry in GetDialogOptions(npcName))
        {
            entry.Value.SetDialogInitialFlag(false);
        }
        GetDialogOptions(npcName)[dialogID].SetDialogInitialFlag(true);
    }


    private static DialogOption SetCommonData(int dialogID, DialogOption dialogOption, List<int> availableDialogIDs = null, int dialogLineID = 0, List<int> availableDialogLines = null)
    {
        dialogOption.SetDialogID(dialogID);

        if (availableDialogIDs != null) { dialogOption.SetAvailableDialogIDs(availableDialogIDs); }   
        if (dialogLineID != 0) { dialogOption.SetDialogLineID(dialogLineID); }
        if (availableDialogLines != null) { dialogOption.SetAvailableDialogLines(availableDialogLines); }

        return dialogOption;
    }



    private static void CreateDialogDictionary()
    {
        int id;
        List<int> availableDialogIDs;
        int dialogLineID;
        List<int> availableDialogLines;

        dialogDict = new Dictionary<string, Dictionary<int, DialogOption>>();

        //--------- TEST

        dialogOptionsDict = new Dictionary<int, DialogOption>();

        {
            id = 1;
            availableDialogIDs = new List<int>() { 2, 3, 10 };
            dialogLineID = 1;
            availableDialogLines = new List<int>() { 2, 3, 5 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogInitialFlag(true);
            dialogOption.AddAction(() => { if (GameState.isTheCheckPassed == true) { dialogOption.SetAvailableDialogIDs(new List<int>() { 8, 3, 10 }); } });
            dialogOption.AddAction(() =>
            {
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 5))
                {
                    dialogOption.AddNewAvailableDialogID(11);
                    dialogOption.AddNewAvailableDialogLine(6);
                }
                else
                {
                    dialogOption.RemoveAvailableDialogID(11);
                    dialogOption.RemoveAvailableDialogLine(6);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 2;
            availableDialogIDs = new List<int>() { 5 };
            dialogLineID = 2;
            availableDialogLines = new List<int>() { 1 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 3;
            availableDialogIDs = new List<int>() { 4, 12 };
            dialogLineID = 3;
            availableDialogLines = new List<int>() { 4, 7 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 4;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 5;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetPowerCheckFlag(true);
            dialogOption.AddAction(() => dialogOption.SetCheckResult(dialogOption.IsCheckPassed(100, 0) == true ? 6 : 7));

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 6;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 4;
            availableDialogLines = new List<int>() { 3 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => GameState.isTheCheckPassed = true);
            dialogOption.AddAction(() => Debug.Log(GameState.isTheCheckPassed));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 7;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 5;
            availableDialogLines = new List<int>() { 3 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 8;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 6;
            availableDialogLines = new List<int>() { 3 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 9;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 7;
            availableDialogLines = new List<int>() { 4 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 10;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 8;
            availableDialogLines = new List<int>() { 4 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => PassInitialFlag("npc_test", 9));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 11;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 9;
            availableDialogLines = new List<int>() { 3 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => ItemManager.RemoveItemFromPosession(5));
            dialogOption.AddAction(() => ItemManager.AddItemToPosession(null, ItemManager.ProcureItemData(1, 10000, 0, 0)));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 12;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            dialogOption.AddAction(() => NPCLibrary.npcRefDict["npc_test"].GetComponent<NPCBehaviour>().SetNpcDead(true));
            dialogOption.AddAction(() => NPCLibrary.npcRefDict["npc_test"].GetComponent<NPCBehaviour>().NPCDropItem(ItemManager.ProcureItemData(1, 140000, 0, 0)));

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }


        dialogDict.Add("npc_test", dialogOptionsDict);

        //--------- SHAMAN

        dialogOptionsDict = new Dictionary<int, DialogOption>();

        {
            id = 1;
            availableDialogIDs = new List<int>() { 2, 3 };
            dialogLineID = 1;
            availableDialogLines = new List<int>() { 1, 2 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogInitialFlag(true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 2;
            availableDialogIDs = new List<int>() { 4, 5 };
            dialogLineID = 2;
            availableDialogLines = new List<int>() { 5, 3 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 3;
            availableDialogIDs = new List<int>() { 4, 5 };
            dialogLineID = 3;
            availableDialogLines = new List<int>() { 5, 4 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 4;
            availableDialogIDs = new List<int>() { 7, 9, 8 };
            dialogLineID = 4;
            availableDialogLines = new List<int>() { 7, 6, 8 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {// intro end
            id = 5;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            dialogOption.SetDialogTransition(true);
            dialogOption.AddAction(() =>
                PassInitialFlag("shaman", 13));
            dialogOption.AddAction(() => {
                dialogDict["shaman"][9].SetAvailableDialogIDs(new List<int>() { 10, 15, 12, 6 });
                dialogDict["shaman"][9].SetAvailableDialogLines(new List<int>() { 10, 17, 12, 9 });
            });
            dialogOption.AddAction(() => {
                dialogDict["shaman"][6].SetAvailableDialogIDs(new List<int>() { 7, 9, 20 });
                dialogDict["shaman"][6].SetAvailableDialogLines(new List<int>() { 7, 6, 16 });
            });
            dialogOption.AddAction(() => {
                dialogDict["shaman"][7].SetAvailableDialogIDs(new List<int>() { 6, 20 });
                dialogDict["shaman"][7].SetAvailableDialogLines(new List<int>() { 13, 16 });
            });
            dialogOption.AddAction(() => {
                dialogDict["shaman"][10].SetAvailableDialogIDs(new List<int>() { 6, 20 });
                dialogDict["shaman"][10].SetAvailableDialogLines(new List<int>() { 13, 16 });
            });
            dialogOption.AddAction(() => {
                dialogDict["shaman"][12].SetAvailableDialogIDs(new List<int>() { 6, 20 });
                dialogDict["shaman"][12].SetAvailableDialogLines(new List<int>() { 13, 16 });
            });
            dialogOption.AddAction(() =>
                {
                    List<TransitionOperator.Action> tranActions = new List<TransitionOperator.Action>()
                        {
                            () => NPCLibrary.npcRefDict["shaman"].transform.position = new Vector3(-3.5f, -89.5f, -6f)
                        };
                    transitionManager.InitiateTransition(1f, 1f, tranActions, true);
                });
            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 6;
            availableDialogIDs = new List<int>() { 7, 9, 8 };
            dialogLineID = 5;
            availableDialogLines = new List<int>() { 7, 6, 14 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                if (GameState.isRitualPerformed)
                {
                    dialogOption.RemoveAvailableDialogID(38); dialogOption.RemoveAvailableDialogLine(43);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 7;
            availableDialogIDs = new List<int>() { 6, 8 };
            dialogLineID = 6;
            availableDialogLines = new List<int>() { 13, 14 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 8;
            availableDialogIDs = new List<int>() { 5 };
            dialogLineID = 7;
            availableDialogLines = new List<int>() { 15 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 9;
            availableDialogIDs = new List<int>() { 10, 11, 12, 6 };
            dialogLineID = 8;
            availableDialogLines = new List<int>() { 10, 11, 12, 9 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 10;
            availableDialogIDs = new List<int>() { 6, 8 };
            dialogLineID = 9;
            availableDialogLines = new List<int>() { 13, 14 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 11;
            availableDialogIDs = new List<int>() { 6, 8 };
            dialogLineID = 10;
            availableDialogLines = new List<int>() { 13, 14 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 12;
            availableDialogIDs = new List<int>() { 6, 8 };
            dialogLineID = 11;
            availableDialogLines = new List<int>() { 13, 14 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 13;
            availableDialogIDs = new List<int>() { 7, 9, 20 };
            dialogLineID = 12;
            availableDialogLines = new List<int>() { 7, 6, 16 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => PassInitialFlag("shaman", 14));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 14;
            availableDialogIDs = new List<int>() { 7, 9, 20 };
            dialogLineID = 13;
            availableDialogLines = new List<int>() { 7, 6, 16 };
            DialogOption dialogOption = new DialogOption();


            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 15;
            availableDialogIDs = new List<int>() { 16, 17, 6, 20 };
            dialogLineID = 14;
            availableDialogLines = new List<int>() { 18, 19, 13, 20 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 16;
            availableDialogIDs = new List<int>() { 18, 19, 6 };
            dialogLineID = 15;
            availableDialogLines = new List<int>() { 21, 22, 9 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 17;
            availableDialogIDs = new List<int>() { 18, 19, 6 };
            dialogLineID = 16;
            availableDialogLines = new List<int>() { 21, 22, 9 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 18;
            availableDialogIDs = new List<int>() { 20 };
            dialogLineID = 17;
            availableDialogLines = new List<int>() { 15 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => {
                dialogDict["shaman"][9].SetAvailableDialogIDs(new List<int>() { 10, 21, 12, 6 });
                dialogDict["shaman"][9].SetAvailableDialogLines(new List<int>() { 10, 17, 12, 9 });
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 19;
            availableDialogIDs = new List<int>() { 20 };
            dialogLineID = 18;
            availableDialogLines = new List<int>() { 15 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // normal end
            id = 20;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 21;
            availableDialogIDs = new List<int>() { 22, 6, 19 };
            dialogLineID = 19;
            availableDialogLines = new List<int>() { 23, 13, 22 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 22;
            availableDialogIDs = new List<int>() { 6, 19 };
            dialogLineID = 20;
            availableDialogLines = new List<int>() { 13, 22 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // plants slain return
            id = 23;
            availableDialogIDs = new List<int>() { 26, 6 };
            dialogLineID = 21;
            availableDialogLines = new List<int>() { 29, 26 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                dialogDict["shaman"][6].RemoveAvailableDialogID(23); dialogDict["shaman"][6].RemoveAvailableDialogLine(24);
                dialogDict["shaman"][13].RemoveAvailableDialogID(23); dialogDict["shaman"][13].RemoveAvailableDialogLine(24);
                dialogDict["shaman"][14].RemoveAvailableDialogID(23); dialogDict["shaman"][14].RemoveAvailableDialogLine(24);

                dialogDict["shaman"][6].AddNewAvailableDialogID(24); dialogDict["shaman"][6].AddNewAvailableDialogLine(25);
                dialogDict["shaman"][13].AddNewAvailableDialogID(24); dialogDict["shaman"][13].AddNewAvailableDialogLine(25);
                dialogDict["shaman"][14].AddNewAvailableDialogID(24); dialogDict["shaman"][14].AddNewAvailableDialogLine(25);
                dialogDict["shaman"][39].AddNewAvailableDialogID(24); dialogDict["shaman"][39].AddNewAvailableDialogLine(25);
            });
            dialogOption.AddAction(() => GameState.isShamanQuestComplete = true);
            dialogOption.AddAction(() =>
            {
                if (GameState.isShamanQuestComplete && GameState.isBlacksmithQuestComplete && GameState.isMonkQuestComplete)
                {
                    PassInitialFlag("shaman", 31);
                }
            });
            dialogOption.AddAction(() =>
            {
                dialogDict["shaman"][9].RemoveAvailableDialogID(15); dialogDict["shaman"][9].RemoveAvailableDialogLine(17);
                dialogDict["shaman"][9].RemoveAvailableDialogID(21); dialogDict["shaman"][9].RemoveAvailableDialogLine(17);

                dialogDict["shaman"][9].AddNewAvailableDialogID(27); dialogDict["shaman"][9].AddNewAvailableDialogLine(36);
            });
            dialogOption.AddAction(() =>
            {
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 7))
                {
                    dialogOption.RemoveAvailableDialogID(25); dialogOption.RemoveAvailableDialogLine(28);
                    dialogOption.AddNewAvailableDialogID(25); dialogOption.AddNewAvailableDialogLine(28);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 24;
            availableDialogIDs = new List<int>() { 26, 6 };
            dialogLineID = 22;
            availableDialogLines = new List<int>() { 29, 27 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 7))
                {
                    dialogOption.RemoveAvailableDialogID(25); dialogOption.RemoveAvailableDialogLine(28);
                    dialogOption.AddNewAvailableDialogID(25); dialogOption.AddNewAvailableDialogLine(28);
                }
            });


            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { //enchant armor
            id = 25;
            availableDialogIDs = new List<int>() { 6, 20 };
            dialogLineID = 23;
            availableDialogLines = new List<int>() { 30, 31 };
            DialogOption dialogOption = new DialogOption();
     
            dialogOption.AddAction(() => ItemManager.UnequipItem(7));
            dialogOption.AddAction(() => ItemManager.RemoveItemFromPosession(7));
            dialogOption.AddAction(() => ItemManager.AddItemToPosession(null, ItemManager.ProcureItemData(8, 0, 4, 0)));
            dialogOption.AddAction(() =>
            {
                dialogDict["shaman"][6].RemoveAvailableDialogID(24); dialogDict["shaman"][6].RemoveAvailableDialogLine(25);
                dialogDict["shaman"][13].RemoveAvailableDialogID(24); dialogDict["shaman"][13].RemoveAvailableDialogLine(25);
                dialogDict["shaman"][14].RemoveAvailableDialogID(24); dialogDict["shaman"][14].RemoveAvailableDialogLine(25);
                dialogDict["shaman"][39].RemoveAvailableDialogID(24); dialogDict["shaman"][39].RemoveAvailableDialogLine(25);
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // empowering ritual
            id = 26;
            availableDialogIDs = new List<int>() { 6, 20 };
            dialogLineID = 24;
            availableDialogLines = new List<int>() { 30, 31 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => GameState.AddRemoveRawPower(1));
            dialogOption.AddAction(() =>
            {
                dialogDict["shaman"][6].RemoveAvailableDialogID(24); dialogDict["shaman"][6].RemoveAvailableDialogLine(25);
                dialogDict["shaman"][13].RemoveAvailableDialogID(24); dialogDict["shaman"][13].RemoveAvailableDialogLine(25);
                dialogDict["shaman"][14].RemoveAvailableDialogID(24); dialogDict["shaman"][14].RemoveAvailableDialogLine(25);
                dialogDict["shaman"][39].RemoveAvailableDialogID(24); dialogDict["shaman"][39].RemoveAvailableDialogLine(25);
            });
            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // shaman gives quest
            id = 27;
            availableDialogIDs = new List<int>() { 28 };
            dialogLineID = 25;
            availableDialogLines = new List<int>() { 32 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 28;
            availableDialogIDs = new List<int>() { 29, 30 };
            dialogLineID = 26;
            availableDialogLines = new List<int>() { 33, 34 };
            DialogOption dialogOption = new DialogOption();
// ADD HERE
            dialogOption.AddAction(() => PassInitialFlag("gate", 2));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 29;
            availableDialogIDs = new List<int>() { 6, 20 };
            dialogLineID = 27;
            availableDialogLines = new List<int>() { 35, 15 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => GameState.hasReceivedMainquest = true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 30;
            availableDialogIDs = new List<int>() { 6, 20 };
            dialogLineID = 28;
            availableDialogLines = new List<int>() { 35, 15 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => GameState.hasReceivedMainquest = true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // encounter start
            id = 31;
            availableDialogIDs = new List<int>() { 0 };
            dialogLineID = 29;
            availableDialogLines = new List<int>() { 0 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                if (GameState.hasReceivedMainquest)
                {
                    dialogOption.SetAvailableDialogIDs(new List<int>() { 32 }); dialogOption.SetAvailableDialogLines(new List<int>() { 37 });
                }
                else
                {
                    dialogOption.SetAvailableDialogIDs(new List<int>() { 33 }); dialogOption.SetAvailableDialogLines(new List<int>() { 37 });
                }

            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 32;
            availableDialogIDs = new List<int>() { 34 };
            dialogLineID = 30;
            availableDialogLines = new List<int>() { 38 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 33;
            availableDialogIDs = new List<int>() { 34 };
            dialogLineID = 31;
            availableDialogLines = new List<int>() { 39 };
            DialogOption dialogOption = new DialogOption();
// ADD HERE
            dialogOption.AddAction(() => PassInitialFlag("gate", 2));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 34;
            availableDialogIDs = new List<int>() { 35, 36 };
            dialogLineID = 32;
            availableDialogLines = new List<int>() { 40, 41 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => PassInitialFlag("shaman", 39));
            dialogOption.AddAction(() =>
                {
                    dialogDict["shaman"][6].RemoveAvailableDialogID(9); dialogDict["shaman"][6].RemoveAvailableDialogLine(6);
                    dialogDict["shaman"][6].AddNewAvailableDialogID(38); dialogDict["shaman"][6].AddNewAvailableDialogLine(43);
                });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 35;
            availableDialogIDs = new List<int>() { 20 };
            dialogLineID = 33;
            availableDialogLines = new List<int>() { 15 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 36;
            availableDialogIDs = new List<int>() { 37 };
            dialogLineID = 34;
            availableDialogLines = new List<int>() { 42 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // ritual of binding
            id = 37;
            availableDialogIDs = new List<int>() { 20 };
            dialogLineID = 35;
            availableDialogLines = new List<int>() { 44 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => GameState.AddRemoveDracPower(3));
            dialogOption.AddAction(() => GameState.isRitualPerformed = true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 38;
            availableDialogIDs = new List<int>() { 37 };
            dialogLineID = 36;
            availableDialogLines = new List<int>() { 42 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 39;
            availableDialogIDs = new List<int>() { 38, 7, 20 };
            dialogLineID = 37;
            availableDialogLines = new List<int>() { 43, 7, 16 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                if (GameState.isRitualPerformed)
                {
                    dialogOption.RemoveAvailableDialogID(38); dialogOption.RemoveAvailableDialogLine(43);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }


        dialogDict.Add("shaman", dialogOptionsDict);

        //--------- PLANTS

        dialogOptionsDict = new Dictionary<int, DialogOption>();

        {
            id = 1;
            availableDialogIDs = new List<int>() { 3 };
            dialogLineID = 1;
            availableDialogLines = new List<int>() { 1 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogInitialFlag(true);
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(4); dialogOption.RemoveAvailableDialogLine(4);
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 4))
                {
                    dialogOption.AddNewAvailableDialogID(4); dialogOption.AddNewAvailableDialogLine(4);
                }
                else
                {
                    dialogOption.RemoveAvailableDialogID(4); dialogOption.RemoveAvailableDialogLine(4);
                }
            });
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(2); dialogOption.RemoveAvailableDialogLine(2);
                if (GameState.rawPower >= 5)
                {
                    dialogOption.AddNewAvailableDialogID(2); dialogOption.AddNewAvailableDialogLine(2);
                }
                else
                {
                    dialogOption.RemoveAvailableDialogID(2); dialogOption.RemoveAvailableDialogLine(2);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // kill plants
            id = 2;
            availableDialogIDs = new List<int>() { 5 };
            dialogLineID = 2;
            availableDialogLines = new List<int>() { 3 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // normal end
            id = 3;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // spill whisky
            id = 4;
            availableDialogIDs = new List<int>() { 5 };
            dialogLineID = 3;
            availableDialogLines = new List<int>() { 5 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => ItemManager.RemoveItemFromPosession(4));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // victory end
            id = 5;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            dialogOption.AddAction(() => NPCLibrary.npcRefDict["plants"].GetComponent<NPCBehaviour>().SetNpcDead(true));
            dialogOption.AddAction(() => NPCLibrary.npcRefDict["plants"].GetComponent<NPCBehaviour>().NPCDropItem(ItemManager.ProcureItemData(6, 0, 0, 0)));
            dialogOption.AddAction(() =>
            {
                dialogDict["shaman"][6].AddNewAvailableDialogID(23); dialogDict["shaman"][6].AddNewAvailableDialogLine(24);
                dialogDict["shaman"][13].AddNewAvailableDialogID(23); dialogDict["shaman"][13].AddNewAvailableDialogLine(24);
                dialogDict["shaman"][14].AddNewAvailableDialogID(23); dialogDict["shaman"][14].AddNewAvailableDialogLine(24);
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        dialogDict.Add("plants", dialogOptionsDict);

        //--------- KNIGHT

        dialogOptionsDict = new Dictionary<int, DialogOption>();

        {
            id = 1;
            availableDialogIDs = new List<int>() { 2, 3 };
            dialogLineID = 1;
            availableDialogLines = new List<int>() { 1, 2 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogInitialFlag(true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 2;
            availableDialogIDs = new List<int>() { 4, 11 };
            dialogLineID = 2;
            availableDialogLines = new List<int>() {4, 3 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 3;
            availableDialogIDs = new List<int>() { 4, 11 };
            dialogLineID = 3;
            availableDialogLines = new List<int>() { 4, 3 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // drac power check
            id = 4;
            availableDialogIDs = new List<int>() { 16, 11 };
            dialogLineID = 4;
            availableDialogLines = new List<int>() { 5, 3 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 5;
            availableDialogIDs = new List<int>() { 17, 11 };
            dialogLineID = 5;
            availableDialogLines = new List<int>() { 6, 3 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 6;
            availableDialogIDs = new List<int>() { 8, 9 };
            dialogLineID = 6;
            availableDialogLines = new List<int>() {7, 8 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 7;
            availableDialogIDs = new List<int>() { 9, 21 };         // ADD LEAVE ENDING HERE
            dialogLineID = 7;
            availableDialogLines = new List<int>() { 8, 9 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 8;
            availableDialogIDs = new List<int>() { 9, 22 };          // ADD JOIN ENDING HERE
            dialogLineID = 8;
            availableDialogLines = new List<int>() { 8, 10 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 9;
            availableDialogIDs = new List<int>() { 18 };
            dialogLineID = 9;
            availableDialogLines = new List<int>() { 11 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 10;
            availableDialogIDs = new List<int>() { 18 };
            dialogLineID = 10;
            availableDialogLines = new List<int>() { 11 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 11;
            availableDialogIDs = new List<int>() { 18 };
            dialogLineID = 11;
            availableDialogLines = new List<int>() { 11 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 12;
            availableDialogIDs = new List<int>() { 13, 14 };
            dialogLineID = 12;
            availableDialogLines = new List<int>() { 12, 13 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 13;
            availableDialogIDs = new List<int>() { 19 };
            dialogLineID = 13;
            availableDialogLines = new List<int>() { 14 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 14;
            availableDialogIDs = new List<int>() { 23 };       // ADD KILL ENDING HERE
            dialogLineID = 14;
            availableDialogLines = new List<int>() { 15 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 15;
            availableDialogIDs = new List<int>() { 20 };      // ADD BAD DEATH ENDING HERE
            dialogLineID = 15;
            availableDialogLines = new List<int>() { 16 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // first power check
            id = 16;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetPowerCheckFlag(true);
            dialogOption.AddAction(() =>
            {
                if (GameState.draconicPower <= 2)
                {
                    dialogOption.SetCheckResult(5);
                }
                else
                {
                    dialogOption.SetCheckResult(10);
                }
            });

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // talk power check
            id = 17;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetPowerCheckFlag(true);
            dialogOption.AddAction(() =>
            {
                if (GameState.rawPower >= 10)
                {
                    dialogOption.SetCheckResult(6);
                }
                else
                {
                    dialogOption.SetCheckResult(7);
                }
            });

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // fight power check
            id = 18;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetPowerCheckFlag(true);
            dialogOption.AddAction(() =>
            {
                if (GameState.rawPower >= 10)
                {
                    dialogOption.SetCheckResult(12);
                }
                else
                {
                    dialogOption.SetCheckResult(15);
                }
            });

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // final power check
            id = 19;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetPowerCheckFlag(true);
            dialogOption.AddAction(() =>
            {
                if (GameState.rawPower >= 11 && GameState.draconicPower >= 7)
                {
                    dialogOption.SetCheckResult(25);    // ADD PERFECT ENDING
                }
                else
                {
                    dialogOption.SetCheckResult(24);      // ADD GOOD ENDING
                }
            });

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // Death ending
            id = 20;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            dialogOption.AddAction(() => GameState.isWorstEnding = true);
            dialogOption.AddAction(() => cutsceneManager.InitiateCutscene("dieending", 1f, 1f, 4f));

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // Leave ending
            id = 21;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            dialogOption.SetDialogTransition(true);
            dialogOption.AddAction(() => GameState.isBadEnding = true);
            dialogOption.AddAction(() => cutsceneManager.InitiateCutscene("leaveending", 1f, 1f, 4f));

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // Join ending
            id = 22;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            dialogOption.SetDialogTransition(true);
            dialogOption.AddAction(() => GameState.isEvilEnding = true);
            dialogOption.AddAction(() => cutsceneManager.InitiateCutscene("joinending", 1f, 1f, 4f));

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // Kill ending
            id = 23;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            dialogOption.SetDialogTransition(true);
            dialogOption.AddAction(() => GameState.isNeutralEnding = true);
            dialogOption.AddAction(() => cutsceneManager.InitiateCutscene("killending", 1f, 1f, 4f));

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // Reclaim ending
            id = 24;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            dialogOption.SetDialogTransition(true);
            dialogOption.AddAction(() => GameState.isGoodEnding = true);
            dialogOption.AddAction(() => cutsceneManager.InitiateCutscene("victoryending", 1f, 1f, 4f));

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // Secret ending
            id = 25;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            dialogOption.SetDialogTransition(true);
            dialogOption.AddAction(() => GameState.isPerfectEnding = true);
            dialogOption.AddAction(() => cutsceneManager.InitiateCutscene("perfectending", 1f, 1f, 4f));

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }

        dialogDict.Add("knight", dialogOptionsDict);

        //--------- BLACKSMITH

        dialogOptionsDict = new Dictionary<int, DialogOption>();

        {
            id = 1;
            availableDialogIDs = new List<int>() { 2, 5, 3 };       
            dialogLineID = 1;
            availableDialogLines = new List<int>() { 1, 3, 2 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogInitialFlag(true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 2;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 2;
            availableDialogLines = new List<int>() { 4 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 3;
            availableDialogIDs = new List<int>() { 4, 6 };
            dialogLineID = 3;
            availableDialogLines = new List<int>() { 5, 6 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 4;
            availableDialogIDs = new List<int>() { 19, 11, 10 };
            dialogLineID = 4;
            availableDialogLines = new List<int>() { 20, 8, 7 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => PassInitialFlag("blacksmith", 9));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 5;
            availableDialogIDs = new List<int>() { 19, 11, 10 };
            dialogLineID = 5;
            availableDialogLines = new List<int>() { 20, 8, 7 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => PassInitialFlag("blacksmith", 9));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 6;
            availableDialogIDs = new List<int>() { 19, 11, 10 };
            dialogLineID = 6;
            availableDialogLines = new List<int>() { 20, 8, 7 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => PassInitialFlag("blacksmith", 9));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 7;
            availableDialogIDs = new List<int>() { 19, 11, 10 };
            dialogLineID = 7;
            availableDialogLines = new List<int>() { 20, 8, 7 };
            DialogOption dialogOption = new DialogOption();

            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(22); dialogOption.RemoveAvailableDialogLine(26);
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 5) && GameState.isBlacksmithQuestActive)
                {
                   
                    dialogOption.AddNewAvailableDialogID(22); dialogOption.AddNewAvailableDialogLine(26);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 8;
            availableDialogIDs = new List<int>() { 19, 11, 10 };
            dialogLineID = 8;
            availableDialogLines = new List<int>() { 20, 8, 7 };
            DialogOption dialogOption = new DialogOption();

            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(22); dialogOption.RemoveAvailableDialogLine(26);
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 5) && GameState.isBlacksmithQuestActive)
                {
                    dialogOption.AddNewAvailableDialogID(22); dialogOption.AddNewAvailableDialogLine(26);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 9;
            availableDialogIDs = new List<int>() { 19, 11, 10 };
            dialogLineID = 9;
            availableDialogLines = new List<int>() { 20, 8, 7 };
            DialogOption dialogOption = new DialogOption();

            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(22); dialogOption.RemoveAvailableDialogLine(26);
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 5) && GameState.isBlacksmithQuestActive)
                {
                    dialogOption.AddNewAvailableDialogID(22); dialogOption.AddNewAvailableDialogLine(26);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // normal end
            id = 10;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 11;
            availableDialogIDs = new List<int>() { 12, 13, 14, 8 };
            dialogLineID = 10;
            availableDialogLines = new List<int>() {9, 10, 11, 19 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                if (GameState.isBlacksmithQuestActive || GameState.isBlacksmithQuestComplete)
                {
                    dialogOption.RemoveAvailableDialogID(12); dialogOption.RemoveAvailableDialogLine(9);
                }
            });
            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 12;
            availableDialogIDs = new List<int>() { 15,  11};
            dialogLineID = 11;
            availableDialogLines = new List<int>() { 12, 18 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 13;
            availableDialogIDs = new List<int>() { 11 };
            dialogLineID = 12;
            availableDialogLines = new List<int>() { 18 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 14;
            availableDialogIDs = new List<int>() { 11 };
            dialogLineID = 13;
            availableDialogLines = new List<int>() { 18 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 15;
            availableDialogIDs = new List<int>() { 16, 11 };
            dialogLineID = 14;
            availableDialogLines = new List<int>() { 13, 17  };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { 
            id = 16;
            availableDialogIDs = new List<int>() { 17, 18 };
            dialogLineID = 15;
            availableDialogLines = new List<int>() { 14, 15 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // obtain blacksmith quest
            id = 17;
            availableDialogIDs = new List<int>() { 11, 10 };
            dialogLineID = 16;
            availableDialogLines = new List<int>() { 18, 16 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => GameState.isBlacksmithQuestActive = true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 18;
            availableDialogIDs = new List<int>() { 11, 10 };
            dialogLineID = 17;
            availableDialogLines = new List<int>() { 18, 16 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => GameState.isBlacksmithQuestActive = true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 19;
            availableDialogIDs = new List<int>() { 8, 27, 28 };
            dialogLineID = 18;
            availableDialogLines = new List<int>() { 23, 21, 22   };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 20;
            availableDialogIDs = new List<int>() {8, 10 };
            dialogLineID = 19;
            availableDialogLines = new List<int>() {25, 24 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                dialogDict["blacksmith"][19].RemoveAvailableDialogID(27); dialogDict["blacksmith"][19].RemoveAvailableDialogLine(21);
            });
            dialogOption.AddAction(() => PassInitialFlag("blacksmith", 7));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 21;
            availableDialogIDs = new List<int>() { 8, 10 };
            dialogLineID = 20;
            availableDialogLines = new List<int>() { 25, 24 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                dialogDict["blacksmith"][19].RemoveAvailableDialogID(28); dialogDict["blacksmith"][19].RemoveAvailableDialogLine(22);
            });
            dialogOption.AddAction(() => PassInitialFlag("blacksmith", 7));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 22;
            availableDialogIDs = new List<int>() { 23 };
            dialogLineID = 22;
            availableDialogLines = new List<int>() { 27 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => PassInitialFlag("blacksmith", 7));
            dialogOption.AddAction(() => ItemManager.RemoveItemFromPosession(5));
            dialogOption.AddAction(() => GameState.isBlacksmithQuestActive = false);
            dialogOption.AddAction(() => GameState.isBlacksmithQuestComplete = true);
            dialogOption.AddAction(() =>
            {
                if (GameState.isShamanQuestComplete && GameState.isBlacksmithQuestComplete && GameState.isMonkQuestComplete)
                {
                    PassInitialFlag("shaman", 31);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 23;
            availableDialogIDs = new List<int>() { 24, 30 };
            dialogLineID = 23;
            availableDialogLines = new List<int>() { 28, 29 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 24;
            availableDialogIDs = new List<int>() { 25 };
            dialogLineID = 24;
            availableDialogLines = new List<int>() { 30 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 25;
            availableDialogIDs = new List<int>() { 26 };
            dialogLineID = 26;
            availableDialogLines = new List<int>() { 31 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => GameState.AddRemoveDracPower(2));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 26;
            availableDialogIDs = new List<int>() { 11, 10 };
            dialogLineID = 27;
            availableDialogLines = new List<int>() { 32, 33 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        {   // buy weapon check
            id = 27;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetPowerCheckFlag(true);
            dialogOption.AddAction(() =>
            {
                if (GameState.goldTotalAmount >= 300)
                {
                    GameState.AddRemoveGold(-300);
                    ItemManager.AddItemToPosession(null, ItemManager.ProcureItemData(3, 0, 3, 0));
                    dialogOption.SetCheckResult(20);
                }
                else
                {
                    dialogOption.SetCheckResult(29);
                }
            });

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            // buy shield check
            id = 28;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetPowerCheckFlag(true);
            dialogOption.AddAction(() =>
            {
                if (GameState.goldTotalAmount >= 200)
                {
                    GameState.AddRemoveGold(-200);
                    ItemManager.AddItemToPosession(null, ItemManager.ProcureItemData(2, 0, 2, 0));
                    dialogOption.SetCheckResult(21);
                }
                else
                {
                    dialogOption.SetCheckResult(29);
                }
            });

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 29;
            availableDialogIDs = new List<int>() { 8, 10 };
            dialogLineID = 21 ;
            availableDialogLines = new List<int>() { 25, 16 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 30;
            availableDialogIDs = new List<int>() { 11, 10 };
            dialogLineID = 25;
            availableDialogLines = new List<int>() { 18, 16 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => ItemManager.AddItemToPosession(null, ItemManager.ProcureItemData(7, 0, 2, 0)));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        dialogDict.Add("blacksmith", dialogOptionsDict);

        //--------- MONK

        dialogOptionsDict = new Dictionary<int, DialogOption>();

        {
            id = 1;
            availableDialogIDs = new List<int>() { 2, 3, 4, 5 };
            dialogLineID = 1;
            availableDialogLines = new List<int>() { 1, 2, 3, 4 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogInitialFlag(true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        {
            id = 2;
            availableDialogIDs = new List<int>() { 3, 4, 5 };
            dialogLineID = 2;
            availableDialogLines = new List<int>() { 2, 3, 4 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        {
            id = 3;
            availableDialogIDs = new List<int>() { 2, 4, 5 };
            dialogLineID = 3;
            availableDialogLines = new List<int>() { 1, 3, 4 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        {
            id = 4;
            availableDialogIDs = new List<int>() { 2, 3, 5 };
            dialogLineID = 4;
            availableDialogLines = new List<int>() { 1, 2, 4 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        {
            id = 5;
            availableDialogIDs = new List<int>() { 10, 12, 9 };
            dialogLineID = 5;
            availableDialogLines = new List<int>() { 9, 11, 5 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => PassInitialFlag("monk", 7));
            // pearl
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(21); dialogOption.RemoveAvailableDialogLine(12);
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 6))
                {
                    dialogOption.AddNewAvailableDialogID(21); dialogOption.AddNewAvailableDialogLine(12);
                }
            });
            dialogOption.AddAction(() =>
            {
                dialogDict["monk"][2].RemoveAvailableDialogID(5); dialogDict["monk"][2].RemoveAvailableDialogLine(4);
                dialogDict["monk"][3].RemoveAvailableDialogID(5); dialogDict["monk"][3].RemoveAvailableDialogLine(4);
                dialogDict["monk"][4].RemoveAvailableDialogID(5); dialogDict["monk"][4].RemoveAvailableDialogLine(4);

                dialogDict["monk"][2].AddNewAvailableDialogID(10); dialogDict["monk"][2].AddNewAvailableDialogLine(9);
                dialogDict["monk"][3].AddNewAvailableDialogID(10); dialogDict["monk"][3].AddNewAvailableDialogLine(9);
                dialogDict["monk"][4].AddNewAvailableDialogID(10); dialogDict["monk"][4].AddNewAvailableDialogLine(9);
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        {
            id = 6;
            availableDialogIDs = new List<int>() { 10, 12, 9 };
            dialogLineID = 6;
            availableDialogLines = new List<int>() { 9, 11, 5 };
            DialogOption dialogOption = new DialogOption();
            // pearl
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(21); dialogOption.RemoveAvailableDialogLine(12);
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 6))
                {
                    dialogOption.AddNewAvailableDialogID(21); dialogOption.AddNewAvailableDialogLine(12);
                }
            });
            // ring
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(23); dialogOption.RemoveAvailableDialogLine(10);
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 9))
                {
                    dialogOption.AddNewAvailableDialogID(23); dialogOption.AddNewAvailableDialogLine(10);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        {
            id = 7;
            availableDialogIDs = new List<int>() { 10, 12, 9 };
            dialogLineID = 7;
            availableDialogLines = new List<int>() { 9, 11, 5 };
            DialogOption dialogOption = new DialogOption();
            // pearl
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(21); dialogOption.RemoveAvailableDialogLine(12);
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 6))
                {
                    dialogOption.AddNewAvailableDialogID(21); dialogOption.AddNewAvailableDialogLine(12);
                }
            });
            // ring
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(23); dialogOption.RemoveAvailableDialogLine(10);
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 9))
                {
                    dialogOption.AddNewAvailableDialogID(23); dialogOption.AddNewAvailableDialogLine(10);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        {
            id = 8;
            availableDialogIDs = new List<int>() { 10, 12, 9 };
            dialogLineID = 8;
            availableDialogLines = new List<int>() { 9, 11, 5 };
            DialogOption dialogOption = new DialogOption();
            // pearl
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(21); dialogOption.RemoveAvailableDialogLine(12);
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 6))
                {
                    dialogOption.AddNewAvailableDialogID(21); dialogOption.AddNewAvailableDialogLine(12);
                }
            });
            // ring
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(23); dialogOption.RemoveAvailableDialogLine(10);
                if (ItemManager.itemsInPosession.Any(obj => obj.GetItemID() == 9))
                {
                    dialogOption.AddNewAvailableDialogID(23); dialogOption.AddNewAvailableDialogLine(10);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // normal end
            id = 9;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 10;
            availableDialogIDs = new List<int>() { 2, 3, 4, 11, 8 };
            dialogLineID = 9;
            availableDialogLines = new List<int>() { 1, 2, 3, 7, 8 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 11;
            availableDialogIDs = new List<int>() { 10 };
            dialogLineID = 10;
            availableDialogLines = new List<int>() { 9 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 12;
            availableDialogIDs = new List<int>() {13, 14};
            dialogLineID = 11;
            availableDialogLines = new List<int>() { 13, 14 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                if (GameState.isMonkQuestActive == true || GameState.isMonkQuestComplete == true)
                {
                    dialogOption.RemoveAvailableDialogID(14); dialogOption.RemoveAvailableDialogLine(14);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 13;
            availableDialogIDs = new List<int>() { 8, 14 };
            dialogLineID = 12;
            availableDialogLines = new List<int>() { 15, 14};
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                if (GameState.isMonkQuestActive == true || GameState.isMonkQuestComplete == true)
                {
                    dialogOption.RemoveAvailableDialogID(14); dialogOption.RemoveAvailableDialogLine(14);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 14;
            availableDialogIDs = new List<int>() { 15, 6 };
            dialogLineID = 13;
            availableDialogLines = new List<int>() { 17, 16 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 15;
            availableDialogIDs = new List<int>() { 16, 17, 18, 6 };
            dialogLineID = 14;
            availableDialogLines = new List<int>() { 18, 19, 20, 21 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 16;
            availableDialogIDs = new List<int>() { 17, 18, 6};
            dialogLineID = 15;
            availableDialogLines = new List<int>() { 19, 20, 21 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 17;
            availableDialogIDs = new List<int>() { 16, 18, 6 };
            dialogLineID = 16;
            availableDialogLines = new List<int>() { 18, 20, 21 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { //quest accept
            id = 18;
            availableDialogIDs = new List<int>() { 19, 20 };
            dialogLineID = 17;
            availableDialogLines = new List<int>() { 22, 23};
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => GameState.isMonkQuestActive = true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 19;
            availableDialogIDs = new List<int>() { 6, 9 };
            dialogLineID = 18;
            availableDialogLines = new List<int>() { 8, 24 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 20;
            availableDialogIDs = new List<int>() {6, 9 };
            dialogLineID = 19;
            availableDialogLines = new List<int>() { 8, 24 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 21;
            availableDialogIDs = new List<int>() { 22, 8 };
            dialogLineID = 20;
            availableDialogLines = new List<int>() {25, 26 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 22;
            availableDialogIDs = new List<int>() { 8, 9 };
            dialogLineID = 21;
            availableDialogLines = new List<int>() { 27, 28 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                ItemManager.RemoveItemFromPosession(6);
                GameState.AddRemoveGold(100);
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // ring return
            id = 23;
            availableDialogIDs = new List<int>() { };  // THIS HERE NEEDS TO BE EMPTIED AND INSERTED BY THIEF DIALOG
            dialogLineID = 22;
            availableDialogLines = new List<int>() { }; // THIS HERE NEEDS TO BE EMPTIED AND INSERTED BY THIEF DIALOG
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                ItemManager.UnequipItem(9);
                ItemManager.RemoveItemFromPosession(9);
                GameState.isMonkQuestComplete = true;
                GameState.isMonkQuestActive = false;
                if (GameState.isShamanQuestComplete && GameState.isBlacksmithQuestComplete && GameState.isMonkQuestComplete)
                {
                    PassInitialFlag("shaman", 31);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 24;
            availableDialogIDs = new List<int>() { 26 };
            dialogLineID = 23;
            availableDialogLines = new List<int>() { 31 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                GameState.AddRemoveDracPower(2);
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 25;
            availableDialogIDs = new List<int>() { 27 };
            dialogLineID = 24;
            availableDialogLines = new List<int>() { 32 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                GameState.AddRemoveDracPower(1);
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 26;
            availableDialogIDs = new List<int>() { 6, 9 };
            dialogLineID = 25;
            availableDialogLines = new List<int>() { 8, 24 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 27;
            availableDialogIDs = new List<int>() { 6, 9 };
            dialogLineID = 26;
            availableDialogLines = new List<int>() { 8, 24 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        dialogDict.Add("monk", dialogOptionsDict);


// THIEF


        dialogOptionsDict = new Dictionary<int, DialogOption>();

        {
            id = 1;
            availableDialogIDs = new List<int>() { 2, 3 };
            dialogLineID = 1;
            availableDialogLines = new List<int>() { 1, 2 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogInitialFlag(true);
            dialogOption.AddAction(() => PassInitialFlag("thief", 4));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 2;
            availableDialogIDs = new List<int>() { 3 };
            dialogLineID = 2;
            availableDialogLines = new List<int>() { 2 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(5); dialogOption.RemoveAvailableDialogLine(3);
                if (GameState.isMonkQuestActive == true)
                {
                    dialogOption.AddNewAvailableDialogID(5); dialogOption.AddNewAvailableDialogLine(3);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {   // normal end
            id = 3;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 4;
            availableDialogIDs = new List<int>() { 3 };
            dialogLineID = 3;
            availableDialogLines = new List<int>() { 2 };
            DialogOption dialogOption = new DialogOption();

            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(5); dialogOption.RemoveAvailableDialogLine(3);
                if (GameState.isMonkQuestActive == true)
                {
                    dialogOption.AddNewAvailableDialogID(5); dialogOption.AddNewAvailableDialogLine(3);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 5;
            availableDialogIDs = new List<int>() { 7 };
            dialogLineID = 4;
            availableDialogLines = new List<int>() { 4 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => PassInitialFlag( "thief", 6));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 6;
            availableDialogIDs = new List<int>() { 8 };
            dialogLineID = 5;
            availableDialogLines = new List<int>() { 5 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 7;
            availableDialogIDs = new List<int>() { 15, 3 };
            dialogLineID = 6;
            availableDialogLines = new List<int>() { 13, 18 };
            DialogOption dialogOption = new DialogOption();

            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(18); dialogOption.RemoveAvailableDialogLine(16);
                if (GameState.rawPower >= 3)
                {
                    dialogOption.AddNewAvailableDialogID(18); dialogOption.AddNewAvailableDialogLine(16);
                }
            });
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(9); dialogOption.RemoveAvailableDialogLine(6);
                if (GameState.rawPower >= 5)
                {
                    dialogOption.AddNewAvailableDialogID(9); dialogOption.AddNewAvailableDialogLine(6);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 8;
            availableDialogIDs = new List<int>() { 15, 3 };
            dialogLineID = 7;
            availableDialogLines = new List<int>() { 13, 18 };
            DialogOption dialogOption = new DialogOption();

            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(18); dialogOption.RemoveAvailableDialogLine(16);
                if (GameState.rawPower >= 3)
                {
                    dialogOption.AddNewAvailableDialogID(18); dialogOption.AddNewAvailableDialogLine(16);
                }
            });
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(9); dialogOption.RemoveAvailableDialogLine(6);
                if (GameState.rawPower >= 5)
                {
                    dialogOption.AddNewAvailableDialogID(9); dialogOption.AddNewAvailableDialogLine(6);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 9;
            availableDialogIDs = new List<int>() { 10 };
            dialogLineID = 8;
            availableDialogLines = new List<int>() { 7 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 10;
            availableDialogIDs = new List<int>() { 11 };
            dialogLineID = 9;
            availableDialogLines = new List<int>() { 8 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 11;
            availableDialogIDs = new List<int>() { 12 };
            dialogLineID = 10;
            availableDialogLines = new List<int>() { 9 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 12;
            availableDialogIDs = new List<int>() { 13 };
            dialogLineID = 11;
            availableDialogLines = new List<int>() { 10 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 13;
            availableDialogIDs = new List<int>() { 14 };
            dialogLineID = 12;
            availableDialogLines = new List<int>() { 11 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // good end
            id = 14;
            availableDialogIDs = new List<int>() { 3 };
            dialogLineID = 13;
            availableDialogLines = new List<int>() { 12 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => PassInitialFlag("thief", 19));
            dialogOption.AddAction(() =>
            {
                dialogDict["monk"][23].AddNewAvailableDialogID(24); dialogDict["monk"][23].AddNewAvailableDialogLine(29);
            });
            dialogOption.AddAction(() => ItemManager.AddItemToPosession(null, ItemManager.ProcureItemData(9, 0, 3, 0)));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 15;
            availableDialogIDs = new List<int>() { 16, 17 };
            dialogLineID = 14;
            availableDialogLines = new List<int>() { 14, 15 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 16;
            availableDialogIDs = new List<int>() { 15, 3 };
            dialogLineID = 15;
            availableDialogLines = new List<int>() { 13, 18 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(18); dialogOption.RemoveAvailableDialogLine(16);
                if (GameState.rawPower >= 3)
                {
                    dialogOption.AddNewAvailableDialogID(18); dialogOption.AddNewAvailableDialogLine(16);
                }
            });
            dialogOption.AddAction(() =>
            {
                dialogOption.RemoveAvailableDialogID(9); dialogOption.RemoveAvailableDialogLine(6);
                if (GameState.rawPower >= 5)
                {
                    dialogOption.AddNewAvailableDialogID(9); dialogOption.AddNewAvailableDialogLine(6);
                }
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // trick end
            id = 17;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            dialogOption.AddAction(() => hiddenCache.SetActive(true));
            dialogOption.AddAction(() =>
            {
                List<TransitionOperator.Action> tranActions = new List<TransitionOperator.Action>()
                        {
                            () => NPCLibrary.npcRefDict["thief"].transform.position = new Vector3(100, 100, 100)
                        };
                transitionManager.InitiateTransition(1f, 1f, tranActions, true);
            });

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // bad end
            id = 18;
            availableDialogIDs = new List<int>() { 3 };
            dialogLineID = 16;
            availableDialogLines = new List<int>() { 17 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() =>
            {
                dialogDict["monk"][23].AddNewAvailableDialogID(25); dialogDict["monk"][23].AddNewAvailableDialogLine(30);
            });
            dialogOption.AddAction(() => ItemManager.AddItemToPosession(null, ItemManager.ProcureItemData(9, 0, 3, 0)));
            dialogOption.AddAction(() => NPCLibrary.npcRefDict["thief"].GetComponent<NPCBehaviour>().SetNpcDead(true));
            dialogOption.AddAction(() => NPCLibrary.npcRefDict["thief"].GetComponent<NPCBehaviour>().NPCDropItem(ItemManager.ProcureItemData(1, 200, 0, 0)));

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 19;
            availableDialogIDs = new List<int>() { 3 };
            dialogLineID = 17;
            availableDialogLines = new List<int>() { 19 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }


        dialogDict.Add("thief", dialogOptionsDict);

// DOOR

        dialogOptionsDict = new Dictionary<int, DialogOption>();

        {
            id = 1;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 1;
            availableDialogLines = new List<int>() { 1 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogInitialFlag(true);

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        {
            id = 2;
            availableDialogIDs = new List<int>() { 3, 4 };
            dialogLineID = 2;
            availableDialogLines = new List<int>() { 2, 1 };
            DialogOption dialogOption = new DialogOption();

            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }

        { // pass
            id = 3;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            dialogOption.SetDialogTransition(true);
            dialogOption.AddAction(() =>
            {
                List<TransitionOperator.Action> tranActions = new List<TransitionOperator.Action>()
                        {
                            () => GameObject.Find("Player").transform.position = new Vector3(155f, -17f, -5f)
                        };
              
                transitionManager.InitiateTransition(3f, 2f, tranActions, true);
            });

            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }
        { // leave
            id = 4;
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogEndFlag(true);
            SetCommonData(id, dialogOption);
            dialogOptionsDict.Add(id, dialogOption);
        }

        dialogDict.Add("gate", dialogOptionsDict);

    }

    private static void LoadDialogLineDocuments()
    {
        //playerLinesDoc = new XmlDocument();
        //playerLinesDoc.Load("Resources/Assets/DialogFiles/PlayerLines.xml");
        TextAsset textAsset = (TextAsset)Resources.Load("DialogFiles/PlayerLines");
        playerLinesDoc = new XmlDocument();
        playerLinesDoc.LoadXml(textAsset.text);

        //npcLinesDoc = new XmlDocument();
       // npcLinesDoc.Load("Resources/Assets/DialogFiles/NPCLines.xml");
        TextAsset textAssetB = (TextAsset)Resources.Load("DialogFiles/NPCLines");
        npcLinesDoc = new XmlDocument();
        npcLinesDoc.LoadXml(textAssetB.text);
    }

    private static Dictionary<int, string> GetPlayerLinesOfName(string npcID)
    {
        XmlNodeList nodes = playerLinesDoc.SelectNodes("/playerdialoglines/character[@name='" + npcID + "']/line");
        return GetLines(nodes);
    }

    private static Dictionary<int, string> GetNPCLinesOfName(string npcID)
    {
        XmlNodeList nodes = npcLinesDoc.SelectNodes("/npcdialoglines/character[@name='" + npcID + "']/line");
        return GetLines(nodes);
    }

    private static Dictionary<int, string> GetLines(XmlNodeList nodes)
    {
        Dictionary<int, string> dialogueLines = new Dictionary<int, string>();
        foreach (XmlNode node in nodes)
        {
            int id = int.Parse(node.SelectSingleNode("id").InnerText);
            string text = node.SelectSingleNode("text").InnerText;
            dialogueLines.Add(id, text);
        }

        return dialogueLines;
    }

}
