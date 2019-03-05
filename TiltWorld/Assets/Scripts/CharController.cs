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

    void Start()
    {
      
        Input.multiTouchEnabled = false;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        //Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        //if (moveVector != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(moveVector);
        //    transform.Translate(moveVector * speed * Time.deltaTime, Space.World);
        //}

        isRunning = (rb.velocity.x > 0.1f || rb.velocity.z > 0.1f);


        }

    //NOT NEEDED (pivoted mechanics):

    //IEnumerator waitForKinematic()
    //{
    //    yield return new WaitForSeconds(1f);
    //    rb.isKinematic = false;

    //}

    //public void MoveToWorld(WorldController world)
    //{
    //    Vector3 pos = world.transform.position;
    //    print("MOVE CHARACTER");
    //    rb.isKinematic = true;
    //    iTween.MoveTo(gameObject, iTween.Hash("position", pos, "easetype", iTween.EaseType.easeOutSine, "time", 1f, "delay", 0f));
    //    //wait and then set iskinematic to false
    //    StartCoroutine(waitForKinematic());
    //    transform.parent = world.transform;

    //}

    //public void ExitWorld()
    //{
    //    rb.isKinematic = true;
    //    iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(), "easetype", iTween.EaseType.easeOutSine, "time", 1f, "delay", 0f));
    //    //wait and then set iskinematic to false
    //    StartCoroutine(waitForKinematic());
    //    transform.parent = null;
    //}

    void updateAnimations()
    {
        anim.SetBool("running", isRunning);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject.FindObjectOfType<iOSHapticFeedback>().Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
    }
   
}
