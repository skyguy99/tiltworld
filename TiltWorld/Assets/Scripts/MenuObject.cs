using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuObject : MonoBehaviour
{
	public string objName;
	Text headline;
	Text subtext;
    PlayerController player;
    public Animator anim;

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
                subtext = t.GetComponent<Text>();
            }
		}

        headline.text = objName.ToUpper();
        if(player.combineObjects.ContainsKey(objName.ToLower()))
        {
            string text = "("+player.combineObjects[objName.ToLower()][0].ToUpper() + " + "+ player.combineObjects[objName.ToLower()][1].ToUpper()+")";
            subtext.text = text;
        } else {
            subtext.text = "OBJ 1 + OBJ 2";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
