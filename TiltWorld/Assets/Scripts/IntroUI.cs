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
    public Animator canvasAnim;

    public Transform exampleChild;
    public Transform ScrollItemPrefab;
    public Transform scrollContainer;
    List<ScrollItem> scrollItems = new List<ScrollItem>();
    GameDataController gameDataController;

    public ScrollItem selectedItem;

    // Start is called before the first frame update
    void Awake()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
        canvas.enabled = false;

        gameDataController = GameObject.FindObjectOfType<GameDataController>();
    }

    private void Start()
    {
        SetupScrollContent();

    }

    void SetupScrollContent()
    {
        //print(gameDataController.NumberOfWorlds);
        //scrollContainer.transform.localPosition = new Vector3(100f, scrollContainer.localPosition.y, scrollContainer.localPosition.z);
        for(int i = 0; i<gameDataController.NumberOfWorlds;i++)
        {
            print("doing this");
            Transform s = Instantiate(ScrollItemPrefab);
            scrollItems.Add(s.GetComponent<ScrollItem>());

            s.GetComponent<ScrollItem>().worldNum = i;
            s.GetComponent<ScrollItem>().savedDate = GameDataController.GetFullWorldState(i).date;
            s.GetComponent<ScrollItem>().selectedWorld = GameDataController.GetFullWorldState(i);

            //s.parent = scrollContainer;
            //s.localScale = new Vector3(1, 1, 1);
            //s.localRotation = exampleChild.localRotation;
            //s.rotation = exampleChild.rotation;
            //s.localPosition = exampleChild.localPosition;
        }
    }

    public void MoveInSavedWorlds(GameObject g)
    {
    
        showingSavedWorldUI = true;
        canvas.enabled = true;
      
        //characterSelector.gameObject.SetActive(true);
        //Camera.main.enabled = false;
        //otherCam.enabled = true;

        cameraAnim.SetBool("toBlur", true);
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
