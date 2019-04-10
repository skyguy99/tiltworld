using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroUI : MonoBehaviour
{

    int tapCount;
    float doubleTapTimer;
    public bool showingSavedWorldUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void MoveInSavedWorlds(GameObject g)
    {
        print("show saved worlds");
        showingSavedWorldUI = true;
        iTween.MoveTo(g, iTween.Hash("position", new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tapCount++;
        }
        if (tapCount > 0)
        {
            doubleTapTimer += Time.deltaTime;
        }
        if (tapCount >= 2)
        {
            //What you want to do
            print("DOUBLE");
            SceneManager.LoadScene("TiltWorldScene");

            doubleTapTimer = 0.0f;
            tapCount = 0;
        }
        if (doubleTapTimer > 0.5f)
        {
            doubleTapTimer = 0f;
            tapCount = 0;
        }

        //for testing
        if(Input.GetKey("space"))
        {
            SceneManager.LoadScene("TiltWorldScene");
        }
    }
}
