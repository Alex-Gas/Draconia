using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public static class ItemLibrary
{
    private static Dictionary<int, ItemData> itemDataDict;
    private static List<string> itemPathList;

    public enum ItemType
    {
        MainHand,
        OffHand,
        Armor,
        Necklace,
        Ring,
        Consumable,
        Static
    }

    public static void PrepareItems()
    {
        CreateItemPathList();

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
        newItemData.SetItemName(libraryItem.GetItemName());
        newItemData.SetItemObject(libraryItem.GetItemObject());
        newItemData.SetItemType(libraryItem.GetItemType());
        newItemData.SetActions(libraryItem.GetActionsIDList());
        newItemData.SetItemIconPath(libraryItem.GetItemIconPath());
        newItemData.SetItemDescription(libraryItem.GetItemDescription());

        return newItemData;
    }

    public static ItemData SetItemStatistics(ItemData newItemData, int newGoldValue, int newRawPowerValue, int newDracPowerValue)
    {
        newItemData.SetGoldValue(newGoldValue);
        newItemData.SetRawPower(newRawPowerValue);
        newItemData.SetDracPower(newDracPowerValue);

        return newItemData;
    }


    public static ItemData.Action GetAddRemoveGoldAction(ItemData itemData) { 
        return () => GameState.AddRemoveGold(itemData.GetGoldValue()); }
    public static ItemData.Action GetToggleRawPower(ItemData itemData) {
        return () => GameState.AddRemoveRawPower(itemData.IsItemActive() == true ? -itemData.GetRawPower() : itemData.GetRawPower()); }
    public static ItemData.Action GetToggleDracPower(ItemData itemData) {
        return () => GameState.AddRemoveDracPower(itemData.IsItemActive() == true ? -itemData.GetDracPower() : itemData.GetDracPower()); }
    public static ItemData.Action GetCheckFlagAction() { 
        return () => ItemManager.TestFlag(); }
    public static ItemData.Action GetDestroyItemAction(ItemData itemData) {
        return () => ItemManager.DestroyInventoryItem(itemData); }

    private static ItemData SetCommonData(int id, ItemData itemData, Dictionary<int, Item> itemExternalDataDict, Dictionary<int, GameObject> prefabDict, List<int> actionsList, string itemIconPath, string itemDescription)
    {
        Item itemExternalData = itemExternalDataDict[id];
        GameObject prefab = prefabDict[id];

        itemData.SetItemID(itemExternalData.GetItemID());
        itemData.SetItemName(itemExternalData.GetItemName());
        itemData.SetItemObject(prefab);
        itemData.SetItemType(itemExternalData.GetItemType());
        itemData.SetGoldValue(itemExternalData.GetGoldValue());
        itemData.SetRawPower(itemExternalData.GetRawPower());
        itemData.SetDracPower(itemExternalData.GetDracPower());
        itemData.SetActions(actionsList);
        itemData.SetItemIconPath(itemIconPath);
        itemData.SetItemDescription(itemDescription);

        return itemData;
    }

    private static void CreateItemPathList()
    {
        string a = "Prefabs/Items/";
        itemPathList = new List<string>()
        {
            a + "Armor", a + "BlacksmithPouch", a + "Buckler", a + "DraconicScaleVest", a + "EnchantedVest",
            a + "GreenPearl", a + "Pouch", a + "RingOfDragonBreath", a + "Shield", a + "StaffBlade", a + "Stick",
            a + "Sword", a + "ToothedNecklace", a + "Whisky"
 
        };
    }

    // Creating a dict of all unique item prefabs in the game
    private static void CreateItemDictionary()
    {
        Item itemExternalData;
        GameObject prefab;
        int id;
        List<int> actionsList;
        string itemIconPath;
        string itemDescription;

        itemDataDict = new Dictionary<int, ItemData>();
        Dictionary<int, Item> itemExternalDataDict = new Dictionary<int, Item>();
        Dictionary<int, GameObject>prefabDict = new Dictionary<int, GameObject>();


        foreach (string itemPath in itemPathList)
        {
            prefab = (GameObject)Resources.Load(itemPath);


            // for each item prefab
            itemExternalData = prefab.GetComponent<Item>();
            itemExternalDataDict.Add(itemExternalData.GetItemID(), itemExternalData);
            prefabDict.Add(itemExternalData.GetItemID(), prefab);
        }

        { // normal pouch
            id = 1;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 2, 1 };
            itemIconPath = "Textures/Items/ItemIcons/ShardsPouch";

            itemDescription = "A pouch full of some sort of purple gems. They look rough and uncut. Commonly used as currency in Draconian lands";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // shield
            id = 2;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3 };
            itemIconPath = "Textures/Items/ItemIcons/ShieldIcon";
            itemDescription = "Considered merely a buckler among draconians, to you its a hefty shield";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { //staffblade
            id = 3;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3 };
            itemIconPath = "Textures/Items/ItemIcons/StaffBladeIcon";
            itemDescription = "As the name suggests its a staff, with a sharp blade attached to one end. A traditional weapon used by draconian guards.";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // whisky
            id = 4;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3, 1 };
            itemIconPath = "Textures/Items/ItemIcons/WhiskyBottleIcon";
            itemDescription = "A thick ominous liquid is sloshing inside. You have a feeling its not meant for human stomachs";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // blacksmith pouch
            id = 5;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 2, 1 };
            itemIconPath = "Textures/Items/ItemIcons/ShardsPouch";
            itemDescription = "A pouch full of some sort of purple gems. They look rough and uncut. Commonly used as currency in Draconian lands. This pouch looks special. It looks like it has a pin with 'A' etched on it.";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // green pearl
            id = 6;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { };
            itemIconPath = "Textures/Items/ItemIcons/GreenPearlIcon";
            itemDescription = "A curious object. Doesn't look useful to you but it looks pretty. It might fetch a nice price from a right buyer.";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // vest
            id = 7;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3 };
            itemIconPath = "Textures/Items/ItemIcons/VestIcon";
            itemDescription = "A chainmail-like vest made of elements shaped to look like draconic scales. Provides an adequate protection while being relatively light.";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // enchanted vest
            id = 8;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3 };
            itemIconPath = "Textures/Items/ItemIcons/EnchVestIcon";
            itemDescription = "A chainmail-like vest made of elements shaped to look like draconic scales. Provides an adequate protection while being relatively light. It has been magically enchanted to reflect hard blows.";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // ring
            id = 9;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3 };
            itemIconPath = "Textures/Items/ItemIcons/RingIcon";
            itemDescription = "The ring is quite hot and has a magical aura emanating from it. Looks like it can be used to cast a magical flame at the cost of users body heat.";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // toothed necklace
            id = 10;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3 };
            itemIconPath = "Textures/Items/ItemIcons/ToothedNecklaceIcon";
            itemDescription = "You can barely hear a faint voice coming from the necklace's mouth-shaped opening. The opening is surounded by unsettling sharp teeth. You resist the urge to put your ear agains it.";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // stick
            id = 11;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { };
            itemIconPath = "Textures/Items/ItemIcons/StickIcon";
            itemDescription = "Yup. Its just a stick.";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // sword
            id = 12;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3 };
            itemIconPath = "Textures/Items/ItemIcons/SwordIcon";
            itemDescription = "A hilt with some dull blade attached to it. Perfect to defend yourself from a particularly aggressive crippled raccoon.";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // buckler
            id = 13;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3 };
            itemIconPath = "Textures/Items/ItemIcons/BucklerIcon";
            itemDescription = "A small very simple shield. Served you better as a bowl";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
        { // shirt
            id = 14;
            ItemData itemData = new ItemData();
            actionsList = new List<int>() { 3 };
            itemIconPath = "Textures/Items/ItemIcons/ArmorIcon";
            itemDescription = "Ragged and worn from many days of travel. The most protection it provides is from elements.";

            itemData = SetCommonData(id, itemData, itemExternalDataDict, prefabDict, actionsList, itemIconPath, itemDescription);
            itemDataDict.Add(id, itemData);
        }
    }
}



