using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class NamePlateOperator : MonoBehaviour
{
    [SerializeField]
    private GameObject panelObj;
    [SerializeField]
    private GameObject textObj;
    private TextMeshProUGUI textMesh;
    private Image image;

    public void Setup()
    {
        textMesh = textObj.GetComponent<TextMeshProUGUI>();
        image = panelObj.GetComponent<Image>();
    }

    public void SetName(string name)
    {
        textMesh.text = name;
    }

    public void SetHighlight() 
    {
        Color color = Color.green;
        color.a = 0.4f;
        image.color = color;
    }

    public void RemoveHighlight()
    {

        Color color = Color.white;
        color.a = 0.4f;
        image.color = color;

    }

    public void ShowNamePlate()
    {
        panelObj.SetActive(true);
    }

    public void HideNamePlate()
    {
        panelObj.SetActive(false);
    }

    public void ToggleActive(bool isActive)
    {
        panelObj.SetActive(isActive);
    }
}
