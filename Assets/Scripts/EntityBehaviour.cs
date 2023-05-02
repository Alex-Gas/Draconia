using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    protected bool interactFlag;
    protected bool highlightFlag;
    protected bool inventoryFlag;
    [SerializeField]

    public void SetInteractFlag()
    {
        interactFlag = true;
    }

    public void SetHighlightFlag()
    {
        highlightFlag = true;
    }

    public void SetInventoryFlag()
    {
        inventoryFlag = true;
    }





}


