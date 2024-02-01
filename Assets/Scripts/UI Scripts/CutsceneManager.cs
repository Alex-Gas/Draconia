using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : UIManager
{
    private Dictionary<int, string> cutsceneLinesDict;
    private Cutscene cutscene;
    private int slidesAmount;
    private GameObject currentSlideID;
    private int cutsceneNo;

    public GameObject cutsceneUIPref;
    private GameObject cutsceneUI;
    private GameObject uiContainer;
    private CutsceneOperator cutsceneOperator;
    private TransitionManager transitionManager;
    private DialogManager dialogManager;

    private float fadeDuration;
    private float delayDuration;

    private float slideShowingTime = 3f;

    private void Start()
    {
        uiContainer = GameObject.Find("UI");

        CutsceneLibrary.PrepareCutscenes();
    }


    protected override void LoadCutscene(string cutsceneID, float fadeDuration, float delayDuration, float slideShowingTime)
    {
        cutsceneLinesDict = CutsceneLibrary.GetCutsceneLines(cutsceneID);
        cutscene = CutsceneLibrary.GetCutscenes(cutsceneID);
        cutsceneNo = 1;
        transitionManager = GetComponent<TransitionManager>();
        dialogManager = GetComponent<DialogManager>();

        this.fadeDuration = fadeDuration;
        this.delayDuration = delayDuration;
        this.slideShowingTime = slideShowingTime;


    }

    protected override void LoadUI()
    {
        cutsceneUI = Instantiate(cutsceneUIPref, uiContainer.transform);
        cutsceneOperator = cutsceneUI.GetComponent<CutsceneOperator>();
        cutsceneOperator.Setup(this);
    }


    protected override void StartCutscene(string cutsceneID)
    {
        cutsceneOperator.HideSlide();

        if (cutscene.IsCutsceneStartBlack())
        {
            // SHOW EMPTY SLIDE
            cutsceneOperator.ShowSlide(); 
        }
        CycleSlide();
    }



    private void CycleSlide()
    {

        float time = (fadeDuration * 2) + delayDuration + slideShowingTime;

        if(cutsceneNo <= cutscene.GetCutsceneLineIDs().Count)
        {
            string cutsceneLine = cutsceneLinesDict[cutsceneNo];
            cutsceneNo++;
            // change slide action added to transition
            List<TransitionOperator.Action> tranActions = new List<TransitionOperator.Action>() { () => { cutsceneOperator.ShowSlide(cutsceneLine);}};
            transitionManager.InitiateTransition(fadeDuration, delayDuration, tranActions, false);
            SlideDelay(1, time);
        }

        else
        {
            if (cutscene.IsCutsceneEndBlack())
            {
                List<TransitionOperator.Action> tranActions = new List<TransitionOperator.Action>() { () => { cutsceneOperator.ShowSlide(); } };

                transitionManager.InitiateTransition(fadeDuration, delayDuration, tranActions, false);
                SlideDelay(2);
            }

            else
            {
                List<TransitionOperator.Action> tranActions = new List<TransitionOperator.Action>() { () => { cutsceneOperator.HideSlide(); } };

                transitionManager.InitiateTransition(fadeDuration, delayDuration, tranActions, false);
                SlideDelay(2);
            }
        }      
    }


    public void SlideDelay(int mode, float time = 4)
    {
        {
            StartCoroutine(Delay());
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(time);

            if (mode == 1)
            {
                CycleSlide();
            }

            else if (mode == 2)
            {
                isUIopen = false;
                cutscene.ExecuteActions();
                CloseCutscene();
            }
        }
    }


    public void CloseCutscene()
    {
        Destroy(cutsceneUI);
    }
}
