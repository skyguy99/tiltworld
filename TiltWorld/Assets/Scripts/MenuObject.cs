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
        player = GameObject.FindObjectOfType<PlayerController>();
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
        Transform c = container.GetChild(0);
        GameObject go = Instantiate(o.gameObject, c.position, c.localRotation);
        go.transform.parent = container;
        go.transform.localScale = c.localScale;
        Destroy(go.GetComponent<Rigidbody>());
        Destroy(go.GetComponent<ObjectController>());

        Destroy(c.gameObject);

        objName = o.objName;
        headline.text = objName.ToUpper();
        if (player.combineObjects.ContainsKey(objName.ToLower()))
        {
            string text = "(" + player.combineObjects[objName.ToLower()][0].ToUpper() + " + " + player.combineObjects[objName.ToLower()][1].ToUpper() + ")";
            subtext.text = text;
        }
        else
        {
            subtext.text = "null";
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
