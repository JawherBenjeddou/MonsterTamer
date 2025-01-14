using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed = 5f;

    [Header("Components")]
    private Rigidbody2D rb;

    private PlayerInput inputHandler;

    private SpriteRenderer spriteRenderer;

    // Stores player input
    private Vector2 movementInput;

    public InputActionAsset inputAction;

    private void Awake()
    {
        // Assign the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    private void Update()
    {
        // Get input from player
        HandleInput();
    }

    private void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        MoveCharacter();
    }

    private void HandleInput()
    {
        // Get horizontal and vertical input (WASD or Arrow Keys by default)
        float horizontal = inputHandler.GetMovementInput().x;
        float vertical = inputHandler.GetMovementInput().y;

        // Combine inputs into a Vector2
        movementInput = new Vector2(horizontal, vertical).normalized; // Normalize to maintain consistent speed

        // Flip the sprite based on horizontal input
        if (movementInput.x < 0)  // Moving left
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)  // Moving right
        {
            spriteRenderer.flipX = false;
        }
    }

    private void MoveCharacter()
    {
        // Calculate new position
        Vector2 newPosition = rb.position + movementInput * moveSpeed * Time.fixedDeltaTime;

        // Apply position to Rigidbody2D
        rb.MovePosition(newPosition);
    }

    public float CurrentSpeed
    {
        get
        {
            // If the player is moving, return the speed; otherwise, return 0
            return movementInput.magnitude * moveSpeed;
        }
    }
}
