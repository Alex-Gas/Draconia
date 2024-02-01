using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public static class CutsceneLibrary
{

    private static XmlDocument cutsceneLinesDoc;
    private static Dictionary<string, Cutscene> cutsceneDict;
    private static DialogManager dialogManager;
    private static EndManager endManager;

    public static void PrepareCutscenes()
    {
        CreateCutsceneDictionary();
        LoadDialogLineDocuments();
        LoadReferences();
    }

    private static void LoadReferences()
    {
        dialogManager = GameObject.Find("UI").GetComponent<DialogManager>();
        endManager = GameObject.Find("UI").GetComponent <EndManager>();
    }

    private static void LoadDialogLineDocuments()
    {
        //cutsceneLinesDoc = new XmlDocument();
        //cutsceneLinesDoc.Load("Assets/Resources/DialogFiles/CutsceneLines.xml");
        // if your original XML file is located at
        // "Ressources/MyXMLFile.xml"
        TextAsset textAsset = (TextAsset)Resources.Load("DialogFiles/CutsceneLines");
        cutsceneLinesDoc = new XmlDocument();
        cutsceneLinesDoc.LoadXml(textAsset.text);


    }

    public static Cutscene GetCutscenes(string cutsceneID)
    {
        return cutsceneDict[cutsceneID];
    }

    public static Dictionary<int, string> GetCutsceneLines(string cutsceneID)
    {
        return GetCutsceneLinesOfID(cutsceneID);
    }


    private static Dictionary<int, string> GetCutsceneLinesOfID(string cutsceneID)
    {
        XmlNodeList nodes = cutsceneLinesDoc.SelectNodes("/cutscenelines/cutscene[@name='" + cutsceneID + "']/line");
        return GetLines(nodes);
    }

    private static Dictionary<int, string> GetLines(XmlNodeList nodes)
    {
        Dictionary<int, string> cutsceneLines = new Dictionary<int, string>();
        foreach (XmlNode node in nodes)
        {
            int id = int.Parse(node.SelectSingleNode("id").InnerText);
            string text = node.SelectSingleNode("text").InnerText;
            cutsceneLines.Add(id, text);
        }

        return cutsceneLines;
    }

    private static void CreateCutsceneDictionary()
    {
        int id;
        List<int> cutsceneLineIDs;

        cutsceneDict = new Dictionary<string, Cutscene>();


        {
            id = 1;
            cutsceneLineIDs = new List<int>() { 1, 2 };
            Cutscene cutscene = new Cutscene();
            cutscene.SetCutsceneID(id);
            cutscene.SetCutsceneLineIDs(cutsceneLineIDs);
            cutscene.SetCutsceneStartBlack(true);
            cutscene.SetCutsceneFinalWorld(false);
            cutscene.AddAction(() => dialogManager.InitiateDialog("shaman"));
            cutsceneDict.Add("intro", cutscene);
        }

        {
            id = 2;
            cutsceneLineIDs = new List<int>() { 1, 2 };
            Cutscene cutscene = new Cutscene();
            cutscene.SetCutsceneID(id);
            cutscene.SetCutsceneLineIDs(cutsceneLineIDs);
            cutscene.SetCutsceneEndBlack(true);
            cutscene.SetCutsceneFinalWorld(true);
            cutscene.AddAction(() => endManager.InitiateEnd());
            cutsceneDict.Add("dieending", cutscene);
        }

        {
            id = 3;
            cutsceneLineIDs = new List<int>() { 1, 2 };
            Cutscene cutscene = new Cutscene();
            cutscene.SetCutsceneID(id);
            cutscene.SetCutsceneLineIDs(cutsceneLineIDs);
            cutscene.SetCutsceneEndBlack(true);
            cutscene.SetCutsceneFinalWorld(true);
            cutscene.AddAction(() => endManager.InitiateEnd());
            cutsceneDict.Add("leaveending", cutscene);
        }

        {
            id = 4;
            cutsceneLineIDs = new List<int>() { 1, 2 };
            Cutscene cutscene = new Cutscene();
            cutscene.SetCutsceneID(id);
            cutscene.SetCutsceneLineIDs(cutsceneLineIDs);
            cutscene.SetCutsceneEndBlack(true);
            cutscene.SetCutsceneFinalWorld(true);
            cutscene.AddAction(() => endManager.InitiateEnd());
            cutsceneDict.Add("joinending", cutscene);
        }

        {
            id = 5;
            cutsceneLineIDs = new List<int>() { 1, 2, 3 };
            Cutscene cutscene = new Cutscene();
            cutscene.SetCutsceneID(id);
            cutscene.SetCutsceneLineIDs(cutsceneLineIDs);
            cutscene.SetCutsceneEndBlack(true);
            cutscene.SetCutsceneFinalWorld(true);
            cutscene.AddAction(() => endManager.InitiateEnd());
            cutsceneDict.Add("killending", cutscene);
        }

        {
            id = 6;
            cutsceneLineIDs = new List<int>() { 1, 2 };
            Cutscene cutscene = new Cutscene();
            cutscene.SetCutsceneID(id);
            cutscene.SetCutsceneLineIDs(cutsceneLineIDs);
            cutscene.SetCutsceneEndBlack(true);
            cutscene.SetCutsceneFinalWorld(true);
            cutscene.AddAction(() => endManager.InitiateEnd());
            cutsceneDict.Add("victoryending", cutscene);
        }
        {
            id = 7;
            cutsceneLineIDs = new List<int>() { 1, 2, 3 };
            Cutscene cutscene = new Cutscene();
            cutscene.SetCutsceneID(id);
            cutscene.SetCutsceneLineIDs(cutsceneLineIDs);
            cutscene.SetCutsceneEndBlack(true);
            cutscene.SetCutsceneFinalWorld(true);
            cutscene.AddAction(() => endManager.InitiateEnd());
            cutsceneDict.Add("perfectending", cutscene);
        }

    }
}
