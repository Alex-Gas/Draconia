using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PushableBehaviour : EntityBehaviour
{
    public PushableMoveDirection moveDirection;
    public int speedMultiplier;
    public float distance;
    public int chargeNo;
    private Rigidbody2D propRigidbody;
    private Vector3 movementVector = new Vector3();
    private bool moveFlag = false;
    private float currentDistance;
    private Vector3 destination;
    private SpriteRenderer spriteRenderer;

    public enum PushableMoveDirection
    {
        Up,
        Down,
        Left,
        Right,
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        propRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ObjHighlight();
        CheckInteraction();
    }

    private void FixedUpdate()
    {
        if (moveFlag)
        {
            MoveProp();
        }
    }

    private void CheckInteraction()
    {
        if (interactFlag)
        {
            interactFlag = false;
            PushableAction();
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

    public void PushableAction()
    {
        if (chargeNo > 0 && speedMultiplier > 0)
        {
            moveFlag = true;
            movementVector = GetMovementVector();
            currentDistance = distance;
            destination = this.transform.position + movementVector * distance;
        }
    }
    
    private Vector2 GetMovementVector()
    {
        if (moveDirection == PushableMoveDirection.Up) { return new Vector3(0, 1); }
        else if (moveDirection == PushableMoveDirection.Down) { return new Vector3(0, -1); }
        else if (moveDirection == PushableMoveDirection.Left) { return new Vector3(-1, 0); }
        else if (moveDirection == PushableMoveDirection.Right) { return new Vector3(1, 0); }
        return new Vector3(0, 0);
    }

    private void MoveProp()
    {
        if (currentDistance > 0)
        {
            Vector3 finalMovementVector = movementVector * speedMultiplier * Time.fixedDeltaTime;
            currentDistance -= finalMovementVector.magnitude;
            propRigidbody.MovePosition(transform.position + finalMovementVector);
        }

        else
        {
            moveFlag = false;
            this.transform.position = destination;
            currentDistance = distance;
            chargeNo -= 1;
        }
    }
}
