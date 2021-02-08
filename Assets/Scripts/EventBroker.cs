using System;

public class EventBroker
{
    public static Action GameEnded;
    public static Action GameStarted;
    
    

    public static void CallGameEnded()
    {
        if(GameEnded != null)
        {
            GameEnded();
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
