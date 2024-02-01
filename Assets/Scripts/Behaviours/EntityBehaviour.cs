using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    protected bool interactFlag;
    protected bool highlightFlag;
    protected bool namePlateFlag;
    protected bool inventoryFlag;
    protected bool menuFlag;

    protected GameObject namePlateObj;
    protected NamePlateOperator namePlateOperator;


    public void SetInteractFlag()
    {
        interactFlag = true;
    }

    public void SetHighlightFlag()
    {
        highlightFlag = true;
    }

    public void SetNamePlateFlag()
    {
        namePlateFlag = true;
    }

    public void SetInventoryFlag()
    {
        inventoryFlag = true;
    }

    public void SetMenuFlag()
    {
        menuFlag = true;
    }




    public void SetEntityNamePlate(string npcName, float yOffset = 0)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/NamePlate");
        
        if (prefab != null)
        {
            Vector3 offset = new Vector3(0, yOffset, -3);
            namePlateObj = Instantiate(prefab, gameObject.transform.position + offset, Quaternion.identity, gameObject.transform);
            namePlateOperator = namePlateObj.GetComponent<NamePlateOperator>();
            namePlateOperator.Setup();
            namePlateOperator.SetName(npcName);
            namePlateOperator.HideNamePlate();
        }
    }

}


