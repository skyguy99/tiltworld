using UnityEngine;
using System.Collections;

public class Inertia : MonoBehaviour
{
    public Vector3 speed;
    public Vector3 avgSpeed;
    private bool dragging = false;
    private Vector3 targetSpeedX;
    public float rotationSpeed;
    public float lerpSpeed;
    Touch theTouch;

    public Transform refObject;

    void Start()
    {
        refObject = GameObject.FindObjectOfType<PlayerController>().transform;

        dragging = false;
    }

    void Update()
    {

        #region Thisis  for mobile and touch screen devices (touch input)
        //if (Input.GetMouseButtonDown(0))
        //{
        //    dragging = true;
        //}
        //if (Input.touchCount == 1 && dragging)
        //{
        //    theTouch = Input.GetTouch(0);
        //    speed = new Vector3(theTouch.deltaPosition.x, theTouch.deltaPosition.y, 0);
        //    avgSpeed = Vector3.Lerp(avgSpeed, speed, Time.deltaTime * 5);
        //}
        //else
        //{
        //    if (Input.touchCount == 0 && dragging)
        //    {
        //        speed = avgSpeed;
        //        dragging = false;
        //    }
        //    var i = Time.deltaTime * lerpSpeed;
        //    speed = Vector3.Lerp(speed, Vector3.zero, i);
        //}
        //refObject.Rotate(Vector3.up * speed.x * rotationSpeed, Space.World);
        #endregion


        #region This is for PC (mouse input)

        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
        }

        if (Input.GetMouseButton(0) && dragging)
        {
            speed = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            avgSpeed = Vector3.Lerp(avgSpeed, speed, Time.deltaTime * 5);
        }
        else
        {
            if (dragging)
            {
                speed = avgSpeed;
                dragging = false;
            }
            var i = Time.deltaTime * lerpSpeed;
            speed = Vector3.Lerp(speed, Vector3.zero, i);
        }
        refObject.Rotate(Vector3.up * speed.x * rotationSpeed, Space.World);
        #endregion
    }
}