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
        SetupScrollContent();
    }

    private void Start()
    {
      

    }

    void SetupScrollContent()
    {

        for(int i =0; i<scrollContainer.childCount; i++)
        {

            //if(i >= GameDataController.NumberOfWorlds && !scrollContainer.GetChild(i).GetComponent<ScrollItem>().isTheAddWorld)
            //{
            //    scrollContainer.GetChild(i).gameObject.SetActive(false);
            //} else
            //{
            //    scrollContainer.GetChild(i).gameObject.SetActive(true);
            //}

            scrollContainer.GetChild(i).GetComponent<ScrollItem>().worldNum = i;
            if(GameDataController.GetFullWorldState(i).date != "")
            {
                scrollContainer.GetChild(i).GetComponent<ScrollItem>().savedDate = GameDataController.GetFullWorldState(i).date;
            } else
            {
                scrollContainer.GetChild(i).GetComponent<ScrollItem>().savedDate = "null";
            }

            //set world
            print("Number of worlds: "+GameDataController.NumberOfWorlds+"setting world " + GameDataController.GetFullWorldState(i).player.position);
            scrollContainer.GetChild(i).GetComponent<ScrollItem>().selectedWorld = GameDataController.GetFullWorldState(i);

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
