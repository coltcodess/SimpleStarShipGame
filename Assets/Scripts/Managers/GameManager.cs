using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

namespace StarShip
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance = null;

        PlayerController playerController;

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

        private void Awake()
        {
            if (_instance == null) _instance = this;

            else if (_instance != this) Destroy(gameObject);

            DontDestroyOnLoad(_instance);
        }

        private void Start()
        {
            StartGame();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            FindObjects();
            StartGame();
        }

        public void StartGame()
        {
            SetGameState(GameState.GAME);
            GameTimer.ResetTimer();
            EventBroker.CallGameStarted();
        }

        private void FindObjects()
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        public void SetGameState(GameState p_state)
        {
            m_state = p_state;
        }


    } 
}


