using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuObject : MonoBehaviour
{
	public string objName;
	Text headline;
    TextMeshPro subtext;
    PlayerController player;
    public Animator anim;
    public ObjectController myObject;
    Transform container;
    public Transform platContainer;
    UIManager uIManager;
    public string[] objectsThatProduceThis;

    Vector3 rot = new Vector3(17.1f, -129f, 21f);

    MenuObjectSelect menuObjectSelect;
    Material originMat;
    public bool isLocked = true;

    bool hasSet;


    void Awake()
    {
        uIManager = GameObject.FindObjectOfType<UIManager>();
        menuObjectSelect = GameObject.FindObjectOfType<MenuObjectSelect>();
        anim = GetComponent<Animator>();
        player = GameObject.FindObjectOfType<PlayerController>();
        platContainer = Instantiate(menuObjectSelect.platform);
        platContainer.parent = transform;
        platContainer.localPosition = menuObjectSelect.platform.localPosition;
        platContainer.rotation = menuObjectSelect.platform.rotation;

        foreach (Transform t in transform)
        {
            if (t.name == "Title")
            {
                headline = t.GetComponent<Text>();
            }
            else if (t.name == "Subtitle")
            {
                subtext = t.GetComponent<TextMeshPro>();
            }
            else if (t.name == "Object")
            {
                container = t;

            } else {
                platContainer = t;
            }

        }
    }

    public void SetObject(ObjectController o)
    {

        myObject = o;
        Transform c = c = container.GetChild(0);
        //SET object item

        GameObject go = container.GetChild(0).gameObject;
        if (!hasSet)
        {
            hasSet = true;
            go = Instantiate(o.gameObject, c.position, Quaternion.identity);
            go.transform.parent = container;
            go.transform.localScale = c.localScale;
            Destroy(go.GetComponent<Rigidbody>());
            Destroy(go.GetComponent<ObjectController>());
            Destroy(c.gameObject);
            go.transform.eulerAngles = rot;
        }

        if (originMat == null)
        {
            originMat = o.gameObject.GetComponent<Renderer>().sharedMaterial;
        }

        for (int i = 0; i < platContainer.childCount; i++)
        {
            platContainer.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            platContainer.GetChild(i).gameObject.SetActive(i == myObject.world);
        }


        objName = o.objName;
        headline.text = isLocked ? "??" : objName.ToUpper();

        if (player.combineObjects.ContainsKey(objName))
        {
         
            string text = "(" + player.combineObjects[objName][0].ToUpper() + " + " + player.combineObjects[objName][1].ToUpper() + ")";
            subtext.text = text;
        }

        subtext.transform.localPosition = new Vector3(subtext.transform.localPosition.x, subtext.transform.localPosition.y, -135f);

        go.transform.eulerAngles = rot;

        //Materials

        go.transform.GetComponent<Renderer>().material = isLocked ? menuObjectSelect.blackMat : originMat;
        for (int i = 0; i < platContainer.childCount; i++)
        {
            platContainer.GetChild(i).GetComponent<Renderer>().material = isLocked ? menuObjectSelect.blackMat : menuObjectSelect.platform.GetChild(i).GetComponent<Renderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {

        foreach(ObjectController o in uIManager.objectsThatWereCombined)
        {
            if(o.objName == myObject.objName)
            {
                isLocked = false;
            }
        }
      
        if(container.transform.childCount > 0)
        {

            container.GetChild(0).gameObject.GetComponent<Renderer>().material = isLocked ? menuObjectSelect.blackMat : originMat;
            if (container.GetChild(0).eulerAngles != rot)
            {
                container.GetChild(0).eulerAngles = rot;
            }
        }

    }
}
