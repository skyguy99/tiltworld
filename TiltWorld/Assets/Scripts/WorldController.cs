using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Linq;

public class WorldController : MonoBehaviour {

    public int num;
    PlayerController player;
    public Vector3 originalPos;

    BoxCollider room;


    // Use this for initialization
    void Start () {

        originalPos = transform.position;
        player = GameObject.FindObjectOfType<PlayerController>();

	}
	
	// Update is called once per frame
	void Update () {

        //LOCK ROTATION---------------------
      
        room = player.room;
        if (!room.bounds.Contains(transform.position))
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
        }

        //if(Input.GetKey("space"))
        //{
        //    HardReset();

        //}

    }

    public void HardReset()
    {
        transform.position = originalPos;
        transform.rotation = Quaternion.identity;
    }

    public void ResetWorld()
    {
        print("RESETTING WORLD " + num);
        iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", 0.6f, "easetype", "easeOutBounce", "oncomplete", "ResetRotation", "oncompletetarget", gameObject));


    }
}
