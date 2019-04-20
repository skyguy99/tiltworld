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

    public Transform exampleChild;
    public Transform ScrollItemPrefab;
    public Transform scrollContainer;
    List<ScrollItem> scrollItems = new List<ScrollItem>();

    // Start is called before the first frame update
    void Awake()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
        canvas.enabled = false;
    
    }

    private void Start()
    {
        SetupScrollContent();
        exampleChild.gameObject.SetActive(false);
    }

    void SetupScrollContent()
    {
        print(scrollContainer.transform.position.x);
        //scrollContainer.transform.localPosition = new Vector3(100f, scrollContainer.localPosition.y, scrollContainer.localPosition.z);
        for(int i = 0; i<4;i++)
        {
            Transform s = Instantiate(ScrollItemPrefab);
            scrollItems.Add(s.GetComponent<ScrollItem>());
            s.GetComponent<ScrollItem>().worldNum = i;
            s.parent = scrollContainer;
            s.localScale = new Vector3(1, 1, 1);
            s.localRotation = exampleChild.localRotation;
            s.rotation = exampleChild.rotation;
            s.localPosition = new Vector3();
        }
    }


    void MoveOutSavedWorlds()
    {

    }
    public void MoveInSavedWorlds(GameObject g)
    {

        print("show saved worlds");
        showingSavedWorldUI = true;
        canvas.enabled = true;
        canvas.GetComponent<Animator>().SetBool("triggerReset", true);
        //characterSelector.gameObject.SetActive(true);
        //Camera.main.enabled = false;
        //otherCam.enabled = true;
       
        cameraAnim.SetBool("toBlur", true);
        iTween.MoveTo(g, iTween.Hash("position", new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), "time", 0.55f, "easetype", iTween.EaseType.easeOutBounce, "oncomplete", "DisableKinematic", "oncompletetarget", gameObject));

    }

    public void LoadScene()
    {
        PlayerPrefs.SetInt("loadedWorld", 0);
        canvas.GetComponent<Animator>().SetBool("triggerReset", false);
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
