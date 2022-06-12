using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayerOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            other.gameObject.GetComponent<Player>().DestroyPlayer();
        }
    }
}
