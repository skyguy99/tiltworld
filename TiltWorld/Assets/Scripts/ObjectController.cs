using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public string objName;
    public string partnerName;

    public bool triggerAttack;
    iOSHapticFeedback iosHaptic;
    AudioSource audio;

    public bool isPriority; //only priority creates combine object
    Vector3 originalPos; //resets
    BoxCollider room;

    PlayerController player;
    public Transform ObjectToSpawn;
    UIManager uIManager;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = Vector3.zero;
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();
        audio = GameObject.FindObjectOfType<AudioSource>();
        player = GameObject.FindObjectOfType<PlayerController>();
        uIManager = GameObject.FindObjectOfType<UIManager>();
     
    }

    // Update is called once per frame
    void Update()
    {
        room = player.room;
        if (!room.bounds.Contains(transform.position))
        {
            ResetObject();
        }
    }

    public void ResetObject()
    {
        print("ResetObject");
        transform.position = originalPos;
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" && originalPos == Vector3.zero)
        {
            originalPos = transform.position;
        }

        if (collision.gameObject.GetComponent<ObjectController>() != null)
        {
            if (collision.gameObject.GetComponent<ObjectController>().objName == partnerName && isPriority && ObjectToSpawn != null)
            {

                Destroy(collision.gameObject.gameObject);

                Transform g = Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
                g.parent = transform.parent;
                Destroy(this);
                //audio.PlayOneShot(myWorld.audioClips[1]); //accent

                print("NEW OBJECT between " + objName + "| " + partnerName);
                uIManager.ShowObjectText(g, g.GetComponent<ObjectController>().objName, "Subtext here");

            }
        }
    }

}
