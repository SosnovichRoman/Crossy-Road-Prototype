using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Wallet : MonoBehaviour
{
    private int _amount;

    public static UnityEvent Changed = new UnityEvent();

    public void AddCoin()
    {
        _amount++;
        Changed?.Invoke();
    } 

    public int GetAmount() { return _amount; }
}
