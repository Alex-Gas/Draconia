using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneOperator : MonoBehaviour
{

    CutsceneManager cutsceneManager;
    [SerializeField]
    private GameObject slide;

    public void Setup(CutsceneManager cutsceneManager)
    {
        this.cutsceneManager = cutsceneManager;
    }

    public void ShowSlide(string line = "")
    {
        SetText(slide, line);
        slide.SetActive(true);
    }


    public void HideSlide()
    {
        slide.SetActive(false);
    }

    private void SetText(GameObject obj, string text)
    {
        GameObject textEle = obj.transform.Find("Text").gameObject;
        TextMeshProUGUI textMesh = textEle.GetComponent<TextMeshProUGUI>();
        textMesh.text = text;
    }

}
