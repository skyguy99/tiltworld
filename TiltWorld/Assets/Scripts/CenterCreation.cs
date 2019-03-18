using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCreation : MonoBehaviour
{
    // Start is called before the first frame update

    UIManager uIManager;
   
    //store object name, then object that it instantiates
    [System.Serializable]
    public class ObjectsThatCollide
    {

    }

    public List<string> objectsThatHaveCollided; //certain 3 object combos produce different end center creations (order matters)



    void Start()
    {
        uIManager = GameObject.FindObjectOfType<UIManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<ObjectController>() != null && collision.gameObject.GetComponent<ObjectController>().renderer.isVisible)
        {
            Destroy(collision.gameObject);

        }
    }

    void TriggerUIFinalAlert()
    {
        uIManager.ShowObjectText(transform, "MASTERPIECE", "You created a new masterpiece", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
