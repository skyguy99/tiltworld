using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas canvas;
    PlayerController player;
    Transform ObjectText;

    public TextMeshPro textHeadline;
    public TextMeshPro textSubtitle;
    public Transform circle;

    float deltaTime;
    Transform target;

    public Image uiImage;
    bool playingInstructions;

    public Frames[] uiFrames;

    [System.Serializable] //So I can swap in other ui later
    public class Frames
    {
        public Sprite[] frames;
    }


    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        ObjectText = GameObject.FindGameObjectWithTag("ObjectText").transform;
        ObjectText.GetComponent<LineRenderer>().startWidth = 0.2f;
        ObjectText.GetComponent<LineRenderer>().endWidth = 0.2f;


        textHeadline = ObjectText.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        textSubtitle = ObjectText.GetChild(0).GetChild(1).GetComponent<TextMeshPro>();

        target = null;
        ObjectText.gameObject.SetActive(false);

        PlayerPrefs.DeleteAll(); //temp
        if(!PlayerPrefs.HasKey("playedOnce"))
        {
            playingInstructions = true;
        }

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

    }



    void Update()
    {
        //ObjectText.gameObject.SetActive(target != null);

       
        if (target != null)
        {
            ObjectText.transform.position = new Vector3(target.transform.position.x, target.transform.position.y+3.8f, target.transform.position.z - 0.3f);

            ObjectText.GetComponent<LineRenderer>().SetPosition(0, new Vector3(ObjectText.transform.position.x, ObjectText.transform.position.y - 0.5f, ObjectText.transform.position.z));
            ObjectText.GetComponent<LineRenderer>().SetPosition(1, target.position);

            circle.transform.position = target.transform.position;

        }

        if(playingInstructions)
        {

            PlayerPrefs.SetInt("playedOnce", 1);

            if(uiImage.GetComponent<VideoPlayerRawImage>().index == 31 || uiImage.GetComponent<VideoPlayerRawImage>().index == 234 || uiImage.GetComponent<VideoPlayerRawImage>().index == 498)
            {

                uiImage.GetComponent<VideoPlayerRawImage>().pause = true;
                if (Input.touchCount > 0 || Input.GetKey("space"))
                {
                    print("unpause");
                    uiImage.GetComponent<VideoPlayerRawImage>().pause = false;
                }

            }

        }


        //TESTING
        //deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        //float fps = 1.0f / deltaTime;
        //print("fps: " + fps);
    }
}
