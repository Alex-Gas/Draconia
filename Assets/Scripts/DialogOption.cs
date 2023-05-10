using System;
using System.Collections.Generic;


public class DialogOption
{
    private int dialogID = 0;
    private List<int> availableDialogIDs = new List<int>() { };
    private int dialogLineID = 0;
    private List<int> availableDialogLines = new List<int>() { };
    public delegate void Action();
    public bool isPowerCheck;
    private int checkResult;
    public bool isDialogEnd = false;
    private bool isDialogInitial = false;
    private bool isTransition = false;
    private List<Action> actions = new List<Action>();

    public int GetDialogID() { return dialogID; }
    public void SetDialogID(int dialogID) { this.dialogID = dialogID; }
    public List<int> GetAvailableDialogIDs() { return availableDialogIDs; }
    public void SetAvailableDialogIDs(List<int> availableDialogIDs) { this.availableDialogIDs = availableDialogIDs; }
    public int GetDialogLineID() { return dialogLineID; }
    public void SetDialogLineID(int dialogLineID) { this.dialogLineID = dialogLineID; }    
    public List<int> GetAvailableDialogLines() { return availableDialogLines; }
    public void SetAvailableDialogLines(List<int> availableDialogLines) { this.availableDialogLines = availableDialogLines; }
    public bool IsDialogEnd() { return isDialogEnd; }
    public void SetDialogEndFlag(bool isDialogEnd) { this.isDialogEnd = isDialogEnd; }
    public bool IsPowerCheck() { return isPowerCheck; }
    public void SetPowerCheckFlag(bool isPowerCheck) { this.isPowerCheck = isPowerCheck; }
    public int GetCheckResult() { return checkResult; }
    public void SetCheckResult(int checkResult) { this.checkResult = checkResult; }
    public bool IsDialogInitial() { return isDialogInitial; }
    public void SetDialogInitialFlag(bool isDialogInitial) { this.isDialogInitial = isDialogInitial; }
    public bool IsDialogTransition() { return isTransition; }
    public void SetDialogTransition(bool isTransition) { this.isTransition = isTransition; }


    public void AddNewAvailableDialogID(int dialogID) 
    {
        this.availableDialogIDs.Add(dialogID);
    }

    public void RemoveAvailableDialogID(int dialogID)
    {
        this.availableDialogIDs.Remove(dialogID);
    }

    public void AddNewAvailableDialogLine(int dialogLineID)
    {
        this.availableDialogLines.Add(dialogLineID);
    }

    public void RemoveAvailableDialogLine(int dialogLineID)
    {
        this.availableDialogLines.Remove(dialogLineID);
    }

    public bool IsCheckPassed(int rawPower, int draconicPower )
    {
        if (GameState.rawPower >= rawPower && GameState.draconicPower >= draconicPower)
        {
            return true;
        }

        return false;
    }


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
