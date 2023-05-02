using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking.Types;

public class ItemBehaviour : EntityBehaviour
{
    private SpriteRenderer spriteRenderer;


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
            ItemManager.AddItemToPosession(gameObject.GetComponent<Item>());
            Destroy(gameObject);
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
