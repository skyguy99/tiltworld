using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarder : MonoBehaviour {

    // Use this for initialization
   public Transform target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {

        // look at camera...
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
        // then lock rotation to Y axis only
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        //transform.rotation = target.rotation;
        //transform.rotation = Quaternion.Euler(transform.rotation.x, target.rotation.y, transform.rotation.z);




    }
}
