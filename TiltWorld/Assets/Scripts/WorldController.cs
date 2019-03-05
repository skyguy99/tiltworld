using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WorldController : MonoBehaviour {

    public int num;
    Vector3 warpPos;
    PlayerController player;
    Vector3 originalPos;

    public float ShakeForceMultiplier;
    public Rigidbody2D[] ShakingRigidbodies;

    // Use this for initialization
    void Awake () {
        originalPos = transform.position;
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
                    //Debug.Log("clicked");
                    player.selectedWorld = num;
                }

            }
        }

        //LOCK ROTATION---------------------
        if (player.selectedWorld == num)
        {
            //Snap to front of camera

            transform.parent = player.transform;
            transform.position = player.transform.position + player.transform.forward * 1.36f;
            //transform.rotation = new Quaternion(0.0f, player.transform.rotation.y, 0.0f, player.transform.rotation.w);
            //transform.LookAt(player.transform);
            //transform.rotation = player.transform.rotation;

        } else {
            transform.parent = null;
            transform.position = originalPos;
        }

    }

    public void ShakeandReset(Vector3 deviceAcceleration)
    {
        //foreach (Rigidbody rb in ShakingRigidbodies)
        //{
        //    rb.AddForce(deviceAcceleration * ShakeForceMultiplier, ForceMode2D.Impulse);
        //}
    }

}
