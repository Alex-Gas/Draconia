using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NPCLibrary
{
    public static Dictionary<string, GameObject> npcRefDict = new Dictionary<string, GameObject>();

    public static void PrepareNPCs()
    {
        GameObject npcObj;
        npcObj = GameObject.Find("NPC_test");
        npcRefDict["npc_test"] = npcObj;

        npcObj = GameObject.Find("Shaman");
        npcRefDict["shaman"] = npcObj;

        npcObj = GameObject.Find("Blacksmith");
        npcRefDict["blacksmith"] = npcObj;

        npcObj = GameObject.Find("Monk");
        npcRefDict["monk"] = npcObj;

        npcObj = GameObject.Find("Thief");
        npcRefDict["thief"] = npcObj;

        npcObj = GameObject.Find("Knight");
        npcRefDict["knight"] = npcObj;

        npcObj = GameObject.Find("Plants");
        npcRefDict["plants"] = npcObj;
    }
}
