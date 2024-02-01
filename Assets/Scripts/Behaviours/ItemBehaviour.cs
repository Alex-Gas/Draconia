using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : EntityBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float namePlateOffset;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        string itemName = gameObject.GetComponent<Item>().GetItemName();
        SetEntityNamePlate(itemName, namePlateOffset);
    }

    private void Update()
    {
        ObjNamePlate();
        ObjHighlight();
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        if (interactFlag)
        {
            interactFlag = false;
            ItemManager.AddItemToPosession(gameObject.GetComponent<Item>());
            Destroy(gameObject);
        }
    }

    private void ObjNamePlate()
    {
        if (namePlateFlag)
        {
            namePlateFlag = false;
            namePlateOperator.ShowNamePlate();
        }
        else
        {
            namePlateOperator.HideNamePlate();
        }
    }

    private void ObjHighlight()
    {
        if (highlightFlag)
        {
            highlightFlag = false;
            namePlateOperator.SetHighlight();

        }
        else
        {
            namePlateOperator.RemoveHighlight();
        }
    }
}
