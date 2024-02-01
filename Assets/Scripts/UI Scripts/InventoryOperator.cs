using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryOperator : MonoBehaviour
{
    private InventoryManager inventoryManager;

    public GameObject itemNameElement;
    public GameObject itemDescriptionElement;
    public GameObject itemStatisticsElement;
    
    public GameObject PlayerGoldElement;
    public GameObject PlayerRPElement;
    public GameObject PlayerDPElement;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;
    public GameObject button10;
    public GameObject button11;
    public GameObject button12;
    public GameObject button13;
    public GameObject button14;
    public GameObject button15;
    public GameObject button16;
    public GameObject button17;
    public GameObject button18;
    private List<GameObject> buttonList;
    List<ItemData> itemsInPosession;


    public void Setup(InventoryManager inventoryManager)
    {
        this.inventoryManager = inventoryManager;

        buttonList = new List<GameObject>()
        {
            button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18
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
        ShowPlayerStats();
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
            parentImage.color = new Color32(190, 174, 114, 255);
        }
        else
        {
            parentImage.color = new Color32(72, 72, 72, 255);
        }
    }

    private void SetButton(int i)
    {
        GameObject button = buttonList[i];

        UnityEngine.UI.Button btncomp = button.GetComponent<UnityEngine.UI.Button>();
        btncomp.onClick.RemoveAllListeners();
        btncomp.onClick.AddListener(() => inventoryManager.SlotClick(i));

        EventTrigger eventTrigger = btncomp.gameObject.GetComponent<EventTrigger>();

        EventTrigger.Entry triggerEnter = new EventTrigger.Entry();
        triggerEnter.eventID = EventTriggerType.PointerEnter;
        triggerEnter.callback.AddListener((BaseEventData ev) => inventoryManager.SlotHover(i, true));
        eventTrigger.triggers.Add(triggerEnter);

        EventTrigger.Entry triggerExit = new EventTrigger.Entry();
        triggerExit.eventID = EventTriggerType.PointerExit;
        triggerExit.callback.AddListener((BaseEventData ev) => inventoryManager.SlotHover(i, false));
        eventTrigger.triggers.Add(triggerExit);

        SetImage(button, itemsInPosession[i].GetItemIconPath());

        SetActiveHighlight(itemsInPosession[i].IsItemActive(), button);

        button.SetActive(true);
    }


    private void SetImage(GameObject obj, string iconPath)
    {
        GameObject imageEle = obj.transform.Find("Image").gameObject;
        Image imageComp = imageEle.GetComponent<Image>();
        Texture2D iconTexture = Resources.Load<Texture2D>(iconPath);

        imageComp.sprite = Sprite.Create(iconTexture, new Rect(0, 0, iconTexture.width, iconTexture.height), Vector2.zero);
    }


    public void ShowItemDescription(int slotNo)
    {
        string itemName = itemsInPosession[slotNo].GetItemName();
        string itemDescription = itemsInPosession[slotNo].GetItemDescription();
        int rawPower = itemsInPosession[slotNo].GetRawPower();
        string rawPowerTxt = rawPower.ToString();

        TextMeshProUGUI textMesh;

        textMesh = itemNameElement.GetComponent<TextMeshProUGUI>();
        textMesh.text = itemName;
        
        textMesh = itemDescriptionElement.GetComponent<TextMeshProUGUI>();
        textMesh.text = itemDescription;

        if (rawPower > 0)
        {
            textMesh = itemStatisticsElement.GetComponent<TextMeshProUGUI>();
            textMesh.text = "Item Power: " + rawPowerTxt;
        }
    }

    public void HideItemDesctiption()
    {
        TextMeshProUGUI textMesh;

        textMesh = itemNameElement.GetComponent<TextMeshProUGUI>();
        textMesh.text = "";

        textMesh = itemDescriptionElement.GetComponent<TextMeshProUGUI>();
        textMesh.text = "";

        textMesh = itemStatisticsElement.GetComponent<TextMeshProUGUI>();
        textMesh.text = "";
    }

    public void ShowPlayerStats()
    {
        TextMeshProUGUI textMesh;

        textMesh = PlayerGoldElement.GetComponent<TextMeshProUGUI>();
        textMesh.text = "Shards: " + GameState.goldTotalAmount;

        textMesh = PlayerRPElement.GetComponent<TextMeshProUGUI>();
        textMesh.text = "Raw Power: " + GameState.rawPower;

        textMesh = PlayerDPElement.GetComponent<TextMeshProUGUI>();
        textMesh.text = "Draconic Power: " + GameState.draconicPower;
    }
}


