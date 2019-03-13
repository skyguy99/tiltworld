using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public Animator anim;
    Rigidbody rb;
    Vector3 moveVector;
    PlayerController player;

    Vector3 originalPos;
    public float rayDistance = 1.2f;

    Transform sword;
    Transform accessory;

    void Start()
    {
        originalPos = transform.position;
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

        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z + 2f);
        //transform.rotation = new Quaternion(transform.rotation.x, player.transform.rotation.y, transform.rotation.z, transform.rotation.w);
        anim.SetBool("run", (rb.velocity.magnitude > 0.1f));


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

        sword.gameObject.SetActive(anim.GetCurrentAnimatorStateInfo(0).IsName("attack"));
        accessory.gameObject.SetActive(anim.GetCurrentAnimatorStateInfo(0).IsName("attack"));

    }

   

    void DisableKinematic()
    {
        rb.isKinematic = false;
    }

    //triggered by shake
    public void LeaveWorld(WorldController world)
    {
        //print("Leaving world");
       
        ////fall
        //rb.isKinematic = true;
        //iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));

        //player.selectedWorld = -1;
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
