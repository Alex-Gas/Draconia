using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBehaviour : EntityBehaviour
{
    public ContainerContentDirection dropDirection;
    private SpriteRenderer spriteRenderer;

    public float dropDistance;

    public int itemId;
    public int itemGoldValue;
    public int itemPowerValue;
    public bool isEmptied = false;

    [SerializeField]
    private string containerName;
    [SerializeField]
    private float namePlateOffset;
    [SerializeField]
    private Sprite containerEmpty;


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

        SetEntityNamePlate(containerName, namePlateOffset);
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
            ContainerAction();
        }
    }


    private void ObjNamePlate()
    {
        if (namePlateFlag && !isEmptied)
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
        if (highlightFlag && !isEmptied)
        {
            highlightFlag = false;
            namePlateOperator.SetHighlight();

        }
        else
        {
            namePlateOperator.RemoveHighlight();
        }
    }


    private void ContainerAction()
    {
        if (isEmptied == false)
        {
            spriteRenderer.sprite = containerEmpty;

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
        if (dropDirection == ContainerContentDirection.Up) { return new Vector3(0, 1, 2); }
        else if (dropDirection == ContainerContentDirection.Down) { return new Vector3(0, -1, 2); }
        else if (dropDirection == ContainerContentDirection.Left) { return new Vector3(-1, 0, 2); }
        else if (dropDirection == ContainerContentDirection.Right) { return new Vector3(1, 0, 2); }
        return new Vector3(0, 0);
    }

}
