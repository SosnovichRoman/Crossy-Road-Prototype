using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent GameStarted = new UnityEvent();
    public static UnityEvent GamePaused = new UnityEvent();
    public static UnityEvent GameResumed = new UnityEvent();
    public static UnityEvent GameOver = new UnityEvent();
    public static UnityEvent GameRestart = new UnityEvent();

    public static void SendPlayerDead()
    {
        GameOver.Invoke();
    }

    public static void SendGameStart()
    {
        GameStarted.Invoke();
    }

    public static void SendGameRestart()
    {
        GameRestart.Invoke();
    }

    public static void SendGamePaused()
    {
        GamePaused.Invoke();
    }

    public static void SendGameResumed()
    {
        GameResumed.Invoke();
    }
}
