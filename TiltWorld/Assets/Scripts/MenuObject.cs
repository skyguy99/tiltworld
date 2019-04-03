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
    ObjectController myObject;
    Transform container;
    public Transform platContainer;

    Vector3 rot = new Vector3(17.1f, -129f, 21f);

    MenuObjectSelect menuObjectSelect;
    Material originMat;
    public bool isLocked = true;

    void Awake()
    {
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

        //SET object item
        Transform c = container.GetChild(0);
        GameObject go = Instantiate(o.gameObject, c.position, Quaternion.identity);
        go.transform.parent = container;


        if(originMat == null)
        {
            originMat = o.gameObject.GetComponent<Renderer>().sharedMaterial;
        }

        go.transform.localScale = c.localScale;
        Destroy(go.GetComponent<Rigidbody>());
        Destroy(go.GetComponent<ObjectController>());

        for (int i = 0; i < platContainer.childCount; i++)
        {
            platContainer.GetChild(i).gameObject.SetActive(i == myObject.world);
        }



        Destroy(c.gameObject);
        go.transform.eulerAngles = rot;

        objName = o.objName;
        headline.text = objName.ToUpper();
        if (player.combineObjects.ContainsKey(objName.ToLower()))
        {
            string text = "(" + player.combineObjects[objName.ToLower()][0].ToUpper() + " + " + player.combineObjects[objName.ToLower()][1].ToUpper() + ")";
            subtext.text = text;
        }
        else
        {
            //islocked
            headline.text = "HELLO";
            subtext.text = "null";
        }


        //HERE
        //headline.enabled = isLocked;
        //subtext.enabled = isLocked;

        //go.GetComponent<Renderer>().sharedMaterial = isLocked ? menuObjectSelect.blackMat : originMat;
        if (isLocked)
        {
            //lock icon

        }
        go.transform.eulerAngles = rot;

    }

    // Update is called once per frame
    void Update()
    {
       
        if (container.GetChild(0).eulerAngles != rot)
        {
            container.GetChild(0).eulerAngles = rot;
        }
       
    }
}
