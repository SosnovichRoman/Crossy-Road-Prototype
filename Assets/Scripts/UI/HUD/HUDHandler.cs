using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHandler : MonoBehaviour
{
    private bool isPaused;
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void SwitchGamePause()
    {
        if(!isPaused) {Time.timeScale = 0; isPaused = true; EventManager.SendGamePaused(); }
        else {Time.timeScale = 1; isPaused = false; EventManager.SendGameResumed(); }
    }
}
