using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Queue<int> _scenes = new Queue<int>();

    

    private void Start() 
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            _scenes.Enqueue(i);

        }
        
    }

    public void LoadStartScene(string level)
    {        
        SceneManager.LoadScene(level);        
    }

    public void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex;

        if(nextScene > SceneManager.sceneCount)
        {
            LoadMainMenu();
        }
        else
        {
            SceneManager.LoadScene(nextScene + 1);
        }        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
