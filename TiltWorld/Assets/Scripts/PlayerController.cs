using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
     
    iOSHapticFeedback iosHaptic;
    public Transform selectedObject;

    public float speed = 1f;
    public FloatingJoystick joystick;
    public BoxCollider room;
    public CharController character;
    Vector3 originPos;

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
            ResetPosition();
        }

            Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

            if (moveVector != Vector3.zero)
            {
                //transform.rotation = Quaternion.LookRotation(moveVector);
                transform.Translate(moveVector * speed * Time.deltaTime, Space.World);

            }
           
        }

}
