using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : EntityBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D doorCollider;

    [SerializeField]
    private string doorName;
    [SerializeField]
    private float namePlateOffset;
    [SerializeField]
    private Sprite doorOpen;
    [SerializeField]
    private Sprite doorClosed;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();

        SetEntityNamePlate(doorName, namePlateOffset);
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
            DoorAction();
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


    private void DoorAction()
    {
        if (spriteRenderer.sprite == doorClosed)
        {
            spriteRenderer.sprite = doorOpen;
            doorCollider.isTrigger = true;
        }
        else
        {
            spriteRenderer.sprite = doorClosed;
            doorCollider.isTrigger = false;
        }
    }
}
