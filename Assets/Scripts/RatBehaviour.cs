using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : EntityBehaviour
{

    private Rigidbody2D ratRigidBody;

    public float speed = 1f;
    private Vector2 direction = Vector2.right;
    private int orientation;

    private void Start()
    {
        ratRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ratRigidBody.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 fromDirection = collision.ClosestPoint(gameObject.transform.position) - (Vector2)gameObject.transform.position;
        direction = -1f * fromDirection;
    }


}
