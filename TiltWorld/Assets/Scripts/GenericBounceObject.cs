using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBounceObject : MonoBehaviour
{
    PlayerController player;
    public Vector3 originalPos;
    BoxCollider room;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

         room = player.room;
        if (!room.bounds.Contains(transform.position) && Time.time > 2f)
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
        }
    }
}
