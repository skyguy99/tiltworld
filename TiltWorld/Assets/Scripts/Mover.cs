using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class Mover : MonoBehaviour
{

    public float speed = 3.3f;
    public FloatingJoystick joystick;
    public bool isMoving;
    public Vector3 moveVector;
    PlayerController player;

    //time to start = 7 seconds

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        moveVector = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical);


        isMoving = (moveVector != Vector3.zero && !LeanSelectable.SomethingIsSelected);
        if (moveVector != Vector3.zero && !LeanSelectable.SomethingIsSelected && Time.time > 6f && PlayerPrefs.HasKey("playedOnce") && (player.room.bounds.Contains(player.transform.position + moveVector)))
            {
                //transform.rotation = Quaternion.LookRotation(moveVector);
                transform.Translate(moveVector * speed * Time.deltaTime, Space.World);

            }
    }
}
