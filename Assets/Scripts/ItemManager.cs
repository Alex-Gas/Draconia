using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using static UnityEditor.Progress;

public static class ItemManager
{
    public static List<ItemData> itemsInPosession = new List<ItemData>() { };
    public static int goldTotalAmount = 0;
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
            Item item = ItemLibrary.GetItemDataByID(id).GetItemObject().GetComponent<Item>();
            AddItemToPosession(item);
        }
    }
    

    public static void AddItemToPosession(Item item)
    {
        int itemID = item.GetItemID();
        int itemValue = item.GetGoldValue();

        
        // Creating a copy of an item template from library
        ItemData itemData = new ItemData();

        // Copying data over from the library
        itemData = ItemLibrary.CopyItemData(itemData, itemID);

        // Applying item stats based on data of received item.
        // Applying new gold value of the item copy
        itemData.SetGoldValue(itemValue);
        //itemData.SetPowerValue();

        // Setting instance number variable to distinguish items of the same id.
        itemData.SetItemInstance(HandleDuplicates(itemData));
        // Adding the item copy to list of posessions
        itemsInPosession.Add(itemData);
        
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


    public static void RemoveItemFromPosession(int itemID)
    {
        foreach (ItemData item in itemsInPosession)
        {
            if (item.GetItemID() == itemID)
            {
                itemsInPosession.Remove(item);
            }
        }
    }

    public static void AddGold(int goldAmount)
    {
        goldTotalAmount += goldAmount;
        Debug.Log("total gold: " + goldTotalAmount);
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
