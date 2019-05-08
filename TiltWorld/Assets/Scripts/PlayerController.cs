using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
     
    iOSHapticFeedback iosHaptic;
    public Transform selectedObject;

    public BoxCollider room;
    public Color[] roomColors;
    public Color[] fogColors;
    Vector3 originPos;

    public AudioClip[] audioClips;
    public AudioSource audioAccents;
    public AudioSource audioBackground; //so can change with settings later
    public Mover mover;

    public Transform explodeCubes;
    public Material particleMat;
    public Color[] particleColors;

    public bool sawCharacter;


    //For serialization
    public ObjectController[] objects;
    public WorldController[] worlds;
    public Transform world1;
    public CharController character;

    public int serializeId;
    public int lastIdAssigned;

    //Object management

    List<ObjectController> allObjects = new List<ObjectController>();
    public Dictionary<string, string[]> combineObjects = new Dictionary<string, string[]>(); //key= produced object, values = [objName, partnername]
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
    void Awake()
    {


        objects = GameObject.FindObjectsOfType<ObjectController>();
        worlds = GameObject.FindObjectsOfType<WorldController>();
        character = GameObject.FindObjectOfType<CharController>();
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();
        originPos = transform.position;
        mover = GetComponent<Mover>();
        audioAccents = GetComponent<AudioSource>();


        int index = 0;

        ObjectController[] theObjs = GameObject.FindObjectsOfType<ObjectController>();
        ShuffleArray(theObjs);

        //Here we set up all objects -----------
        for (int i = 0; i < theObjs.Length; i++)
        {
            index++;
            if (index >= objectsToBeInstantiated.Length)
            {
                index = 0;
            }
            allObjects.Add(theObjs[i]);
            theObjs[i].isPriority = makePriority;

            if (i + 1 < theObjs.Length)
            {
                theObjs[i].partnerName = theObjs[i + 1].objName;
            }
            else
            {
                theObjs[i].partnerName = theObjs[0].objName;
            }

            theObjs[i].ObjectToSpawn = makePriority ? objectsToBeInstantiated[index].transform : null;

            //add to player dictionary!
            combineObjects[objectsToBeInstantiated[index].objName] = new string[] { theObjs[i].partnerName, theObjs[i].objName};
            makePriority = !makePriority;
        }

        //if(!PlayerPrefs.HasKey("GameData"))
        //{
            SetAllIds();
        //}
    }

    public void UpdateObjectsArrays()
    {
        objects = GameObject.FindObjectsOfType<ObjectController>();
    }

    void SetAllIds()
    {

        //set all world objects for serialize
        if(serializeId < 1)
        {
            serializeId = 1;
            character.serializeId = 2;
            lastIdAssigned = character.serializeId;
            foreach (WorldController w in worlds)
            {
                w.serializeId = lastIdAssigned++;
            }
            foreach (ObjectController o in objects)
            {
                o.serializeId = lastIdAssigned++;
            }
        }

    }

    public void AssignNewObjectId(ObjectController o)
    {
        o.serializeId = lastIdAssigned++;
    }

    public void ResetPosition()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", originPos, "time", 0.45f, "easetype", iTween.EaseType.easeOutExpo));
    }

    public float getSqrDistance(Vector3 v1, Vector3 v2)
    {
        return (v1 - v2).sqrMagnitude;
    }
    float mapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax)
    {
        return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
    }

    void Update()
    {


        Color c = particleMat.color;
        c = Color.Lerp(particleColors[0], particleColors[1], Mathf.PingPong(Time.time, 1));
        particleMat.color = c;
        particleMat.SetColor("_EmissionColor", c);


        float distanceApart = getSqrDistance(world1.position, transform.position);

        //Convert 0 and 200 distance range to 0f and 1f range
        float lerp = mapValue(distanceApart, 100f, 200f, 0f, 1f); //max = 10f

        //Lerp Color between near and far color
        Color lerpColor = Color.Lerp(roomColors[0], roomColors[1], lerp);
        room.GetComponent<Renderer>().material.color = lerpColor;

        Color fogColor = Color.Lerp(fogColors[0], fogColors[1], lerp);
        RenderSettings.fogColor = fogColor;

    }

}
