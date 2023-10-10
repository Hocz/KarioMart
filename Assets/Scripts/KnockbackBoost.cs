using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/KnockbackBoost")]

public class KnockbackBoost : ItemEffect
{
    GameManager gameManager;

    public float amount;

    public override void ApplyEffect(PlayerController playerTarget)
    {
        base.ApplyEffect(playerTarget);

        PlayerKnockback playerKnockback = playerTarget.GetComponent<PlayerKnockback>();
        gameManager = FindAnyObjectByType<GameManager>();

        playerTarget.playerHasItem = true;
        playerKnockback.knockbackForce += amount;

        gameManager.StartKnockbackBoostCooldown(amount, playerTarget.gameObject);
    }
}
