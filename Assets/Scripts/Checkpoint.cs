using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Order Number:")]
    public int indexValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {   
            if(player.checkpointIndex == indexValue - 1)
            {
                player.checkpointIndex = indexValue;
            }
        }
    }
}
