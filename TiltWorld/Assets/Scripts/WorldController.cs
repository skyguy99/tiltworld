using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class WorldController : MonoBehaviour {

    public int num;
    PlayerController player;
    Animator animator;
    ScrollRectSnap scrollRectSnap;
 

    public float ShakeForceMultiplier;
    public Rigidbody2D[] ShakingRigidbodies;


    // Use this for initialization
    void Awake () {
        animator = GetComponentInChildren<Animator>();
        scrollRectSnap = GameObject.FindObjectOfType<ScrollRectSnap>();
        player = GameObject.FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        //if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        //{
        //    Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    RaycastHit raycastHit;
        //    if (Physics.Raycast(raycast, out raycastHit))
        //    {
        //        //OR with Tag

        //        if (raycastHit.collider.CompareTag("WorldBox"))
        //        {
        //            //Debug.Log("clicked");
        //            player.selectedWorld = num;
        //        }

        //    }
        //}


        //transform.rotation = new Quaternion(0.0f, player.transform.rotation.y, 0.0f, player.transform.rotation.w);
        //transform.LookAt(player.transform);
        //transform.rotation = player.transform.rotation;

        //LOCK ROTATION---------------------
        if (scrollRectSnap.currentSelectedPly == num)
        {
            //print("SELECTED! " + num);
            //animator.SetFloat("AnimStateTime", 0.5f, 1, 10 * Time.deltaTime);

            //2nd VALUE - percentage
            float percent = Mathf.Abs((Camera.main.WorldToScreenPoint(transform.position).y) / (Screen.height*0.84f));
            if(percent <= 0.9f && percent >= 0.2f)
            {
                animator.SetFloat("AnimStateTime", percent, 1, 10 * Time.deltaTime);
            }
            print(percent);

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
