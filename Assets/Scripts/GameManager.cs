using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    PlayerController playerController;
    PauseMenu pauseGame;
    WinScreen winScreen;
    Timer timer;

    public GameObject canvas;


    [Header("Camera Settings:")]

    [SerializeField]
    private CinemachineTargetGroup _targetGroup;

    [SerializeField]
    private CinemachineVirtualCamera _stageCamera;

    [SerializeField]
    private CinemachineVirtualCamera _playerCamera;


    [Header("Player Starting Positions:")]

    [SerializeField]
    private List<Transform> spawnTransforms = new List<Transform>();


    [Header("Kart Colors:")]

    [SerializeField]
    private List<Color32> playerColors = new List<Color32>();

    private static List<Color32> chosenColors = new List<Color32>();


    [Header("Current Item - UI Positions:")]

    public List<Transform> itemPositions = new List<Transform>();


    [Header("Sprites - KartParts:")]

    [SerializeField]
    private SpriteRenderer p1_Car_1_Body;

    [SerializeField]
    private SpriteRenderer p1_Car_2_Body;

    [SerializeField]
    private SpriteRenderer p2_Car_1_Body;

    [SerializeField]
    private SpriteRenderer p2_Car_2_Body;

    [SerializeField]
    private SpriteRenderer p1_Car_1_Roof;

    [SerializeField]
    private SpriteRenderer p1_Car_2_Roof;

    [SerializeField]
    private SpriteRenderer p2_Car_1_Roof;

    [SerializeField]
    private SpriteRenderer p2_Car_2_Roof;


    [HideInInspector]
    public int playersFinishedRace = 0;

    public static Action playerFinished;
    public static Action pauseGameAction;


    [Header("Scene In Use:")]

    public Scenes currentScene;

    private GameObject player1;
    private GameObject player2;

    private List<GameObject> UIPlayers = new List<GameObject>();

    

    private void Awake()
    {
        pauseGame = FindObjectOfType<PauseMenu>();
        winScreen = FindObjectOfType<WinScreen>();
        timer = FindObjectOfType<Timer>();

        if (currentScene == Scenes.MainMenu)
        {
            FindPlayerSprites();
            chosenColors.Clear();
            return;
        }
        playerInputManager = GetComponent<PlayerInputManager>();
        pauseGameAction += PlayerPausedGame;


        playerInputManager.JoinPlayer(/*0, 0, */controlScheme: "KeyboardOne", pairWithDevice: Keyboard.current); // Can leave empty.
        playerInputManager.JoinPlayer(controlScheme: "KeyboardTwo", pairWithDevice: Keyboard.current);

        _stageCamera.Priority = 1;
        _playerCamera.Priority = 0;

        StartCoroutine(CameraPriorityCooldown());
    }
    public IEnumerator CameraPriorityCooldown()
    {
        yield return new WaitForSeconds(1f);
        
        _stageCamera.Priority = 0;
        _playerCamera.Priority = 1;
        
        yield return new WaitForSeconds(2.5f);
        
        timer.InitializeCountDown();
    }

    public void PlayerJoinedGame(PlayerInput playerinput)
    {
        int currentCar = PlayerPrefs.GetInt($"Player{playerinput.playerIndex}");
        //Player 1 joins = playerIndex = 0
        //Player 2 joins = playerIndex = 1
        playerinput.transform.position = spawnTransforms[playerinput.playerIndex].position;

        if (playerinput.playerIndex == 0)
        {
            player1 = playerinput.gameObject;

            CarColor carColor = player1.AddComponent<CarColor>();
            playerinput.transform.GetChild(currentCar).gameObject.SetActive(true);
            carColor.InitializeCarParts(currentCar);
            carColor.SetCarPartsColor(chosenColors, 0);
            _targetGroup.AddMember(player1.transform, 1, 0);
            DisplayItemText itemText = playerinput.GetComponent<DisplayItemText>();

            itemText.InitializeItemText(this, playerinput.playerIndex);

            PlayerController playerController = playerinput.GetComponent<PlayerController>();
            playerController.GetItemDisplayText(itemText);
        }
        else if (playerinput.playerIndex == 1 )
        {
            player2 = playerinput.gameObject;

            CarColor carColor = player2.AddComponent<CarColor>();
            playerinput.transform.GetChild(currentCar).gameObject.SetActive(true);
            carColor.InitializeCarParts(currentCar);
            carColor.SetCarPartsColor(chosenColors, 2);
            _targetGroup.AddMember(player2.transform, 1, 0);
            DisplayItemText itemText = playerinput.GetComponent<DisplayItemText>();

            itemText.InitializeItemText(this, playerinput.playerIndex);

            PlayerController playerController = playerinput.GetComponent<PlayerController>();
            playerController.GetItemDisplayText(itemText);
        }
        //playerinput.tag = playerinput.playerIndex == 0 ? "PlayerOne" : "PlayerTwo";
        
        if(playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
        //PlayerPrefs.SetInt($"Player{playerinput.playerIndex}", -1);
    }

    public void FindPlayerSprites()
    {
        if (UIPlayers.Count >= 2)
        {
            UIPlayers.Clear();
        }
        UIPlayers.Add(GameObject.Find("PlayerOneCar"));
        UIPlayers.Add(GameObject.Find("PlayerTwoCar"));
    }

    public void CarSelector(int playerIndex)
    {
        GameObject car1 = UIPlayers[playerIndex].transform.GetChild(0).gameObject;
        GameObject car2 = UIPlayers[playerIndex].transform.GetChild(1).gameObject;
        
        car1.SetActive(!car1.activeSelf);
        car2.SetActive(!car2.activeSelf);
    }

    public void SaveSelectedCars()
    {
        for (int i = 0; i < UIPlayers.Count; i++) {
            for (int j = 0; j < UIPlayers[i].transform.childCount; j++) {
                if (UIPlayers[i].transform.GetChild(j).gameObject.activeSelf == true) {
                    PlayerPrefs.SetInt($"Player{i}", j);
                    break;
                }
            }
        }
    }

    public void SaveSelectedColors()
    {
        chosenColors.Add(p1_Car_1_Body.color);
        chosenColors.Add(p1_Car_1_Roof.color);
        chosenColors.Add(p2_Car_1_Body.color);
        chosenColors.Add(p2_Car_1_Roof.color);
    }

    public void CarOneBodyColor(int colorValue) {
        p1_Car_1_Body.color = playerColors[colorValue];
        p1_Car_2_Body.color = playerColors[colorValue];
    }

    public void CarOneRoofColor(int colorValue) {
        p1_Car_1_Roof.color = playerColors[colorValue];
        p1_Car_2_Roof.color = playerColors[colorValue];
    }

    public void CarTwoBodyColor(int colorValue) {
        p2_Car_1_Body.color = playerColors[colorValue];
        p2_Car_2_Body.color = playerColors[colorValue];
    }

    public void CarTwoRoofColor(int colorValue) {
        p2_Car_1_Roof.color = playerColors[colorValue];
        p2_Car_2_Roof.color = playerColors[colorValue];
    }


    /// <summary>
    /// NOTE TO SELF: Four Methods -> One Method
    /// public void LoadLevel_1() 
    /// {
    ///     SceneLoader.LoadScene(Scenes.TrackLevel_1);
    ///     PlayerPrefs.SetInt("LevelLoaded", 1);
    /// }
    /// public void LoadLevel_2() 
    /// {
    ///     SceneLoader.LoadScene(Scenes.TrackLevel_2);
    ///     PlayerPrefs.SetInt("LevelLoaded", 1);
    /// }
    /// public void LoadLevel_3() 
    /// {
    ///     SceneLoader.LoadScene(Scenes.TrackLevel_3);
    ///     PlayerPrefs.SetInt("LevelLoaded", 1);
    /// }
    /// public void LoadLevel_4() 
    /// {
    ///     SceneLoader.LoadScene(Scenes.TrackLevel_4);
    ///     PlayerPrefs.SetInt("LevelLoaded", 1);
    /// }
    /// </summary>
    public void LoadLevels(int level)
    {
        SceneLoader.LoadScene((Scenes)level);
        PlayerPrefs.SetInt("LevelLoaded", 1);
    }

    public void LoadAllLevels() {
        SceneLoader.LoadScene(Scenes.TrackLevel_1);
        PlayerPrefs.SetInt("LevelLoaded", 0);
    }

    public void LoadNext()
    {
        int currentLevelLoaded = PlayerPrefs.GetInt("LevelLoaded");
        if (currentLevelLoaded == 1)
        {
            SceneLoader.LoadScene(Scenes.MainMenu);
            return;
        }
        SceneLoader.LoadScene(++currentScene);
        if (currentScene > Scenes.TrackLevel_4)
        {
            SceneLoader.LoadScene(Scenes.MainMenu);
        }
    }

    public void PlayersFinished()
    {
        playersFinishedRace++;
        if (playersFinishedRace >= playerInputManager.playerCount)
        {
            winScreen.raceIsFinished = true;
            WinScreen();
        }       
    }

    public void WinScreen()
    {
        if (winScreen.raceIsFinished)
        {
            winScreen.EnableWinScreen();
            timer.levelName.text = $"Level {((int)currentScene)}";
            Time.timeScale = 0f;
        }
    }

    public void PlayerPausedGame()
    {
        if (pauseGame.gameIsPaused) {
            pauseGame.SetGameResumed();
        }
        else {
            pauseGame.SetGamePaused();
        }
    }


    public void ItemBox(ItemBox itemBox)
    {
        StartCoroutine(ItemBoxCooldown(itemBox));
    }

    private IEnumerator ItemBoxCooldown(ItemBox itemBox)
    {
        yield return new WaitForSeconds(20f);
        
        itemBox.gameObject.SetActive(true);
        itemBox.RandomizeItemBox();
    }


    public void StartSpeedBoostCooldown(float amount, GameObject playerTarget)
    {
        StartCoroutine(SpeedBoostEffectTime(amount, playerTarget));
    }

    public IEnumerator SpeedBoostEffectTime(float amount, GameObject playerTarget)
    {
        PlayerController player = playerTarget.GetComponent<PlayerController>();

        yield return new WaitForSeconds(8f);
        
        player.playerHasItem = false;

        player.accelerationSpeed -= amount;
        player.currentMaxSpeed -= amount;
        player.currentBackingMaxSpeed -= amount;
        player.SetItemDisplayText(EItemEffect.NoItem);
    }

    public void StartTurnBoostCooldown(float amount, GameObject playerTarget)
    {
        StartCoroutine(TurnBoostEffectTime(amount, playerTarget));
    }

    public IEnumerator TurnBoostEffectTime(float amount, GameObject playerTarget)
    {
        PlayerController player = playerTarget.GetComponent<PlayerController>();

        yield return new WaitForSeconds(10f);

        player.playerHasItem = false;

        player.rotationSpeed -= amount;
        player.SetItemDisplayText(EItemEffect.NoItem);
    }


    public void StartKnockbackBoostCooldown(float amount, GameObject playerTarget)
    {
        StartCoroutine(KnockbackBoostEffectTime(amount, playerTarget));
    }

    public IEnumerator KnockbackBoostEffectTime(float amount, GameObject playerTarget)
    {
        PlayerKnockback playerKnockBack = playerTarget.GetComponent<PlayerKnockback>();
        PlayerController player = playerTarget.GetComponent<PlayerController>();

        yield return new WaitForSeconds(10f);

        player.playerHasItem = false;

        playerKnockBack.knockbackForce -= amount;
        player.SetItemDisplayText(EItemEffect.NoItem);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnEnable() {
        playerFinished += PlayersFinished;
        
    }

    private void OnDisable() {
        playerFinished -= PlayersFinished;
        pauseGameAction -= PlayerPausedGame;
    }
}