using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public float speed = 1.8f;
    public FloatingJoystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

            if (moveVector != Vector3.zero)
            {
                //transform.rotation = Quaternion.LookRotation(moveVector);
                transform.Translate(moveVector * speed * Time.deltaTime, Space.World);

            }
    }
}
