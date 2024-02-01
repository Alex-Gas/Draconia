using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene
{
    public delegate void Action();
    private int cutsceneID;
    private List<int> cutsceneLineIDs;
    // is cutscene gonna end with fade into the game world
    private bool isCutsceneFinalWorld;
    private bool isCutsceneStartBlack = false;
    private bool isCutsceneEndBlack = false;
    private List<Action> actions = new List<Action>();


    public int GetCutsceneID() { return cutsceneID; }
    public void SetCutsceneID(int cutsceneID) { this.cutsceneID = cutsceneID; }
    public List<int> GetCutsceneLineIDs() { return cutsceneLineIDs; }
    public void SetCutsceneLineIDs(List<int> cutsceneLineIDs) { this.cutsceneLineIDs = cutsceneLineIDs; }
    public bool IsCutsceneFinalWorld() {  return isCutsceneFinalWorld; }
    public void SetCutsceneFinalWorld(bool isCutsceneFinalWorld) { this.isCutsceneFinalWorld = isCutsceneFinalWorld; }
    public bool IsCutsceneStartBlack() { return isCutsceneStartBlack; }
    public void SetCutsceneStartBlack(bool isCutsceneStartBlack) { this.isCutsceneStartBlack = isCutsceneStartBlack; }
    public bool IsCutsceneEndBlack() { return isCutsceneEndBlack; }
    public void SetCutsceneEndBlack(bool isCutsceneEndBlack) { this.isCutsceneEndBlack = isCutsceneEndBlack; }


    public void AddAction(Action action)
    {
        actions.Add(action);
    }

    public void ExecuteActions()
    {
        foreach (Action action in actions)
        {
            action();
        }
    }
}
