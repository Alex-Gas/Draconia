using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private StartManager startManager;

    private void Start()
    {
        startManager = GameObject.Find("UI").GetComponent<StartManager>();
        startManager.InitiateStart();
    }

}
