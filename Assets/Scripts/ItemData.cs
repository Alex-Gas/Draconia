using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemLibrary;

public class ItemData
{

    private GameObject itemObject;
    private int itemID; // id of item
    private ItemType itemType; // if equippable: mainhand/offhand/armor/necklace/consumable/static etc. 
    private int goldValue = 0;
    private int itemInstance = 0; //id of an instance of this item (distinguishing many items of same id)
    private bool isItemActive = false; // if equippable: is item currently equipped
    public delegate void Action();

    private List<int> actionIdList = new List<int>();
    private List<Action> actions = new List<Action>();


    public GameObject GetItemObject() { return itemObject; }
    public void SetItemObject(GameObject itemObject) { this.itemObject = itemObject; }

    public int GetItemID() { return itemID; }
    public void SetItemID(int itemID) { this.itemID = itemID; }

    public ItemType GetItemType() { return itemType; }
    public void SetItemType(ItemType itemType) { this.itemType = itemType; }

    public int GetItemInstance() { return itemInstance; }
    public void SetItemInstance(int itemInstance) { this.itemInstance = itemInstance; }

    public bool IsItemActive() { return isItemActive; }
    public void SetItemActive(bool act) { isItemActive = act; }

    public int GetGoldValue() { return goldValue; }
    public void SetGoldValue(int value) { goldValue = value; }

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
        else if (actionId == 2) { actions.Add(GetAddGoldAction(this));}
        else if (actionId == 3) { actions.Add(GetToggleGoldAction(this));}
        else if (actionId == 4) { actions.Add(GetCheckFlagAction());}
    }


    public void ExecuteActions()
    {
        foreach (Action action in actions)
        {
            action();
        }
    }
}
