using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] skinPrefabs;
    [SerializeField]
    private GameObject[] playerPrefabs;
    [SerializeField]
    private GameObject skinContainer;
    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float moveDuration;
    [SerializeField]
    private Vector3 currentContainerPosition;

    private Vector3 startContainerPosition;

    private GameObject chosenPrefab;
    private int chosenPrefabIndex = 0;

    private List<GameObject> createdObjects = new List<GameObject>();

    private void Start()
    {
        startContainerPosition = skinContainer.transform.position;
        Initialize(startContainerPosition);
        chosenPrefab = createdObjects[chosenPrefabIndex]; //First
        chosenPrefab.GetComponent<SkinController>().Scale(true);
    }

    private void Initialize(Vector3 _startPosition)
    {
        Vector3 _position = _startPosition;
        foreach (var skin in skinPrefabs)
        {
            GameObject created = Instantiate(skin, _position, skin.transform.rotation);
            created.transform.SetParent(skinContainer.transform);
            created.layer = 3;
            createdObjects.Add(created);
            _position = _position + offset;
        }
    }

    public void Select()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.playerPrefab = playerPrefabs[chosenPrefabIndex];
            EventManager.SendGameRestart();
        }
    }

    public void Next()
    {
        ÑhangeSelection(true);
    }

    public void Back()
    {
        ÑhangeSelection(false);
    }

    private void ÑhangeSelection(bool increase)
    {
        if (increase) chosenPrefabIndex++;
        else chosenPrefabIndex--;

        if(chosenPrefabIndex < 0) chosenPrefabIndex = 0;
        if(chosenPrefabIndex > skinPrefabs.Length - 1) chosenPrefabIndex = skinPrefabs.Length - 1;

        chosenPrefab.GetComponent<SkinController>().Scale(false);
        chosenPrefab = createdObjects[chosenPrefabIndex];
        chosenPrefab.GetComponent<SkinController>().Scale(true);

        currentContainerPosition = startContainerPosition - chosenPrefabIndex * offset;
        StopAllCoroutines();

        StartCoroutine(MoveContainer(skinContainer, increase));
    }

    private IEnumerator MoveContainer(GameObject _object, bool inLeftDirection)
    {
        float timeElapsed = 0;
        while (_object.transform.position != currentContainerPosition)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > moveDuration) timeElapsed = moveDuration;
            _object.transform.position = Vector3.Lerp(_object.transform.position, currentContainerPosition, timeElapsed / moveDuration);
            yield return null;
        }
    }
}
