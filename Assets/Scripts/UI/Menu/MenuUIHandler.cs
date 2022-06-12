using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private ScreenManager screenManager;//refactor
    public void StartGame()
    {
        EventManager.SendGameStart();
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        EventManager.SendGameRestart();
        gameObject.SetActive(false);
    }

    public void OpenSkins()
    {
        screenManager.EnableSkinScreen(); //refactor
    }
}
