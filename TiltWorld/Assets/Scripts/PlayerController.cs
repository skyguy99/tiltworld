﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    iOSHapticFeedback iosHaptic;
    public int selectedWorld = -1;
    public bool inWorld;
    public Transform worldAnchor;

    public GameObject character;
    private Vector3 offset;

    float distance;
    Vector3 playerPrevPos, playerMoveDir;

    // Use this for initialization
    void Start()
    {
        character = GameObject.FindObjectOfType<CharController>().gameObject;
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();
        offset = transform.position - character.transform.position;

        distance = offset.magnitude;
        playerPrevPos = character.transform.position;



    }

    void Update()
    {
        inWorld = (selectedWorld > -1);
        //playerMoveDir = character.transform.position - playerPrevPos;
        //playerMoveDir.Normalize();
        //transform.position = character.transform.position - playerMoveDir * distance;

        //TEST


        //Check direction
        //Utils.CheckIsFacing(transform, worldAnchor);
      
       
    }

    private void OnTriggerEnter(Collider other)
    {
        iosHaptic.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);

    }
    private void OnTriggerExit(Collider other)
    {

       
    }
}
