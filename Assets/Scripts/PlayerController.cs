using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float stepDistance = 2f;
    [SerializeField]
    private AudioSource jumpSound;
    [SerializeField]
    private AudioClip deathSound;

    [SerializeField]
    private GameObject onDestroyParticle;

    private Animator animator;

    private bool isMoving = false;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //refactor TODO
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(MoveInDirection(Vector3.forward));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(MoveInDirection(Vector3.right));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(MoveInDirection(-Vector3.forward));
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(MoveInDirection(-Vector3.right));
        }




    }


    //Smoothly move player in Direction if it's posible
    IEnumerator MoveInDirection(Vector3 direction)
    {
        //Wait for end of previous move
        while (isMoving)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (CanMoveInDirection(direction))
        {
            isMoving = true;

            //Play sound
            jumpSound.pitch = Random.Range(0.8f, 1.2f);
            jumpSound?.Play();

            //Rotate to target direction
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            //Apply animation
            animator.SetFloat("Speed_f", 1f);

            float startTime = Time.time;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = transform.position + direction.normalized * stepDistance;
            float distCovered = 0f;

            //Move object by manipulating it's position
            while (distCovered < stepDistance)
            {
                distCovered = (Time.time - startTime) * speed;
                float fractionOfJourney = distCovered / stepDistance;
                //transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                if (fractionOfJourney > 1f) fractionOfJourney = 1f;
                transform.position = MathParabola.Parabola(startPosition, endPosition, 2f, fractionOfJourney);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            isMoving = false;
            animator.SetFloat("Speed_f", 0f);
        }

    }

    //Return true if there no obstacle in the way
    bool CanMoveInDirection(Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), direction);
        Physics.Raycast(ray, out hit, 2); // 2 - Lenght of ray (one step)

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Obstacle");
                return false;
            }
            else { return true; }
        }
        else { return true; }

    }

    public void DestroyPlayer()
    {
        EventManager.SendPlayerDead();
        Instantiate(onDestroyParticle, transform.position, onDestroyParticle.transform.rotation);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
    }

}
