using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemLibrary;

public class Item : MonoBehaviour
{
    private GameObject itemObject;
    [SerializeField] private int itemID; // id of item
    [SerializeField] private string itemName = "Unassigned Name";
    [SerializeField] private ItemType itemType; // if equippable: mainhand/offhand/armor/necklace/consumable/static etc. 
    [SerializeField] private int goldValue = 0;
    [SerializeField] private int rawPower = 0;
    [SerializeField] private int dracPower = 0;

    private int itemInstance; //id of an instance of this item (distinguishing many items of same id)
    private bool isItemActive = false; // if equippable: is item currently equipped

    public GameObject GetItemObject() { return itemObject; }
    public void SetItemObject(GameObject itemObject) { this.itemObject = itemObject; }

    public int GetItemID() { return itemID; }
    public void SetItemID(int itemID) { this.itemID = itemID; }

    public string GetItemName() { return itemName; }
    public void SetItemName(string itemName) { this.itemName = itemName; }

    public ItemType GetItemType() {  return itemType; }
    public void SetItemType(ItemType itemType) { this.itemType = itemType; }

    public int GetItemInstance() {  return itemInstance; }
    public void SetItemInstance(int itemInstance) { this.itemInstance = itemInstance; }

    public bool IsItemActive() { return isItemActive; }
    public void SetItemActive(bool act) { isItemActive = act; }

    public int GetGoldValue() {  return goldValue; }
    public void SetGoldValue(int value) { goldValue = value; }

    public int GetRawPower() { return rawPower; }
    public void SetRawPower(int rawPower) { this.rawPower = rawPower; }

    public int GetDracPower() { return dracPower; }
    public void SetDracPower(int dracPower) { this.dracPower = dracPower; }
}

