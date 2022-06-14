using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsText : MonoBehaviour
{
    [SerializeField]
    private Wallet wallet;

    private int _coins = 0;
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
        Wallet.Changed.AddListener(UpdateText);
    }

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _coins = wallet.GetAmount();
        _text.text = "Coins: " + _coins;
    }

}
