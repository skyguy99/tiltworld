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
    public Transform container;

    public bool isLocked = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindObjectOfType<PlayerController>();
		foreach(Transform t in transform)
		{
            if(t.name == "Title")
            {
                headline = t.GetComponent<Text>();
            } else if (t.name == "Subtitle")
            {
                subtext = t.GetComponent<TextMeshPro>();
            } else if (t.name == "Object")
            {
                container = t;
            }
		}

    }

    public void SetObject(ObjectController o)
    {
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
            }
        }
        myObject = o;

        //SET object item
        print(container.transform.childCount);

        objName = o.objName;
        headline.text = objName.ToUpper();
        if (player.combineObjects.ContainsKey(objName.ToLower()))
        {
            string text = "(" + player.combineObjects[objName.ToLower()][0].ToUpper() + " + " + player.combineObjects[objName.ToLower()][1].ToUpper() + ")";
            subtext.text = text;
        }
        else
        {
            subtext.text = "OBJ 1 + OBJ 2";
        }

        headline.enabled = isLocked;
        subtext.enabled = isLocked;
        if (isLocked)
        {
            //lock icon

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
