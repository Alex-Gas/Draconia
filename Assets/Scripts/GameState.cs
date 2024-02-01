using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameState
{

    public static bool isPaused = false;

    public static int goldTotalAmount = 0;
    public static int rawPower = 0;
    public static int draconicPower = 0;

    public static bool isTheCheckPassed;
    public static bool isNPCAnnoyed;


    public static bool isShamanQuestComplete = false;
    public static bool isBlacksmithQuestComplete = false;
    public static bool isMonkQuestComplete = false;
    public static bool hasReceivedMainquest = false;
    public static bool isRitualPerformed = false;
    public static bool isBlacksmithQuestActive = false;
    public static bool isMonkQuestActive = false;

    public static bool isWorstEnding;     //death
    public static bool isGoodEnding;    //reclaim
    public static bool isEvilEnding;    //join
    public static bool isNeutralEnding; //kill
    public static bool isBadEnding;     //leave
    public static bool isPerfectEnding; //transform


    public static void AddRemoveGold(int goldAmount)
    {
        goldTotalAmount += goldAmount;
        Debug.Log("total gold: " + goldTotalAmount);
    }

    public static void AddRemoveRawPower(int RpAmount)
    {
        rawPower += RpAmount;
        Debug.Log("RP: " + rawPower);
    }

    public static void AddRemoveDracPower(int DpAmount)
    {
        draconicPower += DpAmount;
        AddRemoveRawPower(DpAmount);
        Debug.Log("DP: " + draconicPower);
    }



}
