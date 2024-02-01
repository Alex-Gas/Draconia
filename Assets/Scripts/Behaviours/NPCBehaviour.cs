using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : EntityBehaviour
{
    public ContainerContentDirection dropDirection;
    private SpriteRenderer spriteRenderer;
    private DialogManager dialogManager;
    [SerializeField]
    private string npcID;
    [SerializeField]
    private string npcName;
    [SerializeField]
    private float namePlateOffset;
    public bool isNPCDead = false;
    public float dropDistance = 1f;
    public Animator animator;


    public enum ContainerContentDirection
    {
        Up,
        Down,
        Left,
        Right,
    }


    private void Start()
    {
        dialogManager = GameObject.Find("UI").GetComponent<DialogManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetEntityNamePlate(npcName, namePlateOffset);
    }

    private void Update()
    {
        ObjNamePlate();
        ObjHighlight();
        CheckInteraction();
    }

    public void SetNpcDead(bool isNPCDead)
    {
        this.isNPCDead = isNPCDead;
        animator.SetBool("IsNPCDead", isNPCDead);
    }

    public bool IsNPCDead()
    {
        return this.isNPCDead;
    }

    private void CheckInteraction()
    {
        if (interactFlag && !DialogManager.isDialogOpen && !isNPCDead)
        {
            interactFlag = false;
            dialogManager.InitiateDialog(npcID);
        }
    }


    private void ObjNamePlate()
    {
        if (namePlateFlag && !isNPCDead)
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
        if (highlightFlag && !isNPCDead)
        {
            highlightFlag = false;
            namePlateOperator.SetHighlight();
          
        }
        else
        {
            namePlateOperator.RemoveHighlight();
        }
    }

    public void NPCDropItem(ItemData itemData)
    {
        Vector3 trans = gameObject.transform.position;
        GameObject obj = itemData.GetItemObject();


        obj = ItemManager.ProcureItem(obj, itemData.GetGoldValue(), itemData.GetRawPower(), itemData.GetDracPower());

        Vector3 dropLocation = this.transform.position + GetDropVector() * dropDistance;

        Instantiate(obj, dropLocation, Quaternion.identity);
    }



    private Vector3 GetDropVector()
    {
        if (dropDirection == ContainerContentDirection.Up) { return new Vector3(0, 1, -1.5f); }
        else if (dropDirection == ContainerContentDirection.Down) { return new Vector3(0, -1, -1.5f); }
        else if (dropDirection == ContainerContentDirection.Left) { return new Vector3(-1, 0, -1.5f); }
        else if (dropDirection == ContainerContentDirection.Right) { return new Vector3(1, 0, -1.5f); }
        return new Vector3(0, 0, 0);
    }

}
