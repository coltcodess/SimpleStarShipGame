using UnityEngine;

public class GameTimer : MonoBehaviour 
{
    public static float g_gameTimer;
    [SerializeField] float levelTimeLimit = 10f;
    private GameManager m_gameManager;
    private bool b_isActive;

    private void OnEnable()
    {
        EventBroker.GameOver += StopTimer;        
    }

    private void Start() 
    {
        b_isActive = true;
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

    public void StopTimer()
    {
        b_isActive = false;
    }

    private void Update() 
    {
        if(b_isActive)
        {
            CalculateTimer();
        }            
    }

    private void CalculateTimer()
    {
        g_gameTimer += Time.deltaTime;

        if (g_gameTimer >= levelTimeLimit)
        {
            b_isActive = false;
            EventBroker.CallLevelComplete();
            g_gameTimer = 0f;
        }
    }

    private void OnDisable() 
    {
        EventBroker.GameOver -= StopTimer;
    }
    
}