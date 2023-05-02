using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : EntityBehaviour
{
    private InventoryManager inventoryManager;
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        inventoryManager = GameObject.Find("UI").GetComponent<InventoryManager>();
    }

    private void Update()
    {
        PlayerOpenInv();
        CheckInteraction();
    }

    private void CheckInteraction()
    {
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

}
