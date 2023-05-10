using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public static int orientation = 6;
    public static bool isPlayerMoving = false;

    private PlayerInput playerInput;
    private Rigidbody2D playerRigidbody;
    [SerializeField]
    private float speedMultiplier;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float verticalMultiplier;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckMovement();
        PlayerMove();
    }

    private void PlayerMove() 
    {
        Vector2 movementVector = GetInputVelocityVector();
        playerRigidbody.velocity = movementVector * speedMultiplier;
        PlayerMovementState(movementVector);
    }

    private Vector2 GetInputVelocityVector()
    {
        if (!GameState.isPaused)
        {
            float horizontalValue = playerInput.GetHorizontalVal();
            float verticalValue = playerInput.GetVerticalVal() * verticalMultiplier;

            Vector2 playerVelocityVector = new Vector2(horizontalValue, verticalValue);

            return playerVelocityVector;
        }
        else
        {
            return new Vector2(0, 0);
        }
    }


    private void CheckMovement()
    {
        if (playerRigidbody.velocity.magnitude > 0.2)
        {
            isPlayerMoving = true;
        }
        else
        {
            isPlayerMoving = false;
        }
    }


    private void PlayerMovementState(Vector2 movementVector)
    {
        if (movementVector.x > 0 || movementVector.x < 0)
        {
            if (movementVector.x > 0)
            {
                orientation = 3;
            }
            else if (movementVector.x < 0)
            {
                orientation = 9;
            }
        }

        else if (movementVector.y > 0)
        {
            orientation = 12;
        }

        else if (movementVector.y < 0)
        {
            orientation = 6;
        }

        animator.SetInteger("Orientation", orientation);
        animator.SetBool("IsPlayerMoving", isPlayerMoving);
    }
}
