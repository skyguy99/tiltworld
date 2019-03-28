using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas canvas;
    Animator canvasAnim;
    PlayerController player;
    Transform ObjectText;

    public TextMeshPro textHeadline;
    public TextMeshPro textSubtitle;
    public Transform circle;

    float deltaTime;
    Transform target;

    public Image uiImage;
    bool playingInstructions;


    VideoPlayer videoPlayer;
    public Image menu;

    Camera mainCamera;
    public Transform menuObject;

    float t = 0f;
    bool menuIn;


    //public static string screenshotFilename = "screenshotName.png";
    //string pathToImage = Application.persistentDataPath + "/" + screenshotFilename;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        ObjectText = GameObject.FindGameObjectWithTag("ObjectText").transform;

        target = null;

        ObjectText.gameObject.SetActive(false);

        PlayerPrefs.DeleteAll(); //temp
        if(!PlayerPrefs.HasKey("playedOnce"))
        {
            playingInstructions = true;
        }

        canvasAnim = canvas.GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        menuObject.gameObject.SetActive(menuIn);
        canvas.enabled = !menuIn;

    }

    public void ToggleMenu()
    {
        print("menu");

        //Application.CaptureScreenshot(screenshotFilename);
        canvasAnim.SetBool("buttonTouch", true);
        StartCoroutine(ChangeMenuIn());
    }

    IEnumerator BackToNoObject()
    {
        yield return new WaitForSeconds(10f);
        target = null;
        ObjectText.gameObject.SetActive(false);

    }
    public void ShowObjectText(Transform obj, string headline, string subtext, bool showCircle)
    {
        ObjectText.gameObject.SetActive(true);
        target = obj;
        textHeadline.text = headline;
        textSubtitle.text = subtext;

        circle.gameObject.SetActive(showCircle);
        StartCoroutine(BackToNoObject());

        print("add to ui menu");

    }

    IEnumerator ChangeMenuIn()
    {
        menuIn = !menuIn;
        t = 0f;


        yield return new WaitForSeconds(1f);
        menuObject.gameObject.SetActive(menuIn);
        canvas.enabled = !menuIn;
        canvasAnim.SetBool("buttonTouch", false);
    }

    void Update()
    {
    
            Color col = menu.color;


            col.a = menuIn ? Mathf.Lerp(0f, 1f, t) : Mathf.Lerp(1f, 0f, t);

            menu.color = col;
            t += 6f* Time.deltaTime;

        if (target != null)
        {
            ObjectText.transform.position = new Vector3(target.transform.position.x, target.transform.position.y+3.8f, target.transform.position.z - 0.3f);


            circle.transform.position = target.transform.position;

        }

        if(playingInstructions)
        {

            PlayerPrefs.SetInt("playedOnce", 1);


        }


        //TESTING
        //deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        //float fps = 1.0f / deltaTime;
        //print("fps: " + fps);
    }
}
