using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameController : MonoBehaviour {

    PlayerController playerController;
    CharController character;
    string dateNow;


    // Use this for initialization
    void Awake () {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        character = playerController.character;

    }
          
    void SetStates(int worldNum)
    {
        print("Player data:" + GameDataController.GetPlayerControllerState(worldNum).position);
        print("Character: " + GameDataController.GetCharacterState(worldNum).position);

        if (GameDataController.GetCharacterState(worldNum).position != Vector3.zero)
        {
            character.transform.position = GameDataController.GetCharacterState(worldNum).position;
            character.transform.rotation = GameDataController.GetCharacterState(worldNum).rotation;

            playerController.transform.position = GameDataController.GetPlayerControllerState(worldNum).position;
            playerController.transform.rotation = GameDataController.GetPlayerControllerState(worldNum).rotation;

            playerController.UpdateObjectsArrays();
            foreach(ObjectController o in playerController.objects)
            {
                foreach(ObjectControllerData obj in GameDataController.GetObjectControllerStates(worldNum))
                {
                    if(obj.name == o.name)
                    {
                        o.transform.position = obj.position;
                        o.transform.rotation = obj.rotation;
                        o.gameObject.SetActive(obj.isInWorld);
                    }
                }
            }

            foreach (WorldController o in playerController.worlds)
            {
                foreach (WorldContainerData obj in GameDataController.GetWorldControllerStates(worldNum))
                {
                    if (obj.worldNum == o.num)
                    {
                        o.transform.position = obj.position;
                        o.transform.rotation = obj.rotation;

                    }
                }
            }


        }
      
    }  

    private void Start()
    {
        print("HAS KEY: " + (PlayerPrefs.HasKey("loadedWorld")));
        if (PlayerPrefs.HasKey("loadedWorld"))  
        {
            print("Loading world "+ PlayerPrefs.GetInt("loadedWorld"));
            SetStates(PlayerPrefs.GetInt("loadedWorld"));
        } else
        {
            print("fallback");
            SetStates(0);
        }
    }

    // Update is called once per frame
    void Update () {

        dateNow = DateTime.Now.ToString("MM-dd-yyyy")+"@"+DateTime.Now.Hour+":"+DateTime.Now.Minute;
     
    }

    public void SaveWholeWorld()
    {
        print("saving whole world");

        playerController = GameObject.FindObjectOfType<PlayerController>();
        playerController.UpdateObjectsArrays();

        if (PlayerPrefs.HasKey("loadedWorld"))
        {
            GameDataController.SetState(dateNow, PlayerPrefs.GetInt("loadedWorld"), playerController, playerController.character, playerController.objects, playerController.worlds);
            //PlayerPrefs.SetInt("loadedWorld", PlayerPrefs.GetInt("loadedWorld"));
        } else
        {
            print("first");
            GameDataController.SetState(dateNow, 0, playerController, playerController.character, playerController.objects, playerController.worlds);
            PlayerPrefs.SetInt("loadedWorld", 0);
            PlayerPrefs.Save();
        }
       

    }

}
