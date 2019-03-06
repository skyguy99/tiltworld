using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float speed;

    public bool isRunning;
    Rigidbody rb;
    Animator anim;
    PlayerController player;

    public bool swordEnable;
    public bool accessoryEnable;
    Transform sword;
    Transform accessory;

    void Start()
    {
      
        Input.multiTouchEnabled = false;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<PlayerController>();

        foreach(Transform t in transform)
        {
            if(t.name == "Tiltworld-6-5")
            {
                accessory = t;
            } else if (t.name == "Tiltworld-6-5_001")
            {
                sword = t;
            }
        }
    }

    private void Update()
    {
        //Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        //if (moveVector != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(moveVector);
        //    transform.Translate(moveVector * speed * Time.deltaTime, Space.World);
        //}

        sword.gameObject.SetActive(swordEnable);
        accessory.gameObject.SetActive(accessoryEnable);
        isRunning = (rb.velocity.x > 0.1f || rb.velocity.z > 0.1f);


        }

    void updateAnimations()
    {
        anim.SetBool("running", isRunning);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject.FindObjectOfType<iOSHapticFeedback>().Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
    }
   
}
