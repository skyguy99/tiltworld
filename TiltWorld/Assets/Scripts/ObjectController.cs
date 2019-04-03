using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class ObjectController : MonoBehaviour
{

    public bool isOnPlane;
    public string objName;
    public string partnerName;

    public bool triggerAttack;
    iOSHapticFeedback iosHaptic;

    public bool isPriority; //only priority creates combine object
    Vector3 originalPos; //resets
    BoxCollider room;

    PlayerController player;
    public Transform ObjectToSpawn;
    UIManager uIManager;
    public Renderer renderer;
    public Rigidbody rb;
    public LeanSelectable leanSelectable;
    public int world;


    string ToTitleCase(string stringToConvert)
    {
        string firstChar = stringToConvert[0].ToString();
        return (stringToConvert.Length > 0 ? firstChar.ToUpper() + stringToConvert.Substring(1) : stringToConvert);

    }



    // Start is called before the first frame update
    void Awake()
    {

        originalPos = Vector3.zero;
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();
        renderer = GetComponent<Renderer>();
        player = GameObject.FindObjectOfType<PlayerController>();
        uIManager = GameObject.FindObjectOfType<UIManager>();
        rb = GetComponent<Rigidbody>();

        leanSelectable = GetComponent<LeanSelectable>();

        if(GetComponentInParent<WorldController>() != null)
        {
            world = GetComponentInParent<WorldController>().num;
        }

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindObjectOfType<PlayerController>();

        room = player.room;
        if (!room.bounds.Contains(transform.position))
        {
            ResetObject();
        }
    }

    public void MoveToiTween(Vector3 vector, float duration)
    {
        rb.isKinematic = true;
        iTween.MoveTo(gameObject, iTween.Hash("position", vector, "time", duration, "easetype", iTween.EaseType.easeOutQuad, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
    }

    void DisableKinematic()
    {
        rb.isKinematic = false;
    }

    public void ResetObject()
    {
        //print("ResetObject");
        //transform
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CenterCreation>() != null)
        {
            other.GetComponent<CenterCreation>().CollidedWith(gameObject.GetComponent<ObjectController>());
        }
    }

    void test()
    {
        isOnPlane = true;
    }

    private void OnCollisionStay(Collision collision)
    {
    
       if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Draggable")
        {
            isOnPlane = true;
        }
         else if (!leanSelectable.IsSelected)
        {
            isOnPlane = false;
        }


        //print(collision.gameObject.tag == "Wall");
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.tag == "Draggable")
        //{
        //    isOnPlane = false;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "Wall" && originalPos == Vector3.zero)
        {
            originalPos = transform.position;
        }


        if (collision.gameObject.GetComponent<ObjectController>() != null)
        {
            if (collision.gameObject.GetComponent<ObjectController>().objName == partnerName && isPriority && ObjectToSpawn != null && (renderer.isVisible)) //&& (renderer.isVisible)
            {

                this.gameObject.SetActive(false);
                //Destroy(collision.gameObject.gameObject);

                Transform g = Instantiate(ObjectToSpawn, new Vector3(transform.position.x, transform.position.y+1f, transform.position.z), Quaternion.identity);
                //Instantiate(player.explodeCubes, transform.position, Quaternion.identity);

                g.parent = transform.parent;
                g.localScale = new Vector3(100f, 100f, 100f);
                Destroy(this);
                player.audioAccents.PlayOneShot(player.audioClips[1]); //accent

                print("NEW OBJECT between " + objName + "| " + partnerName);
                iosHaptic.Trigger(iOSHapticFeedback.iOSFeedbackType.Success);
                uIManager.ShowObjectText(g, "COMBO ITEM", ToTitleCase(g.GetComponent<ObjectController>().objName), true);

            }
        } else if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Room")
        { //is not player or other object
            player.audioAccents.PlayOneShot(player.audioClips[0]); //metal hit
        }
    }

}
