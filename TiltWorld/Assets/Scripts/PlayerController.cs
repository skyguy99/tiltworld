using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    iOSHapticFeedback iosHaptic;
    public int selectedWorld = -1;
    public bool inWorld;
    public Transform worldAnchor;

    public CharController character;

    // Use this for initialization
    void Start()
    {
        character = GameObject.FindObjectOfType<CharController>();
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();

    }

    void Update()
    {
        inWorld = (selectedWorld > -1);

       
    }

    private void OnTriggerEnter(Collider other)
    {
        iosHaptic.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);

    }
    private void OnTriggerExit(Collider other)
    {

       
    }
}
