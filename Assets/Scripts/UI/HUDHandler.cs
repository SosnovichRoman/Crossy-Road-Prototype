using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHandler : MonoBehaviour
{
    private bool isPaused;
    public void SwitchGamePause()
    {
        if(!isPaused) {Time.timeScale = 0; isPaused = true;}
        else {Time.timeScale = 1; isPaused = false;}
    }
}
