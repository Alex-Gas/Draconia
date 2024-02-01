using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemManager
{
    public static List<ItemData> itemsInPosession = new List<ItemData>() {};
    public static bool redrawFlag = false;

    private static bool testFlag = false;
    public static void TestFlag()
    {
        if (!testFlag)
        {
            Debug.Log("Global action happened and wont happen again");
            testFlag = true;
        }
    }


    public static void AddStartingItems(List<int> list)
    {
        foreach (int id in list)
        {
            ItemData itemData = ItemLibrary.GetItemDataByID(id);
            AddItemToPosession(null, itemData);
        }
    }
    

    public static void AddItemToPosession(Item item = null, ItemData incomingItemData = null)
    {
        // Creating a copy of an item template from library
        ItemData itemData = new ItemData();

        if (item != null)
        {
            // Copying data over from the library
            itemData = ItemLibrary.CopyItemData(itemData, item.GetItemID());
            itemData = ItemLibrary.SetItemStatistics(itemData, item.GetGoldValue(), item.GetRawPower(), item.GetDracPower());
        }
        else if (incomingItemData != null)
        {
            itemData = incomingItemData;
        }
        else { throw new Exception("Item was not added, no incoming data"); }

        // Setting instance number variable to distinguish items of the same id.
        itemData.SetItemInstance(HandleDuplicates(itemData));
        // Adding the item copy to list of posessions
        itemsInPosession.Add(itemData);
    }

    public static ItemData ProcureItemData(int itemID, int itemValue, int itemRawPower, int itemDracPower)
    {
        ItemData itemData = new ItemData();

        itemData = ItemLibrary.CopyItemData(itemData, itemID);
        itemData = ItemLibrary.SetItemStatistics(itemData, itemValue, itemRawPower, itemDracPower);

        return itemData;
    }

    public static GameObject ProcureItem(GameObject obj, int itemValue, int itemRawPower, int itemDracPower)
    {
        Item item = obj.GetComponent<Item>();
        item.SetGoldValue(itemValue);
        item.SetRawPower(itemRawPower);
        item.SetDracPower(itemDracPower);
        return obj;
    }

    private static int HandleDuplicates(ItemData itemData)
    {
        List<int> listOfInstances = new List<int>();

        foreach (ItemData ownedItemData in itemsInPosession)
        {
            if (ownedItemData.GetItemID() == itemData.GetItemID())
            {
                listOfInstances.Add(ownedItemData.GetItemInstance());
            }
        }

        return GenerateInstanceNo(listOfInstances);
    }


    private static int GenerateInstanceNo(List<int> listOfInstances)
    {
        if (listOfInstances.Count > 0)
        {
            int x = 0;
            listOfInstances.Sort();

            foreach (int inst in listOfInstances)
            {
                if (x < inst)
                {
                    return x;
                }

                else
                {
                    x++;
                }
            }
            return x;
        }

        else
        {
            return 0;
        }
    }

    public static void UnequipItem(int itemID)
    {
        foreach(ItemData itemData in itemsInPosession)
        {
            if (itemData.GetItemID() == itemID && itemData.IsItemActive() == true)
            {
                itemData.ExecuteActions();
            }
        }
    }

    public static void RemoveItemFromPosession(int itemID)
    {
        itemsInPosession.RemoveAll(itemData => itemData.GetItemID() == itemID);
    }

    public static void DestroyInventoryItem(ItemData itemData)
    {
        for (int i = 0; i < itemsInPosession.Count; i++)
        {
            if (itemsInPosession[i] == itemData)
            {
                itemsInPosession.RemoveAt(i);
                redrawFlag = true;
            }
        }
    }
}
