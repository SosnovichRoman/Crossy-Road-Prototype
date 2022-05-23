using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Line>() != null)
        {
            other.gameObject.GetComponent<Line>().Destroy();
        }
        else if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.gameObject.GetComponent<PlayerController>().DestroyPlayer();
        }
        else { Destroy(other.gameObject); }
    }
}
