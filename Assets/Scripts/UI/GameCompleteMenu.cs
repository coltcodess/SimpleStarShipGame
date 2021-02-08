using UnityEngine;

public class GameCompleteMenu : MonoBehaviour 
{
    
    public GameObject m_completeLevelMenu;
    public GameObject m_gameOverMenu;

    private GameManager m_gameManager;

    private void OnEnable()
    {
        EventBroker.GameOver += GameOver;
        EventBroker.LevelComplete += LevelComplete;
    }

    void Start()
    {
        m_gameManager = GameManager.Instance;
    }

    public void LevelComplete()
    {
        m_gameManager.SetGameState(GameManager.GameState.MENU);
        m_completeLevelMenu.SetActive(true);       
    }

    public void GameOver()
    {
        m_gameManager.SetGameState(GameManager.GameState.MENU);
        m_gameOverMenu.SetActive(true);        
    }

    private void OnDisable() 
    {
        EventBroker.GameOver -= GameOver;
        EventBroker.LevelComplete -= LevelComplete;
    }


}