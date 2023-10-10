using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerInput playerInput;

    public InputManager(PlayerInput _player)
    {
        playerInput = _player;
    }


    /// <summary>
    /// NOTE TO SELF:
    /// 
    /// get => moveDirValue;
    /// private set => moveDirValue = value;
    /// =
    /// get { return moveDirValuue; }
    /// private set { moveDirValue = value; }
    /// </summary>
    private float moveDirValue;
    public float MoveDirValueProperty
    {
        get { return moveDirValue; }
        private set { moveDirValue = value; }
    }


    private float moveTurnValue;
    public float MoveTurnValueProperty
    {
        get { return moveTurnValue; }
        private set { moveTurnValue = value; }
    }


    private bool breakValue;
    public bool BreakValueProperty
    {
        get { return breakValue; }
        private set { breakValue = value; }
    }


    private void GetMovement(InputAction.CallbackContext ctx)
    {
        moveDirValue = ctx.ReadValue<float>();
    }

    private void StopMovement(InputAction.CallbackContext ctx)
    {
        moveDirValue = 0;
    }


    private void GetTurning(InputAction.CallbackContext ctx)
    {
        moveTurnValue = ctx.ReadValue<float>();
    }

    private void StopTurning(InputAction.CallbackContext ctx)
    {
        moveTurnValue = 0;
    }


    private void GetBrake(InputAction.CallbackContext ctx)
    {
        breakValue = ctx.ReadValueAsButton();
    }

    private void StopBrake(InputAction.CallbackContext ctx)
    {
        breakValue = false;
    }

    
    private void GetPausing(InputAction.CallbackContext ctx)
    {
        GameManager.pauseGameAction?.Invoke();
    }

    /// <summary>
    /// NOTE TO SELF:
    /// 
    /// input.Player.Movement.performed += GetMovement;
    /// input.Player.Turning.performed += GetTurning;
    /// input.Player.Braking.performed += GetBrake;
    /// input.Player.Movement.canceled += StopMovement;
    /// input.Player.Turning.canceled += StopTurning;
    /// input.Player.Braking.canceled += Stopbrake;
    /// </summary>
    public void EnableInputs()
    {
        playerInput.actions.Enable();

        playerInput.actions.FindAction("Movement").performed += GetMovement;
        playerInput.actions.FindAction("Turning").performed += GetTurning;
        playerInput.actions.FindAction("Braking").performed += GetBrake;
        playerInput.actions.FindAction("Pausing").performed += GetPausing;
        playerInput.actions.FindAction("Movement").canceled += StopMovement;
        playerInput.actions.FindAction("Turning").canceled += StopTurning;
        playerInput.actions.FindAction("Braking").canceled += StopBrake;
    }

    /// <summary>
    /// NOTE TO SELF:
    /// 
    /// input.Player.Movement.performed -= GetMovement;
    /// input.Player.Turning.performed -= GetTurning;
    /// input.Player.Braking.performed -= GetBrake;
    /// input.Player.Movement.canceled -= StopMovement;
    /// input.Player.Turning.canceled -= StopTurning;
    /// input.Player.Braking.canceled -= Stopbrake;
    /// </summary>
    public void DisableInputs()
    {
        playerInput.actions.Disable();

        playerInput.actions.FindAction("Movement").performed -= GetMovement;
        playerInput.actions.FindAction("Turning").performed -= GetTurning;
        playerInput.actions.FindAction("Braking").performed -= GetBrake;
        playerInput.actions.FindAction("Pausing").performed -= GetPausing;
        playerInput.actions.FindAction("Movement").canceled -= StopMovement;
        playerInput.actions.FindAction("Turning").canceled -= StopTurning;
        playerInput.actions.FindAction("Braking").canceled -= StopBrake;
    }
}
