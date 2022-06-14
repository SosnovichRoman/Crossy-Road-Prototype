using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    [SerializeField]
    public GameObject playerPrefab;
    [SerializeField]
    private Wallet wallet;

    public int coinsAmount;
    public int bestScore;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScore;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.bestScore;
        }
    }
}
