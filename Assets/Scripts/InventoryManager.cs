using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemLibrary;

public class InventoryManager : MonoBehaviour
{
    private GameObject uiContainer;
    public GameObject inventoryUIpref;
    private GameObject inventoryUI;
    private InventoryOperator inventoryOperator;

    public bool isInventoryOpen = false;

    //TEST
    List<int> startingItemsList = new List<int>() { 1, 2, 3 };

    private void Start()
    {
        uiContainer = GameObject.Find("UI");

        ItemLibrary.PrepareItems();

        // TEST
        //ItemManager.AddStartingItems(startingItemsList);
    }

    private void Update()
    {
        CheckFlags();
    }

    private void CheckFlags()
    {
        if (ItemManager.redrawFlag == true)
        {
            ItemManager.redrawFlag = false;
            inventoryOperator.RedrawInventory();
        }
    }

    public void ToggleInventory()
    {
        if (!isInventoryOpen)
        {
            InitiateInventory();
        }
        else if (isInventoryOpen)
        {
            CloseInventory();
        }
    }

    private void InitiateInventory()
    {
        isInventoryOpen = true;

        LoadUI();
        ShowItems();
    }

    private void LoadUI()
    {
        inventoryUI = Instantiate(inventoryUIpref, uiContainer.transform);
        inventoryOperator = inventoryUI.GetComponent<InventoryOperator>();
        inventoryOperator.Setup(this);
    }

    // Runs when player clicks on inventory slot occupied by an item
    public void SlotClick(int slotNo)
    {
        ItemData itemData = ItemManager.itemsInPosession[slotNo];

        HandleItemType(itemData);
    }

    // Decides what happens with the item based on its type
    private void HandleItemType(ItemData itemData)
    {
        ItemType itemType = itemData.GetItemType();

        bool isConsumable = itemType == ItemType.Consumable;
        bool isMainHand = itemType == ItemType.MainHand;
        bool isOffHand = itemType == ItemType.OffHand;
        bool isArmor = itemType == ItemType.Armor;
        bool isNecklace = itemType == ItemType.Necklace;
        bool isRing = itemType == ItemType.Ring;

        if (isConsumable)
        {
            itemData.ExecuteActions();
        }

        else if (isMainHand || isOffHand || isArmor || isNecklace || isRing)
        {
            ToggleItemEquip(itemData);
        }

    }
    
    // Toggles equippable items and while handling items of the same type
    private void ToggleItemEquip(ItemData itemData)
    {
        if (itemData.IsItemActive() == false)
        {
            DisableAllOfType(itemData);
            itemData.ExecuteActions();
            itemData.SetItemActive(true);
            inventoryOperator.RedrawInventory();
        }
        else if (itemData.IsItemActive() == true)
        {
            itemData.ExecuteActions();
            itemData.SetItemActive(false);
            inventoryOperator.RedrawInventory();
        }
    }

    // Disables all items of the same type while executing actions that come with disabling them
    private void DisableAllOfType(ItemData itemData)
    {
        foreach (ItemData item in ItemManager.itemsInPosession)
        {
            // If the item is of the same type as the clicked item disable it
            // Additionally if it isnt the same item and its also active - execute action that is related to disabling the item
            if (item.GetItemType() == itemData.GetItemType())
            {
                if (item != itemData && item.IsItemActive())
                {
                    item.ExecuteActions();
                }

                item.SetItemActive(false);
            }
        }
    }

    private void ShowItems()
    {
        inventoryOperator.RedrawInventory();
    }




    public void CloseInventory()
    {
        isInventoryOpen = false;
        Destroy(inventoryUI);
    }

}
