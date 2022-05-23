using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField]
    private float xBound;

    private void Start()
    {
        StartCoroutine(_DestroyOutOfBounds());
    }

    IEnumerator _DestroyOutOfBounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (gameObject.transform.position.x > xBound || gameObject.transform.position.x < -xBound)
            {
                Destroy(gameObject);
            }
        }
    }
}
