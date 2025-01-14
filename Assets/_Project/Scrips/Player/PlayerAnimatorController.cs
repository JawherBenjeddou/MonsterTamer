using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private PlayerInput playerInput;
    private Player player;

    private float lastMoveX = 0f;
    private float lastMoveY = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        animator.SetFloat("MoveX",playerInput.GetMovementInput().x);
        animator.SetFloat("MoveY",playerInput.GetMovementInput().y);
        animator.SetFloat("PlayerSpeed",player.CurrentSpeed);

        Vector2 movementInput = playerInput.GetMovementInput();

        float moveX = movementInput.x;
        float moveY = movementInput.y;

        // Update the LastMoveX and LastMoveY when movement input changes to zero
        if (moveX != 0 || moveY != 0)
        {
            lastMoveX = moveX;
            lastMoveY = moveY;
        }

        // Set the last movement direction values
        animator.SetFloat("LastMoveX", lastMoveX);
        animator.SetFloat("LastMoveY", lastMoveY);
    }
}
