using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas canvas;
    public Animator canvasAnim;
    PlayerController player;
    Transform ObjectText;
    Serializer serializer;

    public TextMeshPro textHeadline;
    public TextMeshPro textSubtitle;
    public RadialProgress objCircle;
    public RadialProgress selectCircle;

    float deltaTime;
    Transform target;

    public Image uiImage;
    bool playingInstructions;
    public Transform circleSelector;

    VideoPlayer videoPlayer;
    public Image menu;

    Camera mainCamera;
    public Transform menuObject;
    MenuObjectSelect menuObjectSelect;

    float t = 0f;
    bool menuIn;

    public List<ObjectController> objectsThatWereCombined = new List<ObjectController>(); //list of unlocked objects
    public ObjectController tempObj;


    //public static string screenshotFilename = "screenshotName.png";
    //string pathToImage = Application.persistentDataPath + "/" + screenshotFilename;

    void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        ObjectText = GameObject.FindGameObjectWithTag("ObjectText").transform;
        serializer = GameObject.FindObjectOfType<Serializer>();
        target = null;

        ObjectText.gameObject.SetActive(false);

        PlayerPrefs.DeleteAll(); //temp
        if(!PlayerPrefs.HasKey("playedOnce"))
        {
            playingInstructions = true;
        }

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        menuObjectSelect = GameObject.FindObjectOfType<MenuObjectSelect>();
        menuObject.gameObject.SetActive(menuIn);
        canvas.enabled = !menuIn;


    }

    public void TriggerAskForReset()
    {
        canvasAnim.SetBool("triggerReset", !menuIn);
        mainCamera.GetComponent<Animator>().SetBool("menuIn", !menuIn);

        menuIn = !menuIn;
        //canvas.enabled = !menuIn;  

    }

    public void TriggerSaveScene()
    {
        canvasAnim.SetTrigger("triggerSave");
        serializer.SavePositions();
    }

    public void ToggleResetScene()
    {
        print("Resetting this scene");
        foreach(GameObject g in GameObject.FindObjectsOfType<GameObject>())
        {
            Destroy(g);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //UPDATE MENU HERE
    public void ToggleMenu()
    {

        if(!canvasAnim.GetCurrentAnimatorStateInfo(0).IsName("ResetObjectTriggerIn"))
        {
            //FOR object menu canvas
            print("this");
            canvasAnim.SetBool("buttonTouch", !menuIn);
            mainCamera.GetComponent<Animator>().SetBool("menuIn", !menuIn);

            StartCoroutine(ChangeMenuIn());

        } else {
            //for reset world canvas

            canvasAnim.SetBool("triggerReset", !menuIn);
         
            mainCamera.GetComponent<Animator>().SetBool("menuIn", !menuIn);

            menuIn = !menuIn;
        }

    }

    IEnumerator BackToNoObject()
    {
        yield return new WaitForSeconds(10f);
        target = null;
        ObjectText.gameObject.SetActive(false);
        objCircle.ToggleSelectCircleDown();

    }

    //ADDS OBJECT
    public void ShowObjectText(Transform obj, string headline, string subtext, bool showCircle)
    {
        ObjectText.gameObject.SetActive(true);
        target = obj;
        textHeadline.text = headline;
        textSubtitle.text = subtext;

        StartCoroutine(BackToNoObject());
        objCircle.ToggleSelectCircle(Camera.main.WorldToScreenPoint(obj.transform.position), false);

        objectsThatWereCombined.Add(obj.GetComponent<ObjectController>());

        //check explicitly


    }

    IEnumerator ChangeMenuIn()
    {
        menuIn = !menuIn;



        yield return new WaitForSeconds(0f);

        menuObject.gameObject.SetActive(menuIn);

        menuObjectSelect.UpdateObjects();


        canvas.enabled = !menuIn;
        //canvasAnim.SetBool("buttonTouch", false);

    }

    void Update()
    {
    

        if (target != null)
        {
            ObjectText.transform.position = new Vector3(target.transform.position.x, target.transform.position.y+3.8f, target.transform.position.z - 0.3f);


            objCircle.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);

        }

        if(playingInstructions)
        {

            PlayerPrefs.SetInt("playedOnce", 1);

        }

        if(Input.GetKey(KeyCode.A) && !menuIn)
        {
            TriggerAskForReset();
        }


        //TESTING
        //deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        //float fps = 1.0f / deltaTime;
        //print("fps: " + fps);
    }
}
