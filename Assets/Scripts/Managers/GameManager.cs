using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static bool isGameActive;    

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject completeScreen;
    [SerializeField] GameObject pauseScreen;
    PlayerController playerController;

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
        
    }

    private void FindObjects()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void SetGameState(GameState p_state)
    {
        m_state = p_state;
    }

    public void StartGame()
    {        
        isGameActive = true;
        SetGameState(GameState.GAME); 

        EventBroker.CallGameStarted();
    }

    public void EndGame()
    {        
        isGameActive = false;

        SetGameState(GameState.MENU);
        
        if (playerController.isDead)
        {
            gameOverScreen.SetActive(true);
        } 
        else
        {
            completeScreen.SetActive(true);
        }

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
