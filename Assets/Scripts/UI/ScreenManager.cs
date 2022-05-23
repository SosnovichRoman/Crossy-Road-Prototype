using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject startScreen;
    [SerializeField]
    private float enableGameOverScreenDelay;

    private void Awake()
    {
        EventManager.GameOver.AddListener(() => StartCoroutine(EnableGameOverScreen()));
    }

    private IEnumerator EnableGameOverScreen()
    {
        yield return new WaitForSeconds(enableGameOverScreenDelay);
        gameOverScreen.SetActive(true);
    }

    //Not used
    private void EnableStartScreen()
    {
        startScreen.SetActive(true);
    }
    private void DisableStartScreen()
    {
        startScreen.SetActive(false);
    }
}
