using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : UIManager
{
    private GameObject uiContainer;
    public GameObject transitionUIpref;
    private GameObject transitionUI;
    private TransitionOperator transitionOperator;
    public bool isTransitionIndependent = false;


    private void Start()
    {
        uiContainer = GameObject.Find("UI");
    }

    protected override void LoadUI()
    {
        transitionUI = Instantiate(transitionUIpref, uiContainer.transform);
        transitionOperator = transitionUI.GetComponent<TransitionOperator>();
        transitionOperator.Setup(this);
    }
    
    protected override void TranActionSetup(List<TransitionOperator.Action> actionsList, bool isTransitionIndependent)
    {
        transitionOperator.ActionSetup(actionsList);
        this.isTransitionIndependent = isTransitionIndependent;
    }

    protected override void StartTransition(float fadeDuration, float delayDuration)
    {
        transitionOperator.ExecuteTransition(fadeDuration, delayDuration);
    }

    public void CloseTransition()
    {

        if (isTransitionIndependent)
        {
            isTransitionIndependent = false;
            GameState.isPaused = false;
        }

        isTransitioning = false;
        Destroy(transitionUI);
    }
}
