using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector3 lerpPos;

    // Start is called before the first frame update
    void Start()
    {
        //lerpPos = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y + 0.1f, transform.GetChild(0).position.z);
        lerpPos = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
       
    }



    // Update is called once per frame
    void Update()
    {
        //collider.enabled = (character.transform.position.y <= character.originalY);
    }
}
