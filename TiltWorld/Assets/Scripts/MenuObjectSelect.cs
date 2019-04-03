using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ScrollSnaps;
using UnityEngine.UI;

public class MenuObjectSelect : MonoBehaviour
{

    public Transform pointer;
    public Transform contentBox;
    MenuObject[] menuObjects;

    public Material blackMat;
    public Transform platform;
    bool scaledIn;
    UIManager uIManager;
    PlayerController player;

    ObjectController[] allObjects;
  
    /* Constants */
    const float S = 1.5f; // The maximum size you want to get when closest
    const float D = 200.0f; // The distance where you start to scale
    const float E = 2.5f; // The distance where the object will not scale more (i.e. reached the maximum)

    float GetIconSize(Vector2 pointer, Vector2 icon)
    {
        // Get the value between 0 and 1 from the distance between
        float factor = Mathf.InverseLerp(D, E, Vector2.Distance(pointer, icon));

        // Return the interpolated value size depending on the distance
        return Mathf.Lerp(0.6f, S, factor);
    }
    OmniDirectionalScrollSnap omni;

    // Start is called before the first frame update
    void Start()
    {
        omni = GetComponent<OmniDirectionalScrollSnap>();
        pointer = GetComponent<Image>().transform;
        uIManager = GameObject.FindObjectOfType<UIManager>();
        menuObjects = GameObject.FindObjectsOfType<MenuObject>();
        player = GameObject.FindObjectOfType<PlayerController>();
        UpdateObjects();


    }

    private void Awake()
    {
        allObjects = GameObject.FindObjectsOfType<ObjectController>();
    }


    //HERE we set the objects to the menu places
    public void UpdateObjects()
    {

        print("Update obj");
        for (int i = 0; i < menuObjects.Length; i++)
        {


            int j = i;

            if (j >= player.objectsToBeInstantiated.Length) //cycle thru all potential objs
            {
                j = 0;
            }
            menuObjects[i].SetObject(player.objectsToBeInstantiated[j]); //temp

            foreach(ObjectController o in uIManager.objectsThatWereCombined)
            {
                if(o.objName == menuObjects[i].myObject.objName)
                {
                    menuObjects[i].isLocked = false;
                }
            }

            foreach(ObjectController o in allObjects)
            {
                if(o.ObjectToSpawn == menuObjects[i].myObject)
                {
                    print("object");
                    menuObjects[i].objectsThatProduceThis = new string[] { o.objName, o.partnerName};
                }
            }

        }
    }


    // Update is called once per frame
    void Update()
    {

            foreach (Transform t in contentBox)
            {
                float size = GetIconSize(pointer.position, t.position);
                t.localScale = new Vector3(size, size, size);

            if (t == omni.closestItem.transform)
            {
                t.GetComponent<MenuObject>().anim.SetBool("menuIn", true);
            } else {
                t.GetComponent<MenuObject>().anim.SetBool("menuIn", false);
            }

            }

    }
}
