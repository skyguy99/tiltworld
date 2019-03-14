using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
     
    iOSHapticFeedback iosHaptic;
    public Transform selectedObject;

    public BoxCollider room;
    public CharController character;
    Vector3 originPos;
    public Transform followObject;

    // Use this for initialization
    void Start()
    {
        //room = GameObject.FindGameObjectWithTag("Room").GetComponent<BoxCollider>();
        character = GameObject.FindObjectOfType<CharController>();
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();
        originPos = transform.position;

    }

    public void ResetPosition()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", originPos, "time", 0.45f, "easetype", iTween.EaseType.easeOutExpo));
    }

    void Update()
    {
        if (!room.bounds.Contains(transform.position))
        {
            //ResetPosition();
        }
        //transform.position = new Vector3(followObject.position.x, followObject.position.y+3f, followObject.position.z-3f);
           
        }

}
