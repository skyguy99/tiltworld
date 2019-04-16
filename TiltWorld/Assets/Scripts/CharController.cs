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

    float impulse = 5.5f;
    float jumpTimer;

    int tapCount;
    float doubleTapTimer;

    public int serializeId;

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
        MoveToiTween(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 3f), 1f);
        yield return new WaitForSeconds(0f);
        canFollow = true;
    }

    IEnumerator CancelIntro()
    {
        yield return new WaitForSeconds(2f);
        CancelInvoke("RepeatIntroAnim");

        if(!canFollow)
        {
            StartCoroutine(introRot());
        }

    }

    private void Update()
    {
        jumpTimer += Time.deltaTime;
        if (rend.isVisible && !player.sawCharacter)
        {
            //print("Saw character!");
            player.sawCharacter = true;
            StartCoroutine(CancelIntro());
  
        }

        room = player.room;
        if (!room.bounds.Contains(transform.position))
        {
            rb.isKinematic = true;
            iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
        }


        if(canFollow)
        {
            CancelInvoke("RepeatIntroAnim");
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z - 3f);


            Vector3 rot = player.transform.forward;
            rot.y = 0f;

            if(!player.mover.isMoving)
            {
                transform.rotation = Quaternion.LookRotation(rot); //idle
            } else {


                transform.rotation = Quaternion.LookRotation(new Vector3(player.mover.moveVector.x, 0f, player.mover.moveVector.z));
            }

        }

        anim.SetBool("run", player.mover.moveVector != Vector3.zero);
        //anim.SetBool("run", player.mover.isMoving);
        if(player.mover.moveVector != Vector3.zero)
        {
            canFollow = true;
        }



        //DOUBLE TAP
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tapCount++;
        }
        if (tapCount > 0)
        {
            doubleTapTimer += Time.deltaTime;
        }
        if (tapCount >= 2)
        {
            //JUMP here
            JumpImpulse();

            doubleTapTimer = 0.0f;
            tapCount = 0;
        }
        if (doubleTapTimer > 0.5f)
        {
            doubleTapTimer = 0f;
            tapCount = 0;
        }

        //for testing
        if (Input.GetKey("space"))
        {
            JumpImpulse();
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
        iTween.MoveTo(gameObject, iTween.Hash("position", vector, "time", duration, "easetype", iTween.EaseType.easeOutQuad, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
    }

    void DisableKinematic()
    {
        rb.isKinematic = false;
    }

    //real jump
    void JumpImpulse()
    {
        if(jumpTimer > 1.5f)
        {
            rb.velocity += Vector3.up * impulse;
            player.audioAccents.PlayOneShot(player.audioClips[4]);
            jumpTimer = 0f;
        }
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
