using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ScoreCounter : MonoBehaviour
{

    [SerializeField]
    private float stepDistance;
    private int _score;
    private int _scoreTemp;
    private int _bestScore;
    private Vector3 _startPosition;

    public static UnityEvent Changed = new UnityEvent();

    private void Start()
    {
        _startPosition = transform.position;
        _bestScore = SaveManager.Instance.bestScore;
    }

    private void Update()
    {
        _scoreTemp = (int)Mathf.Round((transform.position.z - _startPosition.z) / stepDistance);  //Refactor: try to avoid Update
        if(_scoreTemp > _score)
        {
            _score = _scoreTemp;
            Changed.Invoke();
            if (_score > _bestScore)
            {
                SaveManager.Instance.bestScore = _score;
                SaveManager.Instance.Save();
            }
        }
    }

    public int GetScore() { return _score; }
}
