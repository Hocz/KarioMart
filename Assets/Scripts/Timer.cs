using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Race Start Counter - UI:")]
    
    [SerializeField]
    private TextMeshProUGUI startCounter;


    [Header("Player 1 - Lap Counter UI:")]
    
    [SerializeField]
    private TextMeshProUGUI player_1_lapOneCounter;

    [SerializeField]
    private TextMeshProUGUI player_1_lapTwoCounter;

    [SerializeField]
    private TextMeshProUGUI player_1_lapThreeCounter;


    [Header("Player 2 - Lap Counter UI:")]
    
    [SerializeField]
    private TextMeshProUGUI player_2_lapOneCounter;

    [SerializeField]
    private TextMeshProUGUI player_2_lapTwoCounter;

    [SerializeField]
    private TextMeshProUGUI player_2_lapThreeCounter;

    [HideInInspector]
    public TextMeshProUGUI levelName;

    [Header("Count Down - Starting Time:")]

    [SerializeField]
    private float currentTime;

    [HideInInspector]
    public bool isCounting = false;

    [HideInInspector]
    public bool startRace = false;

    [HideInInspector]
    public int currentLapCounterOne = 1;
    
    [HideInInspector]
    public int currentLapCounterTwo = 1;

    private float currentLapTimeOne;
    private float currentLapTimeTwo;

    private void Update()
    {
        if (isCounting)
        {
            StartRaceCounter();
        }
        if(startRace)
        {
            StartLapsTimeCounter();
        }
    }

    public void InitializeCountDown()
    {
        startCounter.gameObject.SetActive(true);
        isCounting = true;
    }

    private void StartRaceCounter()
    {
        if (isCounting == false) return;
        startCounter.text = currentTime.ToString("0");
        StartCoroutine(DelayCoolDown());
    }

    private void CountDown()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 1)
        {
            isCounting = false;
            StartCoroutine(EndTextCooldown());
        }
    }

    private IEnumerator DelayCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        CountDown();
    }

    private IEnumerator EndTextCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        startCounter.text = "GO";

        yield return new WaitForSeconds(1f);
        startCounter.gameObject.SetActive(false);
        startRace = true;
    }

    private void StartLapsTimeCounter()
    {
        currentLapTimeOne += Time.deltaTime;
        currentLapTimeTwo += Time.deltaTime;
    }

    public void SetLapTimeOne()
    {
        if (currentLapCounterOne == 1)
        {
            player_1_lapOneCounter.text = currentLapTimeOne.ToString("0.00");
        }
        if(currentLapCounterOne == 2)
        {
            player_1_lapTwoCounter.text = currentLapTimeOne.ToString("0.00");
        }
        if(currentLapCounterOne >= 3)
        {
            player_1_lapThreeCounter.text = currentLapTimeOne.ToString("0.00");
        }
        currentLapTimeOne = 0;
    }

    public void SetLapTimeTwo()
    {
        if (currentLapCounterTwo == 1)
        {
            player_2_lapOneCounter.text = currentLapTimeTwo.ToString("0.00");
        }
        if (currentLapCounterTwo == 2)
        {
            player_2_lapTwoCounter.text = currentLapTimeTwo.ToString("0.00");
        }
        if (currentLapCounterTwo >= 3)
        {
            player_2_lapThreeCounter.text = currentLapTimeTwo.ToString("0.00");
        }
        currentLapTimeTwo = 0;
    }
}
