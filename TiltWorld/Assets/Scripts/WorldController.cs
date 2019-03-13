using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Linq;

public class WorldController : MonoBehaviour {

    public int num;
    Vector3 warpPos;
    PlayerController player;
    public Vector3 originalPos;

    public AudioClip[] audioClips;
    public List<ObjectController> myObjects;



    // Use this for initialization
    void Start () {
        originalPos = transform.position;
        player = GameObject.FindObjectOfType<PlayerController>();
        if(transform.childCount > 0)
        {
            foreach (Transform t in transform.GetChild(0))
            {
                if (t.GetComponent<ObjectController>() != null)
                {
                    myObjects.Add(t.GetComponent<ObjectController>());
                }
            }

            for (int i = 2; i < myObjects.Count + 1; i++)
            {
                if (i % 2 == 0)
                {
                    myObjects[i - 2].isPriority = true; //assigns to half
                }
            }
        } 
	}
	
	// Update is called once per frame
	void Update () {

        //LOCK ROTATION---------------------
        //if (player.selectedWorld == num)
        //{
        //    //Snap to front of camera

        //    //transform.parent = player.transform;
        //    //transform.position = player.transform.position + player.transform.forward * 1.36f;
        //    transform.rotation = new Quaternion(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w);

        //} 

    }

    void ResetRotation()
    {
        iTween.RotateTo(gameObject, iTween.Hash("rotation", Quaternion.identity, iTween.EaseType.easeOutBounce, "time", 0.5f));
    }

    public void ResetWorld()
    {
        print("RESETTING WORLD " + num);
        iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", 0.6f, "easetype", "easeOutBounce", "oncomplete", "ResetRotation", "oncompletetarget", gameObject));


        foreach(ObjectController o in myObjects)
        {
            //o.Invoke("ResetObject", 0.3f);
        }
    }
}
