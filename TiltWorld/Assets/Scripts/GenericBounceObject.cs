using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBounceObject : MonoBehaviour
{

    public Vector3 originalPos;
    public BoxCollider room;

    public bool isTitle;
    IntroUI introUI;
 

    // Start is called before the first frame update
    void Start()
    {
        introUI = GameObject.FindObjectOfType<IntroUI>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if(isTitle && transform.position.y >= 3.5f && !introUI.showingSavedWorldUI)
        {
            introUI.MoveInSavedWorlds(gameObject);
        }

        if (!room.bounds.Contains(transform.position) && Time.time > 2f && !introUI.showingSavedWorldUI)
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
        }
    }
}
