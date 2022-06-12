using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private AudioClip deathSound;

    [SerializeField]
    private GameObject onDestroyParticle;

    [SerializeField]
    private float score;


    private void Start()
    {
        if (SaveManager.Instance != null)
        {
            Instantiate(SaveManager.Instance.playerPrefab, gameObject.transform);
        }
    }
    public void DestroyPlayer()
    {
        EventManager.SendPlayerDead();
        Instantiate(onDestroyParticle, transform.position, onDestroyParticle.transform.rotation);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
    }

}
