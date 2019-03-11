using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
     
    iOSHapticFeedback iosHaptic;
    public int selectedWorld = -1;
    public bool inWorld;
    public Transform worldAnchor;

    public CharController character;
    

    //Combination objects to instantiate in ObjectController

    [System.Serializable]
    public struct ObjectsToCreate
    {
        public string name;
        public Transform obj;
    }
    public ObjectsToCreate[] objectsStandbyArray;
    public Dictionary<string, Transform> objectsStandby;

    // Use this for initialization
    void Start()
    {
        character = GameObject.FindObjectOfType<CharController>();
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();

        for (int i = 0; i < objectsStandbyArray.Length; i++)
        {
            objectsStandby.Add(objectsStandbyArray[i].name, objectsStandbyArray[i].obj);

        }

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
