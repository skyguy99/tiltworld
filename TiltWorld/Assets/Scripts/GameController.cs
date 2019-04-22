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
          
    void SetStates(int worldNum)
    {
        print("Player data:" + GameDataController.GetPlayerControllerState(worldNum).position);
        print("Character: " + GameDataController.GetCharacterState(worldNum).position);

        character.transform.position = GameDataController.GetCharacterState(worldNum).position;
        character.transform.rotation = GameDataController.GetCharacterState(worldNum).rotation;

        playerController.transform.position = GameDataController.GetPlayerControllerState(worldNum).position;
        playerController.transform.rotation = GameDataController.GetPlayerControllerState(worldNum).rotation;
    }  

    private void Start()
    {
        //set world state 

        //GameObject.FindObjectOfType<GameDataController>().LoadData();

        SetStates(0);
        //print("Player data:" + GameDataController.GetPlayerControllerState(worldNum).position);
        //print("Character: " + GameDataController.GetCharacterState(worldNum).position);
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
