using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroUI : MonoBehaviour
{

    int tapCount;
    float doubleTapTimer;
    public bool showingSavedWorldUI;
    public Animator cameraAnim;
    Canvas canvas;
    public Transform characterSelector;

    // Start is called before the first frame update
    void Awake()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
        canvas.enabled = false;
    
    }


    void MoveOutSavedWorlds()
    {

    }
    public void MoveInSavedWorlds(GameObject g)
    {

        print("show saved worlds");
        showingSavedWorldUI = true;
        canvas.enabled = true;
        //characterSelector.gameObject.SetActive(true);
        //Camera.main.enabled = false;
        //otherCam.enabled = true;
       
        cameraAnim.SetBool("toBlur", true);
        iTween.MoveTo(g, iTween.Hash("position", new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));

    }

    public void LoadScene()
    {
        SceneManager.LoadScene("TiltWorldScene");
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
            //SceneManager.LoadScene("TiltWorldScene");

            doubleTapTimer = 0.0f;
            tapCount = 0;
        }
        if (doubleTapTimer > 0.5f)
        {
            doubleTapTimer = 0f;
            tapCount = 0;
        }
    }
}
