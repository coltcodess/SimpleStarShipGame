using System;

public class EventBroker
{
    public static Action GameOver;
    public static Action LevelComplete;
    public static Action GameStarted;
    
    

    public static void CallGameOver()
    {
        if(GameOver != null)
        {
            GameOver();
        }
    }

    public static void CallLevelComplete()
    {
        if (LevelComplete != null)
        {
            LevelComplete();
        }
    }

    public static void CallGameStarted()
    {
        if (GameStarted != null)
        {
            GameStarted();
        }
    }

    
}
