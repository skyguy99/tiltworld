using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float speed;
    public FloatingJoystick joystick;
    public Animator anim;
    Rigidbody rb;
    Vector3 moveVector;
    PlayerController player;

    void Start()
    {
       joystick = GameObject.FindObjectOfType<FloatingJoystick>();
        Input.multiTouchEnabled = false;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * speed * Time.deltaTime, Space.World);

        }
        anim.SetBool("run", (moveVector != Vector3.zero));

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject.FindObjectOfType<iOSHapticFeedback>().Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);

        if(collision.gameObject.GetComponent<ObjectController>() != null)
        {
            if (collision.gameObject.GetComponent<ObjectController>().triggerAttack)
            {
                anim.SetTrigger("attack");
            }
        }
    }
   
}
