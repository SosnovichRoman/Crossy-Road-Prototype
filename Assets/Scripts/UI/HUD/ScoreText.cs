using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private ScoreCounter scoreCounter;
    private int _score = 0;
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
        ScoreCounter.Changed.AddListener(UpdateText);
    }

    private void UpdateText()
    {
        _score = scoreCounter.GetScore();
        _text.text = "Score: " + _score;
    }

}
