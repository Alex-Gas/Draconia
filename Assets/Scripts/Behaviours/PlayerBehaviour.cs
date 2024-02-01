using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : EntityBehaviour
{
    private InventoryManager inventoryManager;
    private MenuManager menuManager;
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        inventoryManager = GameObject.Find("UI").GetComponent<InventoryManager>();
        menuManager = GameObject.Find("UI").GetComponent<MenuManager>();
    }

    private void Update()
    {
        PlayerOpenInv();
        PlayerOpenMenu();
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        if (menuFlag)
        {
            menuFlag = false;
            menuManager.ToggleMenu();
        }


        if (inventoryFlag)
        {
            inventoryFlag = false;
            inventoryManager.ToggleInventory();
        }
    }

    private void PlayerOpenInv()
    {
        if (playerInput.GetInventoryInput())
        {
            SetInventoryFlag();
        }
    }

    private void PlayerOpenMenu()
    {
        if (playerInput.GetMenuInput())
        {
            SetMenuFlag();
        }
    }

}
