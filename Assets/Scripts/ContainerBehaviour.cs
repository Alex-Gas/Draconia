using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PushableBehaviour;
using UnityEngine.EventSystems;

public class ContainerBehaviour : EntityBehaviour
{
    public ContainerContentDirection dropDirection;
    private SpriteRenderer spriteRenderer;

    public float dropDistance;

    public int itemId;
    public int itemGoldValue;
    public int itemPowerValue;
    public bool isEmptied = false;
    

    public enum ContainerContentDirection
    {
        Up,
        Down,
        Left,
        Right,
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ObjHighlight();
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        if (interactFlag)
        {
            interactFlag = false;
            ContainerAction();
        }
    }

    private void ObjHighlight()
    {
        if (highlightFlag)
        {
            spriteRenderer.color = Color.yellow;
            highlightFlag = false;
        }

        else
        {
            spriteRenderer.color = Color.red;
        }
    }


    private void ContainerAction()
    {
        if (isEmptied == false)
        {
            isEmptied = true;
            GameObject obj = ItemLibrary.GetItemDataByID(itemId).GetItemObject();
            Item item = obj.GetComponent<Item>();
            item.SetGoldValue(itemGoldValue);

            Vector3 dropLocation = this.transform.position + GetDropVector() * dropDistance;

            Instantiate(obj, dropLocation, Quaternion.identity);
        }
    }




    private Vector3 GetDropVector()
    {
        if (dropDirection == ContainerContentDirection.Up) { return new Vector3(0, 1, 0); }
        else if (dropDirection == ContainerContentDirection.Down) { return new Vector3(0, -1, 0); }
        else if (dropDirection == ContainerContentDirection.Left) { return new Vector3(-1, 0, 0); }
        else if (dropDirection == ContainerContentDirection.Right) { return new Vector3(1, 0, 0); }
        return new Vector3(0, 0);
    }

}
