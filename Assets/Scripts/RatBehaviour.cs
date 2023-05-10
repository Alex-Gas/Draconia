using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : EntityBehaviour
{
    public float moveSpeed; 
    public float minTurnAngle; 
    public float maxTurnAngle; 
    private bool collided = false;
    //private Vector3 moveDirection = new Vector3(1f, 0f, 0f);
    //private float newAngle = 0f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer("InteractCircle"))
        collided = true;
    }

    void FixedUpdate()
    {
        /*
        if (!collided)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;


        }
        else
        {
            newAngle = Random.Range(0f, 360f);
        }
        */
    }
}
