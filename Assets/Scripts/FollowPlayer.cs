using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float forwardSpeed;
    [SerializeField]
    private float smoothSpeed;
    [SerializeField]
    private float smoothForwardDistance;
    [SerializeField]
    private float smoothSideDistance;
    [SerializeField]
    private float sideLinearSpeed;

    private float cameraStartZoom;
    [SerializeField]
    private float cameraEndZoom;
    [SerializeField]
    private float cameraZoomDuration;
    [SerializeField]
    private float zoomForwardSpeed;

    private void Awake()
    {
        EventManager.GameOver.AddListener(OnPlayerDeath);
        EventManager.GameStarted.AddListener(() => StartCoroutine(Follow()));
    }

    private IEnumerator Follow()
    {
        while (true)
        {
            //Follow if player is Alive
            if (target != null)
            {
                //Control left & right movement
                SmoothMoveSide_Controller();
                //Simple move forward when player too far of camera
                SmoothFollow_Controller();
                //Simple move forward
                transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
            }
            yield return null;
        }
    }

    private void OnPlayerDeath()
    {
        Debug.Log("started");
        StartCoroutine(ZoomOnTarget());
    }

    private IEnumerator ZoomOnTarget()
    {
        //Move to player death position
        Vector3 _target = target.position;
        while (transform.position != _target)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, zoomForwardSpeed * Time.deltaTime);
            yield return null;
        }

        //Zoom camera
        Camera _camera = gameObject.GetComponentInChildren<Camera>();
        cameraStartZoom = _camera.fieldOfView;

        float timeElapsed = 0;
        while (timeElapsed < cameraZoomDuration)
        {
            _camera.fieldOfView = Mathf.Lerp(cameraStartZoom, cameraEndZoom, timeElapsed / cameraZoomDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _camera.fieldOfView = cameraEndZoom;
    }


    private void SmoothFollow_Controller()
    {
        if (target.transform.position.z > transform.position.z + smoothForwardDistance)
        {
            Vector3 _desiredPosition = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);
            Vector3 _smoothPosition = Vector3.Lerp(transform.position, _desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = _smoothPosition;
        }
    }

    private void SmoothMoveSide_Controller()
    {
        Vector3 _desiredPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

        if (Mathf.Abs(target.transform.position.x - transform.position.x) > smoothSideDistance)
        {
            Vector3 _smoothPosition = Vector3.Lerp(transform.position, _desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = _smoothPosition;
        }
        else if (Mathf.Abs(target.transform.position.x - transform.position.x) < smoothSideDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, _desiredPosition, sideLinearSpeed * Time.deltaTime);
        }
    }

}
