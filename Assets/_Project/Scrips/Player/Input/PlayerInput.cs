using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField]
    private InputActionAsset playerControlsAsset;

    private InputActionMap playerControlsMap;

    private InputAction moveAction, attackAction;

    private bool inputEnabled = true;


    private void Awake()
    {
        // Initialize input actions
        moveAction = playerControlsAsset.FindAction("Move");
        attackAction = playerControlsAsset.FindAction("Attack");

        playerControlsMap = playerControlsAsset.FindActionMap("Player");
        // Enable input actions
        playerControlsMap.Enable();
    }

    public Vector2 GetMovementInput()
    {
        return moveAction.ReadValue<Vector2>();
    }

    public bool GetAttackInput()
    {
        return attackAction.triggered;
    }

    public void DisableInput()
    {
        inputEnabled = false;
        playerControlsMap.Disable();
    }

    public void EnableInput()
    {
        inputEnabled = true;
        playerControlsMap.Enable();
    }

    private void OnApplicationQuit()
    {
        // Disable input actions when quitting
        playerControlsMap.Disable();
    }
}