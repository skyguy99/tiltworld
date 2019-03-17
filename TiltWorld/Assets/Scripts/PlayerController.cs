using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
     
    iOSHapticFeedback iosHaptic;
    public Transform selectedObject;

    public BoxCollider room;
    public CharController character;
    Vector3 originPos;

    public AudioClip[] audioClips;
    public AudioSource audioAccents;
    public AudioSource audioBackground;
    public Mover mover;

    public bool sawCharacter;

    // Use this for initialization
    void Start()
    {
        // = GameObject.FindGameObjectWithTag("Room").GetComponent<BoxCollider>();
        character = GameObject.FindObjectOfType<CharController>();
        iosHaptic = GameObject.FindObjectOfType<iOSHapticFeedback>();
        originPos = transform.position;
        mover = GetComponent<Mover>();
        audioBackground = GetComponent<AudioSource>();
    }

    public void ResetPosition()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", originPos, "time", 0.45f, "easetype", iTween.EaseType.easeOutExpo));
    }

    void Update()
    {
       
        if (!room.bounds.Contains(transform.position))
        {
            //ResetPosition();
        }
           
        }

}
