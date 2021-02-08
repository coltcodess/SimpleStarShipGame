using UnityEngine;

public class GameTimer : MonoBehaviour 
{
    public static float g_gameTimer;
    [SerializeField] float levelTimeLimit = 10f;
    private GameManager m_gameManager;

    private void Start() 
    {
        m_gameManager = GameManager.Instance;
    }

    public static float GetGameTimer()
    {
        return Mathf.Round(g_gameTimer);
    }

    public void ResetTimer()
    {
        g_gameTimer = 0f;
    }

    private void Update() 
    {
        CalculateTimer();    
    }

    private void CalculateTimer()
    {
        g_gameTimer += Time.deltaTime;

        if (g_gameTimer >= levelTimeLimit)
        {
            m_gameManager.GameOver();
        }
    }
    
}