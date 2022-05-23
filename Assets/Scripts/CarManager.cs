using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] carPrefab;
    [SerializeField]
    private Vector2 initialSpawnOffsetRange;
    [SerializeField]
    private int initialCount;
    [SerializeField]
    private float spawnOffsetX;
    [SerializeField]
    private float minDelay;
    [SerializeField]
    private float maxDelay;


    private float _isRight;
    private int _prefabIndex;
    // Start is called before the first frame update
    void Start()
    {
        //Determinates in which side...
        _isRight = Random.value;
        //... and which cars will be spawned
        _prefabIndex = Random.Range(0, carPrefab.Length);
        InitialSpawn();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            Vector3 _spawnOffset = new Vector3(spawnOffsetX, 0, 0);

            if (_isRight > 0.5)
            {
                Instantiate(carPrefab[_prefabIndex], transform.position + _spawnOffset, Quaternion.Euler(0, -90, 0));
            }
            else
            {
                Instantiate(carPrefab[_prefabIndex], transform.position - _spawnOffset, Quaternion.Euler(0, 90, 0));
            }
        }
    }

    void InitialSpawn()
    {
        float _spawnOffsetX = 0;
        Vector3 _spawnOffset = Vector3.zero;
        for (int i = 0; i < initialCount; i++)
        {
            _spawnOffsetX = _spawnOffsetX + Random.Range(initialSpawnOffsetRange.x, initialSpawnOffsetRange.y);
            _spawnOffset = new Vector3(_spawnOffsetX, 0, 0); 
            if (_isRight > 0.5)
            {
                Instantiate(carPrefab[_prefabIndex], transform.position + _spawnOffset, Quaternion.Euler(0, -90, 0));
            }
            else
            {
                Instantiate(carPrefab[_prefabIndex], transform.position - _spawnOffset, Quaternion.Euler(0, 90, 0));
            }
        }
    }
}
