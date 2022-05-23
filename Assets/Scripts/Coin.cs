using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickupSound;

    //Executes when wallet picks up coin
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Wallet>(out Wallet wallet))
        {
            wallet?.AddCoin();
            AudioSource.PlayClipAtPoint(pickupSound, gameObject.transform.position);
            Destroy(gameObject);
        }
    }

}
