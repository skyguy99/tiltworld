using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Lean.Touch;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas canvas;
    public Animator canvasAnim;
    PlayerController player;
    public Transform ObjectText;

    public TextMeshPro textHeadline;
    public TextMeshPro textSubtitle;
    public RadialProgress objCircle;
    public RadialProgress selectCircle;

    float deltaTime;
    public Transform target;

    public Image uiImage;
    bool playingInstructions;
    public Transform circleSelector;

    VideoPlayer videoPlayer;
    public Image menu;
    GameController gc;
    GameDataController gameDataController;
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
        target = null;
        gc = GameObject.FindObjectOfType<GameController>();
   

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        menuObjectSelect = GameObject.FindObjectOfType<MenuObjectSelect>();
        menuObject.gameObject.SetActive(menuIn);
        canvas.enabled = !menuIn;
        gameDataController = GameObject.FindObjectOfType<GameDataController>();

    }

    public void TriggerAskForReset()
    {
        canvasAnim.SetBool("triggerReset", !menuIn);
        mainCamera.GetComponent<Animator>().SetBool("menuIn", !menuIn);

        menuIn = !menuIn;
        canvas.enabled = true;

    }

    //SAVE SCENE JSON!!!
    public void TriggerSaveScene()
    {
        canvasAnim.SetBool("touchedSaved", true);
        canvasAnim.SetTrigger("triggerSave");

        gc = GameObject.FindObjectOfType<GameController>();

        gc.SaveWholeWorld();
        gameDataController.SaveGame();
    }

    public void BackToMenu()
    {

        gc.SaveWholeWorld();
        gameDataController.SaveGame();
        foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>())
        {
            Destroy(g);
        }
        SceneManager.LoadScene("INTRO");


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

    //toggles overall options
    public void ToggleGlobalMenu()
    {

       
        TriggerSaveScene();
        if (!canvasAnim.GetCurrentAnimatorStateInfo(0).IsName("ResetObjectTriggerIn"))
        {
            //FOR object menu canvas

            canvasAnim.SetBool("buttonTouch", !menuIn);
            mainCamera.GetComponent<Animator>().SetBool("menuIn", !menuIn);
            menuIn = !menuIn;
            RemoveObjectMenu();
        }
        else
        {
            //for reset world canvas
            canvasAnim.SetBool("buttonTouch", !menuIn);
            canvasAnim.SetBool("triggerReset", !menuIn);

            mainCamera.GetComponent<Animator>().SetBool("menuIn", !menuIn);

            menuIn = !menuIn;
        }
    }

    void RemoveObjectMenu()
    {
        canvasAnim.SetBool("onObjectMenu", false);
        menuObject.gameObject.SetActive(false);

        //menuObjectSelect.UpdateObjects();

        canvas.enabled = false;

    }

    //triggers object menu map
    public void ToggleMenu()
    {
        //print("menu map"+menuIn);

        canvasAnim.SetBool("onObjectMenu", true);
        menuObject.gameObject.SetActive(menuIn);

        menuObjectSelect.UpdateObjects();

        canvas.enabled = !menuIn;

    }

    IEnumerator BackToNoObject()
    {
        yield return new WaitForSeconds(9f);
        target = null;
       
        objCircle.ToggleSelectCircleDown();

    }
    public void BackToNoObjectImmediate()
    {
        target = null;
      
        objCircle.ToggleSelectCircleDown();
    }

    //ADDS OBJECT
    public void ShowObjectText(Transform obj, string headline, string subtext, bool showCircle)
    {
        BackToNoObjectImmediate();
        target = obj;
        StartCoroutine(BackToNoObject());

        objCircle.ToggleSelectCircle(Camera.main.WorldToScreenPoint(obj.transform.position), false);

        objectsThatWereCombined.Add(obj.GetComponent<ObjectController>());
    }

    public void TriggerIncompatible(Transform obj)
    {
        //print("TRIGGER INCOMPAT --" + target);
        if(target == null) //dont interrupt new obj indicator
        {
            target = obj;
            objCircle.ToggleIncompatibleCircle(Camera.main.WorldToScreenPoint(obj.transform.position));
        }
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

    void TriggerWinEnd()
    {

        mainCamera.GetComponent<Animator>().SetBool("menuIn", !menuIn);
        canvasAnim.SetBool("triggerWin", true);
        menuIn = !menuIn;
    }

    void Update()
    {
    
        if(menuIn)
        {
            objCircle.ToggleSelectCircleDown();
        }
       
        if (target != null)
        {
            //ObjectText.transform.position = new Vector3(target.transform.position.x, target.transform.position.y+1.9f, target.transform.position.z - 0.3f);


            objCircle.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);

        }


        if(Input.GetKey(KeyCode.A) && !menuIn)
        {
            //TriggerAskForReset();
            TriggerWinEnd(); 
        }

        if(menuObjectSelect.AllObjectsUnlocked)
        {
            TriggerWinEnd();
        }


        //TESTING
        //deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        //float fps = 1.0f / deltaTime;
        //print("fps: " + fps);
    }
}
