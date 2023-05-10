using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool interactInput;
    private bool inventoryInput;
    private bool menuInput;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        interactInput = Input.GetButtonDown("Interact");
        inventoryInput = Input.GetButtonDown("Inventory");
        menuInput = Input.GetButtonDown("Menu");
    }

    public float GetHorizontalVal() 
    { 
        return horizontalInput; 
    }

    public float GetVerticalVal() 
    { 
        return verticalInput; 
    }

    public bool GetInteractInput() 
    { 
        return interactInput; 
    }

    public bool GetInventoryInput()
    {
        return inventoryInput;
    }

    public bool GetMenuInput()
    {
        return menuInput;
    }

}