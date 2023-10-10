using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    
    private InputManager input;

    public Rigidbody2D rigidbody2d;

    public PlayerKnockback playerKnockback;
    
    Timer timer;
    DisplayItemText itemText;


    [Header("Player Movement Settings:")]
    
    [SerializeField]
    public float currentMaxSpeed = 10f;

    [SerializeField]
    public float accelerationSpeed = 5f;

    [SerializeField]
    public float decelerationSpeed = 5f;
    
    [HideInInspector]
    private float currentMovementSpeed = 0f;
    
    [HideInInspector]
    public float currentMoveDirection = 1f;

    [SerializeField]
    public float rotationSpeed = 80f;

    private float lastRotationDirection;

    [HideInInspector]
    public bool canMovePlayer;

    [HideInInspector]
    public bool canRotatePlayer;

    [HideInInspector]
    public bool playerIsBreaking;

    [HideInInspector]
    public bool playerHasItem;

    [HideInInspector]
    public int lapNumber;

    [HideInInspector]
    public int checkpointIndex;

    /// <summary>
    /// NOTE TO SELF: 
    /// public int GetPlayerIndex() => playerInput.playerIndex;
    /// =
    /// public int GetPlayerIndex()
    /// {
    ///     return playerInput.playerIndex;
    /// }
    /// </summary>
    public int GetPlayerIndex() => playerInput.playerIndex;

    private void Start()
    {
        timer = FindAnyObjectByType<Timer>();
        
        lapNumber = 1;
        checkpointIndex = 0;
        
        canMovePlayer = true;
        canRotatePlayer = true;
        playerIsBreaking = false;
        playerHasItem = false;
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
        BrakePlayer();
    }

    public void GetItemDisplayText(DisplayItemText _itemText)
    {
        itemText = _itemText;
    }

    public void SetItemDisplayText(EItemEffect itemEffect)
    {
        switch (itemEffect)
        {
            case EItemEffect.NoItem:
                itemText.SetText("");
                break;

            case EItemEffect.SpeedBoost:
                itemText.SetText("Speed");
                break;

            case EItemEffect.SpeedDebuff:
                itemText.SetText("Slow");
                break;

            case EItemEffect.TurnBoost:
                itemText.SetText("Turn");
                break;

            case EItemEffect.KnockbackBoost:
                itemText.SetText("Knockback");
                break;

            default:
                Debug.LogError("DisplayItemText Error!");
                break;
        }
    }

    private void MovePlayer()
    {
        if (canMovePlayer && timer.startRace)
        {
            CalculateMovementSpeed(input.MoveDirValueProperty);
            if (input.MoveDirValueProperty > 0)
            {
                currentMoveDirection += accelerationSpeed * Time.deltaTime;
                //currentMoveDirection = Mathf.Clamp01(currentMoveDirection);
                currentMoveDirection = Mathf.Clamp(currentMoveDirection, 0, 1); 
            }
            else if (input.MoveDirValueProperty < 0)
            {
                currentMoveDirection -= accelerationSpeed * Time.deltaTime;
                currentMoveDirection = Mathf.Clamp(currentMoveDirection, -1, 1);
            }
            rigidbody2d.velocity = (rigidbody2d.transform.up * currentMovementSpeed * currentMoveDirection);
        }
    }

    public void ResetSpeed()
    {
        rigidbody2d.velocity = Vector3.zero;
    }

    private void CalculateMovementSpeed(float moveDirValueProperty)
    {
        if (Mathf.Abs(input.MoveDirValueProperty) > 0)
        {
            currentMovementSpeed += accelerationSpeed * Time.deltaTime;
        }
        else
        {
            currentMovementSpeed -= decelerationSpeed * Time.deltaTime;
        }
        currentMovementSpeed = Mathf.Clamp(currentMovementSpeed, 0f, currentMaxSpeed);
    }

    private void RotatePlayer()
    {
        if (canRotatePlayer) 
        {
            if (currentMovementSpeed != 0)
            {
                rigidbody2d.rotation += -input.MoveTurnValueProperty * rotationSpeed * Time.deltaTime;
                lastRotationDirection = rigidbody2d.rotation;
            }
            else
            {
                rigidbody2d.rotation = lastRotationDirection;
            }
        } 
    }

    private void BrakePlayer()
    {
        if(input.BreakValueProperty || playerIsBreaking)
        {
            decelerationSpeed = 10f;
            currentMovementSpeed -= decelerationSpeed * Time.deltaTime;
        }
        else if (input.BreakValueProperty == false || playerIsBreaking == false)
        {
            decelerationSpeed = 5f;
        }
    }

    private void OnEnable()
    {
        input = new InputManager(playerInput);
        input.EnableInputs();
    }

    private void OnDisable()
    {
        input.DisableInputs();
    }
}