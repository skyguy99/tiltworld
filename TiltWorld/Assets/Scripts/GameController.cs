using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.VR;
//using GoogleVR;
//using Gvr.Internal;

public class GameController : MonoBehaviour {

    PlayerController playerController;

	// Use this for initialization
	void Start () {
        playerController = GameObject.FindObjectOfType<PlayerController>();

        print(GameDataController.GetState(0));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveWholeWorld()
    {
        print("saving whole world");
        GameDataController.SetState("DATE", playerController.objects);
    }

}
