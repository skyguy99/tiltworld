using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public string objName;
    public string partnerName;

    public bool isPriority; //only priority creates combine object
    Vector3 originalPos; //resets

    public GameObject combineObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetObject()
    {
        transform.position = originalPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ObjectController>() != null)
        {
            if (other.GetComponent<ObjectController>().objName == partnerName)
            {
                Destroy(this);

            }
        }
    }
}
