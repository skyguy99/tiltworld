using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WorldController : MonoBehaviour {

    public int num;
    Vector3 warpPos;
    PlayerController player;
    Vector3 originalPos;


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

            transform.parent = player.transform;
            transform.position = new Vector3(0.03f, 1f, 1.55f);

        } else {
            transform.parent = null;
            transform.position = originalPos;
        }

    }

    //EDITOR TESTING
    private void OnMouseDown()
    {
        Debug.Log("clicked");
        player.selectedWorld = num;
    }
}
