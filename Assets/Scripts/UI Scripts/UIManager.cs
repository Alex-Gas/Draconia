using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static bool isUIopen = false;
    public static bool isTransitioning = false;

    protected virtual void LoadUI() { }
    protected virtual void LoadDialog(string npcID) { }
    protected virtual void StartDialog(string npcID) { }
    protected virtual void ShowItems() { }
    protected virtual void StartTransition(float fadeDuration, float delayDuration) { }
    protected virtual void TranActionSetup(List<TransitionOperator.Action> actionsList = null, bool isTransitionIndependent = false) { }
    protected virtual void OpenStartScreen() { }
    protected virtual void OpenMenuScreen() { }
    protected virtual void OpenEndScreen() { }
    protected virtual void StartCutscene(string cutsceneID) { }
    protected virtual void LoadCutscene(string cutsceneID, float fadeDuration, float delayDuration, float slideShowingTime) { }


    public void InitiateDialog(string npcID)
    {
        if (!isUIopen)
        {
            isUIopen = true;
            DialogManager.isDialogOpen = true;
            GameState.isPaused = true;
            LoadDialog(npcID);
            LoadUI();
            StartDialog(npcID);
        }
    }

    public void InitiateInventory()
    {
        if (!isUIopen && !isTransitioning)
        {
            isUIopen = true;
            InventoryManager.isInventoryOpen = true;
            GameState.isPaused = true;
            LoadUI();
            ShowItems();
        }
    }

    public void InitiateTransition(float fadeDuration, float delayDuration, List<TransitionOperator.Action> actionsList = null, bool isTransitionIndependent = false)
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            GameState.isPaused = true;
            LoadUI();
            TranActionSetup(actionsList, isTransitionIndependent);
            StartTransition(fadeDuration, delayDuration);
        }
    }

    public void InitiateStart()
    {
        isUIopen = true;
        GameState.isPaused = true;
        LoadUI();
        OpenStartScreen();
    }

    public void InitiateMenu()
    {
        if (!isUIopen && !isTransitioning)
        {
            isUIopen = true;
            MenuManager.isMenuOpen = true;
            GameState.isPaused = true;
            LoadUI();
            OpenMenuScreen();
        }
    }

    public void InitiateEnd()
    {
        isUIopen = true;
        GameState.isPaused = true;
        LoadUI();
        OpenEndScreen();
    }

    public void InitiateCutscene(string cutsceneID, float fadeDuration, float delayDuration, float slideShowingTime)
    {
        isUIopen = true;
        GameState.isPaused = true;
        LoadCutscene(cutsceneID, fadeDuration, delayDuration, slideShowingTime);
        LoadUI();
        StartCutscene(cutsceneID);
    }
}
