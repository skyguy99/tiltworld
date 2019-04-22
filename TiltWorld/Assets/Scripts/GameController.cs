using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.VR;
//using GoogleVR;
//using Gvr.Internal;

public class GameController : MonoBehaviour {

    PlayerController playerController;
    CharController character;

	// Use this for initialization
	void Awake () {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        character = playerController.character;

    }

    private void Start()
    {

        //GameObject.FindObjectOfType<GameDataController>().LoadData();
        print("Player data:"+GameDataController.GetPlayerControllerState(0).position + "|"+ GameDataController.GetPlayerControllerState(0).rotation);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SaveWholeWorld()
    {
        print("saving whole world");

        playerController = GameObject.FindObjectOfType<PlayerController>();
        GameDataController.SetState("DATE", playerController, character);
    }

}
