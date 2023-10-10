using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/SpeedBoost")]

public class SpeedBoost : ItemEffect
{
    GameManager gameManager;
    
    public float amount;

    public override void ApplyEffect(PlayerController playerTarget)
    {
        base.ApplyEffect(playerTarget);
        
        PlayerController player = playerTarget.GetComponent<PlayerController>();
        gameManager = FindAnyObjectByType<GameManager>();
        
        player.playerHasItem = true;
        player.accelerationSpeed += amount;
        player.currentMaxSpeed += amount;

        gameManager.StartSpeedBoostCooldown(amount, playerTarget.gameObject);
    }
}
