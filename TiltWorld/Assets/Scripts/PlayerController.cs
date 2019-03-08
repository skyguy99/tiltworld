using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    iOSHapticFeedback iosHaptic;
    public int selectedWorld;
    public bool inWorld;

    public GameObject character;
    private Vector3 offset;

    float distance;
    Vector3 playerPrevPos, playerMoveDir;

    // Use this for initialization
    void Start()
    {
        character = GameObject.FindObjectOfType<CharController>().gameObject;
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();
        offset = transform.position - character.transform.position;

        distance = offset.magnitude;
        playerPrevPos = character.transform.position;
    }

    void Update()
    {
        inWorld = (selectedWorld > 0);
        //playerMoveDir = character.transform.position - playerPrevPos;
        //playerMoveDir.Normalize();
        //transform.position = character.transform.position - playerMoveDir * distance;

        //transform.LookAt(character.transform.position);

        //playerPrevPos = character.transform.position;
        selectedWorld = 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WorldBox") //and isnt attached
        {
            print("World enter!!");
            iosHaptic.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WorldBox")
        {
            print("World exit!!");
        }
    }
}
