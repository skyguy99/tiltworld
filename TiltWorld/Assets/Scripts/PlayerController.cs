using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    iOSHapticFeedback iosHaptic;
    public int selectedWorld;
    public bool inWorld;

    public CharController character;

    public Transform testprefab;
    public Material[] skyboxes;

    // Use this for initialization
    void Start()
    {
        character = GameObject.FindObjectOfType<CharController>();
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();

    }

    void Update()
    {
        inWorld = (selectedWorld > 0);
        //playerMoveDir = character.transform.position - playerPrevPos;
        //playerMoveDir.Normalize();
        //transform.position = character.transform.position - playerMoveDir * distance;

        //TEST ----------------------
        UpdateSkybox();
       
    }

    public void UpdateSkybox()
    {
        //RenderSettings.skybox.Lerp(skyboxes[0], skyboxes[1], 0.5f * Time.deltaTime);
        RenderSettings.skybox.SetColor("Top Color", Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1)));

    }

  
}
