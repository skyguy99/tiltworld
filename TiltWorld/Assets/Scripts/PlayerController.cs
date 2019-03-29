using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
     
    iOSHapticFeedback iosHaptic;
    public Transform selectedObject;

    public BoxCollider room;
    public CharController character;
    Vector3 originPos;

    public AudioClip[] audioClips;
    public AudioSource audioAccents;
    public AudioSource audioBackground; //so can change with settings later
    public Mover mover;

    public Transform explodeCubes;

    public bool sawCharacter;


    //Object management

    List<ObjectController> allObjects = new List<ObjectController>();
    public Dictionary<string, string[]> combineObjects = new Dictionary<string, string[]>();
    public ObjectController[] objectsToBeInstantiated;

    bool makePriority;

    public static void ShuffleArray<T>(T[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            T tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }


    // Use this for initialization
    void Start()
    {
        // = GameObject.FindGameObjectWithTag("Room").GetComponent<BoxCollider>();
       
        character = GameObject.FindObjectOfType<CharController>();
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();
        originPos = transform.position;
        mover = GetComponent<Mover>();
        audioBackground = GetComponent<AudioSource>();


        int index = 0;

        ObjectController[] theObjs = GameObject.FindObjectsOfType<ObjectController>();
        ShuffleArray(theObjs);

        for (int i = 0; i < theObjs.Length; i++)
        {
            allObjects.Add(theObjs[i]);
            theObjs[i].isPriority = makePriority;

            if (i+1 < theObjs.Length)
            {
                theObjs[i].partnerName = theObjs[i + 1].objName;
            } else {
                theObjs[i].partnerName = theObjs[0].objName;
            }

            theObjs[i].ObjectToSpawn = makePriority ? objectsToBeInstantiated[index].transform : null;

            if (index >= objectsToBeInstantiated.Length)
            {
                index = 0;
            }
            makePriority = !makePriority;
        }
  
    }

    public void ResetPosition()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", originPos, "time", 0.45f, "easetype", iTween.EaseType.easeOutExpo));
    }

    void Update()
    {

        if (!room.bounds.Contains(transform.position))
        {
            //ResetPosition();
        }
           
    }

}
