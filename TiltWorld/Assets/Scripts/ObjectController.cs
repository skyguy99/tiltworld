using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public string objName;
    public string partnerName;
    WorldController myWorld;

    public bool triggerAttack;
    iOSHapticFeedback iosHaptic;
    AudioSource audio;

    public bool isPriority; //only priority creates combine object
    Vector3 originalPos; //resets

    PlayerController player;
    public Transform ObjectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();
        audio = GameObject.FindObjectOfType<AudioSource>();
        originalPos = transform.position;
        player = GameObject.FindObjectOfType<PlayerController>();
        myWorld = transform.parent.GetComponentInParent<WorldController>();

     
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetObject()
    {
        print("ResetObject");
        transform.position = originalPos;
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WorldBox")
        {
            iosHaptic.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
            audio.PlayOneShot(myWorld.audioClips[0]);
        }

        if (collision.gameObject.GetComponent<ObjectController>() != null)
        {
            if (collision.gameObject.GetComponent<ObjectController>().objName == partnerName && isPriority && ObjectToSpawn != null)
            {

                Destroy(collision.gameObject.gameObject);

                Transform g = Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
                g.parent = transform.parent;
                Destroy(this);
                audio.PlayOneShot(myWorld.audioClips[1]); //accent

                print("NEW OBJECT between " + objName + "| " + partnerName);

            }
        }
    }

}
