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

    Vector3 originalPos;
    public float rayDistance = 1.2f;

    Transform sword;
    Transform accessory;
    PinchZoom pinchZoom;

    void Start()
    {
        originalPos = transform.position;
       joystick = GameObject.FindObjectOfType<FloatingJoystick>();
        pinchZoom = GameObject.FindObjectOfType<PinchZoom>();
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

        if (!pinchZoom.isPinching)
        {
            Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

            if (moveVector != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveVector);
                transform.Translate(moveVector * speed * Time.deltaTime, Space.World);

            }
            anim.SetBool("run", (moveVector != Vector3.zero));


            //raycasting

            Vector3 rotation = transform.forward;
            RaycastHit hit;
            Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
            if (Physics.Raycast(startPoint, rotation, out hit, 1f))
            {
                Debug.DrawRay(startPoint, rotation * hit.distance, Color.yellow);
                //Debug.Log("Did Hit"+hit.transform.name);
                if (hit.transform.GetComponent<Platform>() != null)
                {
                    Jump(hit.transform);
                }
                }
            else
            {
                Debug.DrawRay(startPoint, rotation * 1000, Color.white);
                //Debug.Log("Did not Hit");
            }
        }

        sword.gameObject.SetActive(anim.GetCurrentAnimatorStateInfo(0).IsName("attack"));
        accessory.gameObject.SetActive(anim.GetCurrentAnimatorStateInfo(0).IsName("attack"));

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WorldBox")
        {
            GameObject.FindObjectOfType<iOSHapticFeedback>().Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
            print("ENTERING WORLD: "+ other.GetComponent<WorldController>().num);
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

    //triggered by shake
    public void LeaveWorld(WorldController world)
    {
        print("Leaving world");
       
        //fall
        rb.isKinematic = true;
        iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));

        player.selectedWorld = -1;
    }

    void Jump(Transform other)
    {
        rb.isKinematic = true;
        iTween.MoveTo(gameObject, iTween.Hash("position", other.GetComponent<Platform>().lerpPos, "time", 0.55f, "easetype", iTween.EaseType.easeOutCubic, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
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
