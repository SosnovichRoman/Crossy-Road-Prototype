using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent GameStarted = new UnityEvent();
    public static UnityEvent GameOver = new UnityEvent();

    public static void SendPlayerDead()
    {
        GameOver.Invoke();
    }

    public static void SendGameStart()
    {
        GameStarted.Invoke();
        Debug.Log("Invoke");
    }

}
