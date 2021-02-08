using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static bool isGameActive;    

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject completeScreen;
    [SerializeField] GameObject pauseScreen;
    PlayerController playerController;
    GameTimer gameTimer;

    private GameState m_state;

    public enum GameState
    {
        MENU,
        GAME,
        PAUSE
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    private void Awake() 
    {
        if (_instance == null) _instance = this;        

        else if (_instance != this) Destroy(gameObject);

        DontDestroyOnLoad(_instance);
    }
    
    private void Start() 
    { 
        FindObjects();
        ListenEvents();
        
    }

    private void ListenEvents()
    {
        if(playerController)
        {
            playerController.OnPlayerDeathEvent += GameOver;
        }
        
    }

    private void FindObjects()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameTimer = FindObjectOfType<GameTimer>();
    }

    public void SetGameState(GameState p_state)
    {
        m_state = p_state;
    }

    public void StartGame()
    {        
        isGameActive = true;
        SetGameState(GameState.GAME); 
        gameTimer.ResetTimer();
        EventBroker.CallGameStarted();
    }

    public void LevelComplete()
    {
        isGameActive = false;
        SetGameState(GameState.MENU);
        completeScreen.SetActive(true);
        EventBroker.CallGameEnded();
    }

    public void GameOver()
    {        
        isGameActive = false;
        SetGameState(GameState.MENU);
        gameOverScreen.SetActive(true);
        EventBroker.CallGameEnded();
    }

    public void PauseGame()
    {
        SetGameState(GameState.PAUSE);
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        isGameActive = false;
    }

    public void UnPauseGame()
    {
        SetGameState(GameState.GAME);
        Time.timeScale = 1f;
        pauseScreen.SetActive(true);
        isGameActive = false;
    }
}
