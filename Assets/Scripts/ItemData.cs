using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemLibrary;

public class ItemData
{

    private GameObject itemObject;
    private int itemID; // id of item
    private string itemName = "Unassigned Name";
    private string itemDescription = "Unassigned Description";
    private ItemType itemType; // if equippable: mainhand/offhand/armor/necklace/consumable/static etc. 
    private int goldValue = 0;
    private int rawPower = 0;
    private int dracPower = 0;
    private int itemInstance = 0; //id of an instance of this item (distinguishing many items of same id)
    private bool isItemActive = false; // if equippable: is item currently equipped
    public delegate void Action();
    private string itemIconPath;

    private List<int> actionIdList = new List<int>();
    private List<Action> actions = new List<Action>();


    public GameObject GetItemObject() { return itemObject; }
    public void SetItemObject(GameObject itemObject) { this.itemObject = itemObject; }

    public int GetItemID() { return itemID; }
    public void SetItemID(int itemID) { this.itemID = itemID; }

    public string GetItemName() { return itemName; }
    public void SetItemName(string itemName) { this.itemName = itemName; }

    public ItemType GetItemType() { return itemType; }
    public void SetItemType(ItemType itemType) { this.itemType = itemType; }

    public int GetItemInstance() { return itemInstance; }
    public void SetItemInstance(int itemInstance) { this.itemInstance = itemInstance; }

    public bool IsItemActive() { return isItemActive; }
    public void SetItemActive(bool act) { this.isItemActive = act; }

    public int GetGoldValue() { return goldValue; }
    public void SetGoldValue(int value) { this.goldValue = value; }

    public int GetRawPower() { return rawPower; }
    public void SetRawPower(int rawPower) { this.rawPower = rawPower; }

    public int GetDracPower() { return dracPower; }
    public void SetDracPower(int dracPower) { this.dracPower = dracPower; }

    public string GetItemIconPath() { return itemIconPath; }
    public void SetItemIconPath(string itemIconPath) { this.itemIconPath = itemIconPath; }

    public string GetItemDescription() { return itemDescription; }
    public void SetItemDescription(string itemDescription) { this.itemDescription = itemDescription; }



    public List<int> GetActionsIDList() { return actionIdList; }
    
    public void SetActions(List<int> actionIdList) 
    { 
        this.actionIdList = actionIdList;
        actions.Clear();

        foreach (int actionId in actionIdList)
        {
            AddAction(actionId);
        }
      
    }

    public void AddAction(int actionId) 
    { 
        if (actionId == 1) { actions.Add(GetDestroyItemAction(this));}
        else if (actionId == 2) { actions.Add(GetAddRemoveGoldAction(this));}
        else if (actionId == 3) { actions.Add(GetToggleRawPower(this));}
        else if (actionId == 4) { actions.Add(GetToggleDracPower(this)); }
        else if (actionId == 5) { actions.Add(GetCheckFlagAction());}
    }


    public void ExecuteActions()
    {
        foreach (Action action in actions)
        {
            action();
        }
    }
}
