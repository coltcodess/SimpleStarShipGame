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
    PlayerController playerController;
    GameTimer gameTimer;

    private GameState m_state;

    public enum GameState
    {
        MENU,
        GAME,
        PAUSE,        
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

    private void Start() 
    {
        FindObjects();
    }

    private void Awake() 
    {
        if (_instance == null) _instance = this;        

        else if (_instance != this) Destroy(gameObject);

        DontDestroyOnLoad(_instance);
    }
    
    public void StartGame()
    {
        FindObjects();

        gameTimer.ResetTimer();
        EventBroker.CallGameStarted();
        isGameActive = true;
        SetGameState(GameState.GAME);
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


}
