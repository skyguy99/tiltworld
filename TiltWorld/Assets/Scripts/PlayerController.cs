using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public int selectedWorld;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WorldBox")
        {
            print("World enter!!");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WorldBox")
        {
            print("World exit!!");
        }
    }
}
