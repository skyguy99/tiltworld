using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WorldController : MonoBehaviour {

    public int num;
    Vector3 warpPos;
    PlayerController player;

    public GameObject test;

    // Optional, allows user to drag left/right to rotate the world.
    private const float DRAG_RATE = .2f;
    float dragYawDegrees;



    // Use this for initialization
    void Start () {
        player = GameObject.FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                //OR with Tag

                if (raycastHit.collider.CompareTag("WorldBox"))
                {
                    Debug.Log("clicked");
                    player.selectedWorld = num;
                }

            }
        } 

        //LOCK ROTATION---------------------
        if(player.selectedWorld == num)
        {
            //player.transform.position = gameObject.transform.forward;
            //test.transform.position = transform.position + transform.forward;
            //test.transform.LookAt(transform.position);

            if (XRSettings.enabled)
            {
                // Unity takes care of updating camera transform in VR.
                return;
            }

            // android-developers.blogspot.com/2010/09/one-screen-turn-deserves-another.html
            // developer.android.com/guide/topics/sensors/sensors_overview.html#sensors-coords
            //
            //     y                                       x
            //     |  Gyro upright phone                   |  Gyro landscape left phone
            //     |                                       |
            //     |______ x                      y  ______|
            //     /                                       \
            //    /                                         \
            //   z                                           z
            //
            //
            //     y
            //     |  z   Unity
            //     | /
            //     |/_____ x
            //

            // Update `dragYawDegrees` based on user touch.
            //CheckDrag();

            transform.localRotation =
              // Allow user to drag left/right to adjust direction they're facing.
              Quaternion.Euler(0f, -dragYawDegrees, 0f) *

              // Neutral position is phone held upright, not flat on a table.
              Quaternion.Euler(90f, 0f, 0f) *

              // Sensor reading, assuming default `Input.compensateSensors == true`.
              Input.gyro.attitude *

              // So image is not upside down.
              Quaternion.Euler(0f, 0f, 180f);
        }

    }

    //EDITOR TESTING
    private void OnMouseDown()
    {
        Debug.Log("clicked");
        player.selectedWorld = num;
    }
}
