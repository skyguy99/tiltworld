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

    TextMeshPro textHeadline;
    TextMeshPro textSubtitle;
    public bool menuIsUp;

    float deltaTime;
    Transform target;

    public Image instructionsTitle;
    public Image instructionsGesture;
    bool playingInstructions;

    public Frames[] instructionsTitleFrames;
    public Frames[] instructionsGestureFrames;

    [System.Serializable]
    public class Frames
    {
        public Sprite[] frames;
    }
    int instructionIndex;


    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        ObjectText = GameObject.FindGameObjectWithTag("ObjectText").transform;
        ObjectText.GetComponent<LineRenderer>().startWidth = 0.2f;
        ObjectText.GetComponent<LineRenderer>().endWidth = 0.2f;

        textHeadline = ObjectText.GetChild(0).GetComponent<TextMeshPro>();
        textSubtitle = ObjectText.GetChild(1).GetComponent<TextMeshPro>();

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
    public void ShowObjectText(Transform obj, string headline, string subtext)
    {
        ObjectText.gameObject.SetActive(true);
        target = obj;
        textHeadline.text = headline.ToUpper();
        textSubtitle.text = subtext;
       
        StartCoroutine(BackToNoObject());
    }

   
    void Update()
    {
        //ObjectText.gameObject.SetActive(target != null);
        if (target != null)
        {
            ObjectText.transform.position = new Vector3(target.transform.position.x, target.transform.position.y+3.5f, target.transform.position.z - 0.3f);

            ObjectText.GetComponent<LineRenderer>().SetPosition(0, new Vector3(ObjectText.transform.position.x, ObjectText.transform.position.y - 0.5f, ObjectText.transform.position.z));
            ObjectText.GetComponent<LineRenderer>().SetPosition(1, target.position);
            //make it face player?

        }

        if(playingInstructions)
        {
            PlayerPrefs.SetInt("playedOnce", 1);

            instructionsTitle.GetComponent<VideoPlayerRawImage>().frames = instructionsGestureFrames[instructionIndex].frames;
            instructionsGesture.GetComponent<VideoPlayerRawImage>().frames = instructionsGestureFrames[instructionIndex].frames;
            instructionsGesture.enabled = !instructionsTitle.enabled;

            if (instructionsTitle.GetComponent<VideoPlayerRawImage>().done)
            {
                instructionIndex++;
                instructionsTitle.GetComponent<VideoPlayerRawImage>().Reset();
                instructionsTitle.enabled = false;
            }
            if(instructionsGesture.GetComponent<VideoPlayerRawImage>().done)
            {
                instructionsGesture.GetComponent<VideoPlayerRawImage>().Reset();
                instructionsTitle.enabled = true;
            }

            if(instructionIndex == 2)
            {
                playingInstructions = false;
            }

        }


        //TESTING
        //deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        //float fps = 1.0f / deltaTime;
        //print("fps: " + fps);
    }
}
