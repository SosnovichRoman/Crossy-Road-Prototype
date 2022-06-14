using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.IO;

public class Wallet : MonoBehaviour
{
    private int amount;

    public static UnityEvent Changed = new UnityEvent();

    private void Start()
    {
        Load();
    }

    public void AddCoin()
    {
        amount++;
        Changed.Invoke();
        Save();
    }

    [System.Serializable]
    class SaveData
    {
        public int amount;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.amount = amount;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/wallet.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/wallet.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            amount = data.amount;
        }
    }

    public int GetAmount() { return amount; }
}
