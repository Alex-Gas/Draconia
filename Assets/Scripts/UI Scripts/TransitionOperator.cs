using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionOperator : MonoBehaviour
{
    private TransitionManager transitionManager;
    [SerializeField]
    private GameObject fadeScreen;
    private Image fadeImage;

    public delegate void Action();
    private List<Action> actions = new List<Action>() { };

    public void Setup(TransitionManager transitionManager)
    {
        this.transitionManager = transitionManager;
        fadeImage = this.fadeScreen.GetComponent<Image>();
    }

    public void ActionSetup(List<Action> actions)
    {
        if(actions != null)
        {
            this.actions = actions;
        }
    }

    public void ExecuteTransition(float fadeDuration, float delayDuration)
    {
        float startAlpha = 0;
        float endAlpha = 1;

        {
            StartCoroutine(Fade());
        }

        IEnumerator Fade()
        {
            // Set the starting alpha value of the panel
            Color startColor = fadeImage.color;
            startColor.a = startAlpha;
            fadeImage.color = startColor;

            // Fade the panel in over the specified duration
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
                Color newColor = fadeImage.color;
                newColor.a = alpha;
                fadeImage.color = newColor;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Keep the panel fully visible for the specified duration
            yield return new WaitForSeconds(delayDuration);
            ExecuteActions();

            // Fade the panel back out over the specified duration
            elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(endAlpha, startAlpha, elapsedTime / fadeDuration);
                Color newColor = fadeImage.color;
                newColor.a = alpha;
                fadeImage.color = newColor;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Set the final alpha value of the panel
            Color endColor = fadeImage.color;
            endColor.a = startAlpha;
            fadeImage.color = endColor;

            transitionManager.CloseTransition();
        }
    }

    public void ExecuteActions()
    {
        foreach (Action action in actions)
        {
            action();
        }
    }
}
