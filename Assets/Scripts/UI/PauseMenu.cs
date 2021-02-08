using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{   
    public static bool isPaused = false;

    public GameObject m_pauseMenu;

    private GameManager m_gameManager;


    void Start()
    {
        m_gameManager = GameManager.Instance;

    }
    
    void Update()
    {
        CheckInputs();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    public void Resume()
    {
        HidePauseScreen();
    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                m_gameManager.SetGameState(GameManager.GameState.PAUSE);
                DisplayPauseScreen();
            }
            else
            {
                m_gameManager.SetGameState(GameManager.GameState.GAME);
                HidePauseScreen();
            }
        }
    }

    private void HidePauseScreen()
    {
        m_pauseMenu.SetActive(false);
        isPaused = false;
        SetTimeScale(1f);
    }

    private void DisplayPauseScreen()
    {
        m_pauseMenu.SetActive(true);
        isPaused = true;
        SetTimeScale(0f);
    }

    private void SetTimeScale(float p_value)
    {
        Time.timeScale = p_value;
    }

    
}
