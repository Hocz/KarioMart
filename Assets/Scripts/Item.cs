using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    PlayerController player;
    public ItemEffect itemEffect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerController>();

        gameObject.SetActive(false);
        if (player.playerHasItem == false)
        {
            itemEffect.ApplyEffect(player);
        }
    }
}
