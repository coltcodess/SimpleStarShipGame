using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Queue<int> m_scenes = new Queue<int>();    

    private void Start() 
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            m_scenes.Enqueue(i);
        }
        
    }
    
    public void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex;
       

        if(nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene + 1);
        }
        else
        {
            LoadMainMenu();
        }        
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
