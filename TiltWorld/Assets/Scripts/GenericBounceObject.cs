using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class GenericBounceObject : MonoBehaviour
{

    public Vector3 originalPos;
    public BoxCollider room;

    public bool isTitle;
    IntroUI introUI;
    GyroController gyro;
    bool hasBeenSelected;
 

    // Start is called before the first frame update
    void Awake()
    {
        introUI = GameObject.FindObjectOfType<IntroUI>();
        gyro = GameObject.FindObjectOfType<GyroController>();
    }

    void TriggerIntroSavedWorlds()
    {
        introUI.MoveInSavedWorlds(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<LeanSelectable>().IsSelected)
        {
            hasBeenSelected = true;
        }
        if(isTitle && !hasBeenSelected)
        {
            //transform.rotation = new Quaternion(transform.rotation.x, gyro.transform.rotation.y, transform.rotation.z, transform.rotation.w);
            transform.rotation = new Quaternion(gyro.transform.rotation.x, gyro.transform.rotation.y, gyro.transform.rotation.z, gyro.transform.rotation.w);
        }

        if (!room.bounds.Contains(transform.position) && Time.time > 2f && !introUI.showingSavedWorldUI)
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", originalPos, "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));
            if(isTitle)
            {
                Invoke("TriggerIntroSavedWorlds", 0.29f);
            }
        }
    }
}
