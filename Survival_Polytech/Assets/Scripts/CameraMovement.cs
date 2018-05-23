using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float speed = 0.2f;
    public Transform target;
    public float smoothing = 3f;

    [Space]    
    [Range(10, 70)]
    public int startingZoom = 45;
    [Range(10, 69)]
    public int minZoom = 15;
    [Range(11, 70)]
    public int maxZoom = 65;
    public float zoomSpeed = 25;

    RaycastHit hit;
    Vector3 offset;
    Camera mainCam;
    float speedMult;

    void Start()
    {
        mainCam = GetComponent<Camera>();
        offset = transform.position - target.position;

        if (minZoom > maxZoom)
        {
            minZoom += maxZoom;
            maxZoom = minZoom - maxZoom;
            minZoom -= maxZoom;
        }
        if (!(startingZoom >= minZoom && startingZoom <= maxZoom))
            startingZoom = (maxZoom + minZoom) / 2;
        mainCam.fieldOfView = startingZoom;

        zoomSpeed *= 20;
        speedMult = speed / 100;
    }


    void Update()
    {
        Zooming(Input.GetAxisRaw("Mouse ScrollWheel"));

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            WeightPosition();
        }
        else
        {
            Vector3 targetCamPos = target.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }

    void Zooming(float scrolling)
    {
        if (scrolling != 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            mainCam.fieldOfView -= scroll * zoomSpeed * Time.deltaTime;
            mainCam.fieldOfView = Mathf.Clamp(mainCam.fieldOfView, minZoom, maxZoom);
            speed -= scrolling * speedMult;
        }
    }
    
    void WeightPosition()
    {
        if (Input.mousePosition.x < 20)
        {
            transform.position -= new Vector3(speed, 0, 0);
            if (transform.position.x < target.position.x - 10.0)
            {
                transform.position += new Vector3(speed, 0, 0);
            }
        }
        if (Input.mousePosition.x > Screen.width - 20)
        {
            transform.position += new Vector3(speed, 0, 0);
            if (transform.position.x > target.position.x + 10.0)
            {
                transform.position -= new Vector3(speed, 0, 0);
            }
        }
        if (Input.mousePosition.y < 20)
        {
            transform.position -= new Vector3(0, 0, speed);
            if (transform.position.z < target.position.z - 10.0)
            {
                transform.position += new Vector3(0, 0, speed);
            }
        }
        if (Input.mousePosition.y > Screen.height - 20)
        {
            transform.position += new Vector3(0, 0, speed);
            if (transform.position.z > target.position.z + 10.0)
            {
                transform.position -= new Vector3(0, 0, speed);
            }
        }
    }
}
