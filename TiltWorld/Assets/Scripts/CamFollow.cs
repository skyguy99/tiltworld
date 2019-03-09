using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject player;
   float cameraHeight = 2.79f;
    float cameraZ = -5.29f;


    private void Awake()
    {
        player = GameObject.FindObjectOfType<CharController>().gameObject;
    }
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y+cameraHeight, player.transform.position.z+cameraZ);
    }
}
