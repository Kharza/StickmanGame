using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpSpeed = 2f;
    [SerializeField] Vector2 movementX;
    Vector2 jumpVector = new Vector2(0, 10);

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movementX = new Vector2(10, transform.position.y);
        if (Input.GetKeyDown("space"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter(movementX);
        
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed * Time.deltaTime;
    }

    void Jump()
    {
        rb.AddForce(jumpVector * jumpSpeed);
    }

}
