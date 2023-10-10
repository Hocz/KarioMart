using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    [SerializeField]
    protected EItemEffect effect;

    public virtual void ApplyEffect(PlayerController playerTarget)
    {
        playerTarget.SetItemDisplayText(effect);
    }
}

public enum EItemEffect
{
    NoItem, SpeedBoost, SpeedDebuff, TurnBoost, KnockbackBoost 
}
