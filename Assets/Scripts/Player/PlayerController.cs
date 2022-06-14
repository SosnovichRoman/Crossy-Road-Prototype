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

    private bool isMoving = false;
    private bool gameStarted = false;

    private void Awake()
    {
        EventManager.GameStarted.AddListener(() => gameStarted = true);
    }

    void Update()
    {
        SendGameStart();

        if (gameStarted)
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
    }

    //Refactor ToDo
    //Start game by clicking any playabe input
    private void SendGameStart() 
    {
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow))
            EventManager.SendGameStart();
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
            GetComponentInChildren<Animator>().SetFloat("Speed_f", 1f); //not clean

            float startTime = Time.time;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = transform.position + direction.normalized * stepDistance;
            float distCovered = 0f;

            //Move object by manipulating it's position
            while (distCovered < stepDistance)
            {
                distCovered = (Time.time - startTime) * speed;
                float fractionOfJourney = distCovered / stepDistance;
                if (fractionOfJourney > 1f) fractionOfJourney = 1f;
                transform.position = MathParabola.Parabola(startPosition, endPosition, stepDistance, fractionOfJourney);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            isMoving = false;
            GetComponentInChildren<Animator>().SetFloat("Speed_f", 0f); //not clean
        }

    }

    //Return true if there no obstacle in the way
    bool CanMoveInDirection(Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), direction);
        Physics.Raycast(ray, out hit, stepDistance); // stepDistance - Lenght of ray (one step)

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Obstacle"))
            {
                return false;
            }
            else { return true; }
        }
        else { return true; }
    }

}
