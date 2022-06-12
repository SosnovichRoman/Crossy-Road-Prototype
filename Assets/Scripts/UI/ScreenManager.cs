using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject startScreen;
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private GameObject HUD;
    [SerializeField]
    private GameObject transitionScreen;
    [SerializeField]
    private GameObject skinScreen;
    [SerializeField]
    private float restartDelay;
    [SerializeField]
    private float enableGameOverScreenDelay;

    private void Awake()
    {
        EventManager.GameStarted.AddListener(() => StartCoroutine(StartWithTransition()));
        EventManager.GameStarted.AddListener(EnableHUD);
        EventManager.GamePaused.AddListener(EnablePauseScreen);
        EventManager.GameResumed.AddListener(DisablePauseScreen);
        EventManager.GameOver.AddListener(() => StartCoroutine(EnableGameOverScreen()));
        EventManager.GameOver.AddListener(DisableHUD);
        EventManager.GameRestart.AddListener(() => StartCoroutine(RestartWithTransition()));
    }

    private IEnumerator EnableGameOverScreen()
    {
        yield return new WaitForSeconds(enableGameOverScreenDelay);
        gameOverScreen.SetActive(true);
    }

    private IEnumerator StartWithTransition()
    {
        transitionScreen?.SetActive(true);
        Animator animator = transitionScreen?.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("t_titleOut");
            //Delay
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator RestartWithTransition()
    {
        transitionScreen?.SetActive(true);
        Animator animator = transitionScreen?.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("t_fadeIn");
            //Delay
            yield return new WaitForSecondsRealtime(restartDelay);
        }
        SceneManager.LoadScene("MainScene");
    }

    public void EnableSkinScreen()
    {
        skinScreen?.SetActive(true);
        DisableAllScreens();
    }



    private void EnableHUD()
    {
        HUD?.SetActive(true);
    }
    private void DisableHUD()
    {
        HUD?.SetActive(false);
    }
    private void EnablePauseScreen()
    {
        pauseScreen?.SetActive(true);
    }
    private void DisablePauseScreen()
    {
        pauseScreen?.SetActive(false);
    }

    private void DisableAllScreens()
    {
        foreach (Transform screen in transform)
        {
            screen.gameObject.SetActive(false);
            Debug.Log(screen.name);
        }
    }
}
