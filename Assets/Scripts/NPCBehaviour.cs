using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : EntityBehaviour
{
    private SpriteRenderer spriteRenderer;
    private DialogManager dialogManager;
    [SerializeField]
    private string npcID;

    private void Start()
    {
        dialogManager = GameObject.Find("UI").GetComponent<DialogManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ObjHighlight();
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        if (interactFlag && !dialogManager.isDialogOpen)
        {
            interactFlag = false;
            dialogManager.InitiateDialog(npcID);
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

}
