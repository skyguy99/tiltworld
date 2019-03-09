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

    public float heightOffset;
    public float rayDistance = 1.2f;

    Transform sword;
    Transform accessory;

    void Start()
    {
       joystick = GameObject.FindObjectOfType<FloatingJoystick>();
        Input.multiTouchEnabled = false;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<PlayerController>();

        foreach(Transform t in transform)
        {
            foreach (Transform j in t)
            {
                if (j.name == "sword")
                {
                    sword = j;
                }
                if (j.name == "goggle")
                {
                    accessory = j;
                }
            }
        }

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

        Vector3 rotation = transform.forward;

        RaycastHit hit;
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z), rotation.normalized*rayDistance, Color.green);
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z), rotation, out hit, rayDistance))
        {
            print(hit.transform);
            if (hit.transform.GetComponent<Platform>() != null)
            {
                Jump(hit.transform);
            }
        }

        sword.gameObject.SetActive(anim.GetCurrentAnimatorStateInfo(0).IsName("attack"));
        accessory.gameObject.SetActive(anim.GetCurrentAnimatorStateInfo(0).IsName("attack"));

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "WorldBox")
        {
            player.selectedWorld = other.GetComponent<WorldController>().num;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WorldBox")
        {
            player.selectedWorld = -1;
        }
    }

    void DisableKinematic()
    {
        rb.isKinematic = false;
    }

    void Jump(Transform other)
    {
        rb.isKinematic = true;
        iTween.MoveTo(gameObject, iTween.Hash("position", other.GetComponent<Platform>().lerpPos, "time", 0.4f, "easetype", "easeOutSine", "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
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
