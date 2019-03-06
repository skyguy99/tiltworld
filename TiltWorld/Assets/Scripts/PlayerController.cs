using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    iOSHapticFeedback iosHaptic;
    public int selectedWorld;
    public bool inWorld;

    public CharController character;

    public Transform testprefab;

    // Use this for initialization
    void Start()
    {
        character = GameObject.FindObjectOfType<CharController>();
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();

    }

    void Update()
    {
        inWorld = (selectedWorld > 0);
        //playerMoveDir = character.transform.position - playerPrevPos;
        //playerMoveDir.Normalize();
        //transform.position = character.transform.position - playerMoveDir * distance;

        //transform.LookAt(character.transform.position);

        //playerPrevPos = character.transform.position;

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WorldBox") //and isnt attached
        {
            print("World enter!!");
            iosHaptic.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
           
            selectedWorld = other.GetComponent<WorldController>().num;
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
