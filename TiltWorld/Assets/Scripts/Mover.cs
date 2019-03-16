using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class Mover : MonoBehaviour
{

    public float speed = 3.3f;
    public FloatingJoystick joystick;
    PlayerController player;
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        //transform.rotation = new Quaternion(transform.rotation.x, player.transform.rotation.y, transform.rotation.z, transform.rotation.w);
        //Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);
        Vector3 moveVector = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical);

        isMoving = (moveVector != Vector3.zero && !LeanSelectable.SomethingIsSelected);
        if (moveVector != Vector3.zero && !LeanSelectable.SomethingIsSelected)
            {
                //transform.rotation = Quaternion.LookRotation(moveVector);
                transform.Translate(moveVector * speed * Time.deltaTime, Space.World);

            }
    }
}
