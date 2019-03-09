using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WorldController : MonoBehaviour {

    public int num;
    Vector3 warpPos;
    PlayerController player;
    Vector3 originalPos;

    public AudioClip[] audioClips;

    //Combination objects to instantiate in ObjectController
    public GameObject[] comboObjectsStandby;


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
                    Debug.Log("clicked");
                    player.selectedWorld = num;
                }

            }
        }

        //LOCK ROTATION---------------------
        if (player.selectedWorld == num)
        {
            //Snap to front of camera

            //transform.parent = player.transform;
            //transform.position = player.transform.position + player.transform.forward * 1.36f;
            transform.rotation = new Quaternion(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w);

        } else {
            //transform.parent = null;
            //transform.position = originalPos;
        }

    }

    //EDITOR TESTING
    private void OnMouseDown()
    {
        //Debug.Log("clicked");
        //player.selectedWorld = num;
    }
}
