using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    private PlayerInput playerInput;
    private Rigidbody2D playerRigidbody;
    [SerializeField]
    private float speedMultiplier;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove() 
    {
        playerRigidbody.velocity = GetInputVelocityVector() * speedMultiplier;
    }

    private Vector2 GetInputVelocityVector()
    {
        float horizontalValue = playerInput.GetHorizontalVal();
        float verticalValue = playerInput.GetVerticalVal();

        Vector2 playerVelocityVector = new Vector2(horizontalValue, verticalValue);

        return playerVelocityVector;
    }
}
