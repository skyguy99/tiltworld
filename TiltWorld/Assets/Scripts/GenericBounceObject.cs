using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBounceObject : MonoBehaviour
{

    public Vector3 originalPos;
    public BoxCollider room;

    // Start is called before the first frame update
    void Start()
    {

      
    }

    // Update is called once per frame
    void Update()
    {


        if (!room.bounds.Contains(transform.position) && Time.time > 2f)
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
        }
    }
}
