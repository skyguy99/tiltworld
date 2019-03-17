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

    Vector3 currentPlayerPos;
    Vector3 lastPlayerPos;
    BoxCollider room;
    Renderer rend;
    bool canFollow;

    void Start()
    {
        //originalPos = transform.position;
        rend = GetComponentInChildren<Renderer>();
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

        currentPlayerPos = player.transform.position;
        InvokeRepeating("RepeatIntroAnim", 0f, 4.5f);

    }


    //INTRO animate
    void RepeatIntroAnim()
    {
 
        MoveToiTween(new Vector3(transform.position.x, 0.5f, transform.position.z), 2f);
        anim.SetTrigger("attack");
    }

    IEnumerator introRot()
    {
        yield return new WaitForSeconds(2.5f);
     
        rb.isKinematic = true;
        iTween.RotateBy(gameObject, iTween.Hash("y", .5, "easeType", "easeInOutBack", "loopType", iTween.LoopType.none, "delay", 0f));
        yield return new WaitForSeconds(2f);
        rb.isKinematic = false;
        canFollow = true;
    }

    IEnumerator CancelIntro()
    {
        yield return new WaitForSeconds(2f);
        CancelInvoke("RepeatIntroAnim");
        MoveToiTween(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 2.5f), 2f);
        StartCoroutine(introRot());
    }

    private void Update()
    {

        if (rend.isVisible && !player.sawCharacter)
        {
            print("Saw character!");
            player.sawCharacter = true;
            StartCoroutine(CancelIntro());
  
        }

        room = player.room;
        if (!room.bounds.Contains(transform.position))
        {
            rb.isKinematic = true;
            iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
        }


        //currentPlayerPos = player.transform.position;
        //if(currentPlayerPos != lastPlayerPos)
        //{
        //    print("pos changed!");
        //    UpdateMovement();
        //}
        //lastPlayerPos = currentPlayerPos;

        if(canFollow)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 2.5f);


            Vector3 rot = player.transform.forward;
            rot.y = 0f;

            transform.rotation = Quaternion.LookRotation(rot);
            //transform.rotation = new Quaternion(transform.rotation.x, player.transform.rotation.y, transform.rotation.z, transform.rotation.w);
            //transform.localEulerAngles = new Vector3(transform.rotation.x, player.transform.rotation.y, transform.rotation.z);
        }
        anim.SetBool("run", player.mover.isMoving);


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

    void UpdateMovement()
    {
        rb.isKinematic = true;
        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z + 2f), "time", 0.55f, "easetype", iTween.EaseType.easeOutCubic, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
        //transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z + 2f);
    }

    void MoveToiTween(Vector3 vector, float duration)
    {
        rb.isKinematic = true;
        iTween.MoveTo(gameObject, iTween.Hash("position", vector, "time", duration, "easetype", iTween.EaseType.easeInBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
    }

    void DisableKinematic()
    {
        rb.isKinematic = false;
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
