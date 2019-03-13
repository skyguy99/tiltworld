﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
     
    iOSHapticFeedback iosHaptic;
    public Transform selectedObject;

    public float speed = 1f;
    public FloatingJoystick joystick;

    public CharController character;
    Vector3 originPos;

    // Use this for initialization
    void Start()
    {
        character = GameObject.FindObjectOfType<CharController>();
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();


    }

    public void ResetPosition()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", originPos, "time", 0.45f, "easetype", iTween.EaseType.easeOutBounce));
    }

    void Update()
    {
        //Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        //if (moveVector != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(moveVector);
        //    transform.Translate(moveVector * speed * Time.deltaTime, Space.World);

        //}
        //character.anim.SetBool("run", (moveVector != Vector3.zero));
    }

}
