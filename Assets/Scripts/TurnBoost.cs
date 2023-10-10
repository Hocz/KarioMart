using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/TurnBoost")]

public class TurnBoost : ItemEffect
{
    GameManager gameManager;

    public float amount;

    public override void ApplyEffect(PlayerController playerTarget)
    {
        base.ApplyEffect(playerTarget);

        PlayerController player = playerTarget.GetComponent<PlayerController>();
        gameManager = FindAnyObjectByType<GameManager>();

        player.playerHasItem = true;
        player.rotationSpeed += amount;

        gameManager.StartTurnBoostCooldown(amount, playerTarget.gameObject);
    }
}
