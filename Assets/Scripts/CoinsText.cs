using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsText : MonoBehaviour
{
    // Start is called before the first frame update

    private int _coins = 0;
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
        Wallet.Changed.AddListener(UpdateText);
    }

    private void UpdateText()
    {
        _coins++;
        _text.text = "Coins: " + _coins;
    }

}
