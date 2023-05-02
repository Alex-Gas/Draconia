using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public static class ItemLibrary
{
    private static string prefabDirectory = "Assets/Prefabs/Items";
    private static Dictionary<int, ItemData> itemDataDict;

    public enum ItemType
    {
        MainHand,
        OffHand,
        Armor,
        Necklace,
        Ring,
        Consumable
    }

    public static void PrepareItems()
    {
        CreateItemDictionary();
    }
    
    public static ItemData GetItemDataByID(int itemID)
    {
        return itemDataDict[itemID];
    }

    public static ItemData CopyItemData(ItemData newItemData, int itemID)
    {
        ItemData libraryItem = GetItemDataByID(itemID);

        newItemData.SetItemID(libraryItem.GetItemID());
        newItemData.SetItemObject(libraryItem.GetItemObject());
        newItemData.SetItemType(libraryItem.GetItemType());
        newItemData.SetActions(libraryItem.GetActionsIDList());

        return newItemData;
    }

    public static ItemData.Action GetAddGoldAction(ItemData itemData) { 
        return () => ItemManager.AddGold(itemData.GetGoldValue()); }
    public static ItemData.Action GetToggleGoldAction(ItemData itemData) {
        return () => ItemManager.AddGold(itemData.IsItemActive() == true ? -itemData.GetGoldValue() : itemData.GetGoldValue()); }
    public static ItemData.Action GetCheckFlagAction() { 
        return () => ItemManager.TestFlag(); }
    public static ItemData.Action GetDestroyItemAction(ItemData itemData) {
        return () => ItemManager.DestroyInventoryItem(itemData); }

    private static ItemData SetCommonData(int id, ItemData itemData, Dictionary<int, Item> itemExternalDataDict, Dictionary<int, GameObject> prefabDict, List<int> actionsList)
    {
        Item itemExternalData = itemExternalDataDict[id];
        GameObject prefab = prefabDict[id];

        itemData.SetItemID(itemExternalData.GetItemID());
        itemData.SetItemObject(prefab);
        itemData.SetItemType(itemExternalData.GetItemType());
        itemData.SetGoldValue(itemExternalData.GetGoldValue());
        itemData.SetActions(actionsList);

        return itemData;
    }


    // Creating a dict of all unique item prefabs in the game
    private static void CreateItemDictionary()
    {
        Item itemExternalData;
        GameObject prefab;
        int id;
        List<int> actionsList;
        itemDataDict = new Dictionary<int, ItemData>();
        Dictionary<int, Item> itemExternalDataDict = new Dictionary<int, Item>();
        Dictionary<int, GameObject>prefabDict = new Dictionary<int, GameObject>();
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { prefabDirectory });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            itemExternalData = prefab.GetComponent<Item>();
            itemExternalDataDict.Add(itemExternalData.GetItemID(), itemExternalData);
            prefabDict.Add(itemExternalData.GetItemID(), prefab);
        }

        {
            id = 1;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 1, 2 };
            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList);
            itemDataDict.Add(id, itemData);
        }
        {
            id = 2;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3, 4 };
            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList);
            itemDataDict.Add(id, itemData);
        }
        {
            id = 3;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3, 4 };
            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList);
            itemDataDict.Add(id, itemData);
        }
        {
            id = 4;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3, 4 };
            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList);
            itemDataDict.Add(id, itemData);
        }
    }
}
