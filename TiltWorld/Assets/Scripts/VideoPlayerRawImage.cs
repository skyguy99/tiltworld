   using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoPlayerRawImage : MonoBehaviour
{
    public Sprite[] frames;
    public int framesPerSecond = 60;
    public int index;
    public bool done;
    Image image;


    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void Reset()
    {
        done = false;
        index = 0;
    }
    void Update()
    {
        index = (int)(Time.time * framesPerSecond) % frames.Length;

        //this.GetComponent<Renderer>().material.mainTexture = frames[index];
        image.sprite = frames[index];


        //repeat
        if(index >= frames.Length-1)
        {
            //index = 0;
            done = true;
        }
    
    }

}
