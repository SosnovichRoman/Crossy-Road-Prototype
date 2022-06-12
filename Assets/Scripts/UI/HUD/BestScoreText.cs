using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreText : MonoBehaviour
{
    private int _bestScore = 0;
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Start()
    {
        SaveManager.Instance.LoadBestScore();
        _bestScore = SaveManager.Instance.bestScore;
        _text.text = "Best Score: " + _bestScore;
        Debug.Log("st");
    }

}
