using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas canvas;
    public Transform objectShowContainer;

    void Start()
    {
        foreach (Transform child in objectShowContainer)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void ShowObjectCreated(string name)
    {
        foreach(Transform child in objectShowContainer)
        {
            if(child.GetComponent<ObjectController>().objName == name)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    void BackToNoObject()
    {
        foreach (Transform child in objectShowContainer)
        {
            child.gameObject.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
