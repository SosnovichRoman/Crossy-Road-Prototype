using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private AudioSource[] hornSounds;
    [SerializeField]
    private float hornSoundChance;
    [SerializeField]
    private Vector2 hornSoundDelay;

    private void Start()
    {
        StartCoroutine(CarHorn());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.DestroyPlayer();
        }
    }

    IEnumerator CarHorn()
    {
        float _delay = Random.Range(hornSoundDelay.x, hornSoundDelay.y);
        yield return new WaitForSeconds(_delay);
        if (Random.value < hornSoundChance)
        {
            int index = Random.Range(0, hornSounds.Length);
            hornSounds?[index].Play();
        }
    }

}
