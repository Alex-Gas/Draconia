using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    private TransitionManager transitionManager;
    private GameObject mainCamera;
    public float x = 0;
    public float y = 0;
    public float fadeDuration;
    public float delayDuration;
    public bool isDialog;
    public string dialogName;
    DialogManager dialogManager;


    private void Start()
    {
        transitionManager = GameObject.Find("UI").GetComponent<TransitionManager>();
        mainCamera = GameObject.Find("Main Camera");
        dialogManager = GameObject.Find("UI").GetComponent<DialogManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isDialog)
            {
                dialogManager.InitiateDialog("gate");
            }
            else if (!isDialog)
            {
                Teleport();
            }
        }
    }


    private void Teleport()
    {
        GameObject playerObj = GameObject.Find("Player");
        List<TransitionOperator.Action> tranActions = new List<TransitionOperator.Action>()
        {
            () =>
            {
                playerObj.transform.position = new Vector3(x, y, playerObj.transform.position.z);
                mainCamera.transform.position = new Vector3(x, y, mainCamera.transform.position.z);
            }
        };
        transitionManager.InitiateTransition(fadeDuration, delayDuration, tranActions, true);
    }
}
