using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject bestScoreText;

    private bool isPaused;
    private void Awake()
    {
        EventManager.GameOver.AddListener(EnableBestScore);
    }
    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) SwitchGamePause();
    }
    public void SwitchGamePause()
    {
        if(!isPaused) {Time.timeScale = 0; isPaused = true; EventManager.SendGamePaused(); }
        else {Time.timeScale = 1; isPaused = false; EventManager.SendGameResumed(); }
    }

    public void EnableBestScore()
    {
        bestScoreText?.SetActive(true);
    }
}
