using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerInput playerInput;
    private Dictionary<GameObject, float> interactableObjects;

    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        interactableObjects = new Dictionary<GameObject, float>();
    }

    private void Update()
    {
        PlayerInteract();
    }

    private void PlayerInteract()
    {
        bool isInteract = playerInput.GetInteractInput();

        foreach (KeyValuePair<GameObject, float> entry in interactableObjects)
        {
            entry.Key.GetComponent<EntityBehaviour>().SetNamePlateFlag();
        }

        GetClosestObj()?.GetComponent<EntityBehaviour>().SetHighlightFlag();

        if (!GameState.isPaused && isInteract)
        {
            GetClosestObj()?.GetComponent<EntityBehaviour>().SetInteractFlag();
        }

    }

    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Interactable"))
        {
            GameObject interactableObj = obj.gameObject;
            float distance = Vector2.Distance(interactableObj.transform.position, this.gameObject.transform.position);
            interactableObjects.Add(interactableObj, distance);
        }

    }

    public void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Interactable"))
        {
            GameObject interactableObj = obj.gameObject;
            interactableObjects.Remove(interactableObj);
        }
    }
  
    private GameObject GetClosestObj()
    {
        GameObject minObject = null;
        float minValue = float.MaxValue;

        UpdateDictionary();

        foreach (KeyValuePair<GameObject, float> entry in interactableObjects)
        {
            if (entry.Value < minValue)
            {
                minValue = entry.Value;
                minObject = entry.Key;
            }
        }

        return minObject;
    }


    private void UpdateDictionary()
    {
        Dictionary<GameObject, float> tempDict = new Dictionary<GameObject, float>();

        foreach (KeyValuePair<GameObject, float> entry in interactableObjects)
        {
            GameObject interactable = entry.Key;
            float newDistance = Vector2.Distance(interactable.transform.position, this.gameObject.transform.position);
            tempDict[interactable] = newDistance;
        }

        interactableObjects = tempDict;
    }

}
