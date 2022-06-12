using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLineOnCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Line>() != null)
        {
            other.gameObject.GetComponent<Line>().Destroy();
        }
    }
}
