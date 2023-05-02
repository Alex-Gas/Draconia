using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOperator : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    private List<GameObject> buttonList;
    List<ItemData> itemsInPosession;


    public void Setup(InventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;

        buttonList = new List<GameObject>()
        {
            button1, button2, button3, button4, button5, button6
        };
    }

    public void DisableButtons()
    {
        foreach (GameObject button in buttonList)
        {
            button.SetActive(false);
        }
    }

    public void RedrawInventory()
    {
        DisableButtons();
        SetItems();
    }

    public void SetItems()
    {
        itemsInPosession = ItemManager.itemsInPosession;
        for (int i = 0; i < itemsInPosession.Count; i++)
        {
            SetButton(i);
        }
    }


    private void SetActiveHighlight(bool isActive, GameObject button)
    {
        Image parentImage = button.transform.parent.GetComponent<Image>();
        if (isActive)
        {
            parentImage.color = Color.red;
        }
        else
        {
            parentImage.color = Color.gray;
        }
    }

    private void SetButton(int i)
    {
        GameObject button = buttonList[i];

        UnityEngine.UI.Button btncomp = button.GetComponent<UnityEngine.UI.Button>();
        btncomp.onClick.RemoveAllListeners();
        btncomp.onClick.AddListener(() => inventoryManager.SlotClick(i));

        SetText(button, itemsInPosession[i].GetItemObject().name);

        SetActiveHighlight(itemsInPosession[i].IsItemActive(), button);

        button.SetActive(true);
    }



    private void SetText(GameObject obj, string text)
    {
        GameObject textEle = obj.transform.Find("Text").gameObject;
        TextMeshProUGUI textMesh = textEle.GetComponent<TextMeshProUGUI>();
        textMesh.text = text;
    }


}


