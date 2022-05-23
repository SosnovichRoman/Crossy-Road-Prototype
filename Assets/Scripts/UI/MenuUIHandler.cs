using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{

    public void StartGame()
    {
        EventManager.SendGameStart();
        //Disable
        StartCoroutine(Disable());

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator Disable()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("b_moveOut", true);
            //Delay
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }
}
