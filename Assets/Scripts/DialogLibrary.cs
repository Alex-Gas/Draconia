using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking.Types;

public static class DialogLibrary
{
    private static Dictionary<int, DialogOption> dialogOptionsDict;
    private static Dictionary<string, Dictionary<int, DialogOption>> dialogDict;
    private static XmlDocument playerLinesDoc;
    private static XmlDocument npcLinesDoc;


    public static void PrepareDialog()
    {
        CreateDialogDictionary();
        LoadDialogLineDocuments();
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

        //---------

        dialogOptionsDict = new Dictionary<int, DialogOption>();

        {
            id = 1;
            availableDialogIDs = new List<int>() { 2, 3, 10 };
            dialogLineID = 1;
            availableDialogLines = new List<int>() { 2, 3, 5 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.SetDialogInitialFlag(true);
            dialogOption.AddAction(() => Debug.Log("Selected Option 1"));
            dialogOption.AddAction(() => { if (GameState.isTheCheckPassed == true) { dialogOption.SetAvailableDialogIDs(new List<int>() { 8, 3, 10 }); } });
            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 2;
            availableDialogIDs = new List<int>() { 5 };
            dialogLineID = 2;
            availableDialogLines = new List<int>() { 1 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => Debug.Log("Selected Option 2"));
            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 3;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 3;
            availableDialogLines = new List<int>() { 4 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => Debug.Log("Selected Option 3"));
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
            dialogOption.AddAction(() => Debug.Log("Check Passed!"));
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
            dialogOption.AddAction(() => Debug.Log("Check Failed!"));
            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 8;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 6;
            availableDialogLines = new List<int>() { 3 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => Debug.Log("Check Already Done!"));
            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 9;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 7;
            availableDialogLines = new List<int>() { 4 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => Debug.Log("NPC Doesnt like you anymore"));
            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }
        {
            id = 10;
            availableDialogIDs = new List<int>() { 4 };
            dialogLineID = 8;
            availableDialogLines = new List<int>() { 4 };
            DialogOption dialogOption = new DialogOption();
            dialogOption.AddAction(() => Debug.Log("NPC Disliked that!"));
            dialogOption.AddAction(() => PassInitialFlag("blacksmith", 9));
            SetCommonData(id, dialogOption, availableDialogIDs, dialogLineID, availableDialogLines);
            dialogOptionsDict.Add(id, dialogOption);
        }




        dialogDict.Add("blacksmith", dialogOptionsDict);

        //---------

    }

    private static void LoadDialogLineDocuments()
    {
        playerLinesDoc = new XmlDocument();
        playerLinesDoc.Load("Assets/DialogFiles/PlayerLines.xml");

        npcLinesDoc = new XmlDocument();
        npcLinesDoc.Load("Assets/DialogFiles/NPCLines.xml");
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
