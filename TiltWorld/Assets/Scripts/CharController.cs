using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float speed;
    public FloatingJoystick joystick;
    Animator anim;
    Rigidbody rb;

    void Start()
    {
       joystick = GameObject.FindObjectOfType<FloatingJoystick>();
        Input.multiTouchEnabled = false;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * speed * Time.deltaTime, Space.World);
        }
    }

    void UpdateAnim()
    {

        anim.SetBool("run", (rb.velocity.z > 0.1f || rb.velocity.x > 0.1f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject.FindObjectOfType<iOSHapticFeedback>().Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
    }
   
}
