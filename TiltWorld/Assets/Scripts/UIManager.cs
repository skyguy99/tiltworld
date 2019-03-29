﻿using System.Collections;
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


    //public static string screenshotFilename = "screenshotName.png";
    //string pathToImage = Application.persistentDataPath + "/" + screenshotFilename;

    void Awake()
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

        menuObjectSelect = GameObject.FindObjectOfType<MenuObjectSelect>();

    }

    public void ToggleMenu()
    {
        print("menu");


        //ScreenCapture.CaptureScreenshot(screenshotFilename);
        canvasAnim.SetBool("buttonTouch", true);
        StartCoroutine(ChangeMenuIn());
    }

    IEnumerator BackToNoObject()
    {
        yield return new WaitForSeconds(10f);
        target = null;
        ObjectText.gameObject.SetActive(false);
        objCircle.ToggleSelectCircleDown();

    }
    public void ShowObjectText(Transform obj, string headline, string subtext, bool showCircle)
    {
        ObjectText.gameObject.SetActive(true);
        target = obj;
        textHeadline.text = headline;
        textSubtitle.text = subtext;

        StartCoroutine(BackToNoObject());
        objCircle.ToggleSelectCircle(Camera.main.WorldToScreenPoint(obj.transform.position), false);
        menuObjectSelect.objectsThatWereCombined.Add(headline.ToUpper());

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


            objCircle.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);

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
