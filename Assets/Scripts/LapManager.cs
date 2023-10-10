using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LapManager : MonoBehaviour
{
    Timer timer;

    [Header("UI - Laps Counters:")]

    [SerializeField]
    private TextMeshProUGUI lapsCounter_1;

    [SerializeField]
    private TextMeshProUGUI lapsCounter_2;


    [Header("UI - Total Laps:")]

    [SerializeField]
    private TextMeshProUGUI TotalLaps_1;

    [SerializeField]
    private TextMeshProUGUI TotalLaps_2;


    [Header("Checkpoints:")]

    public List<Checkpoint> checkpoints = new List<Checkpoint>();
    public int totalLaps;

    private void Awake()
    {
        TotalLaps_1.text = totalLaps.ToString();
        TotalLaps_2.text = totalLaps.ToString();
        
        timer = FindObjectOfType<Timer>();
    }

    /// <summary>
    /// NOTE TO SELF:
    /// GameManager.playerFinished?.Invoke();
    /// =
    /// if (GameManager.playerFinished.Method != null)
    /// {
    ///     GameManager.playerFinished.Invoke();
    /// }
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (!player) return;
        // Player 1 - Lap Counter
        if (player.GetPlayerIndex() == 0)
        {            
            if (player.checkpointIndex == checkpoints.Count)
            {
                player.checkpointIndex = 0;
                player.lapNumber++;

                timer.SetLapTimeOne();
                timer.currentLapCounterOne++;

                if (player.lapNumber <= totalLaps)
                {
                    lapsCounter_1.text = player.lapNumber.ToString();
                }

                if (player.lapNumber > totalLaps)
                {
                    player.decelerationSpeed = 15f;
                    player.playerIsBreaking = true;
                    GameManager.playerFinished?.Invoke();
                }
            }
        }
        // Player 2 - Lap Counter
        else if (player.GetPlayerIndex() == 1)
        {
            if (player.checkpointIndex == checkpoints.Count)
            {
                player.checkpointIndex = 0;
                player.lapNumber++;

                timer.SetLapTimeTwo();
                timer.currentLapCounterTwo++;

                if (player.lapNumber <= totalLaps)
                {
                    lapsCounter_2.text = player.lapNumber.ToString();
                }

                if (player.lapNumber > totalLaps)
                {
                    player.decelerationSpeed = 15f;
                    player.playerIsBreaking = true;
                    GameManager.playerFinished?.Invoke();
                }
            }
        }
    }
}
