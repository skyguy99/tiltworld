using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollItem : MonoBehaviour
{

    public int worldNum = 0;
    public string savedDate;
    public Text dateText;
    public Text worldNumText;
    public Button btn;
    public Transform outline;

    Animator anim;
    public Transform pointer;

    public World selectedWorld;
    public bool isTheAddWorld;

    const float S = 1f; // The maximum size you want to get when closest
    const float D = 250.0f; // The distance where you start to scale
    const float E = 2.5f; // The distance where the object will not scale more (i.e. reached the maximum)

    float GetIconSize(Vector2 pointer, Vector2 icon)
    {
        // Get the value between 0 and 1 from the distance between
        float factor = Mathf.InverseLerp(D, E, Vector2.Distance(pointer, icon));

        // Return the interpolated value size depending on the distance
        return Mathf.Lerp(0.85f, S, factor);
    }

    // Start is called before the first frame update
    void Awake()
    {
        //PlayerPrefs.DeleteAll();

        anim = GetComponent<Animator>();
        pointer = GameObject.FindGameObjectWithTag("DirectionalScroll").GetComponent<Image>().transform;
        foreach(Transform t in transform.GetChild(0))
        {
            if(t.name == "outline")
            {
                outline = t;
            }
        }
    }

    public void EnterWorld()
    {
        if (selectedWorld.objects == null)
        {
            //NEW WORLD
            print("LOADING NEW: " + GameDataController.NumberOfWorlds); //1 more than last
            PlayerPrefs.SetInt("loadedWorld", GameDataController.NumberOfWorlds); //defaults to zero
            PlayerPrefs.Save();

        }
        else
        {
            print("LOADING: " + selectedWorld.id);
            PlayerPrefs.SetInt("loadedWorld", selectedWorld.id); //defaults to zero
            PlayerPrefs.Save();
        }

        LoadSceneNext();
    }

    void LoadSceneNext()
    {
        SceneManager.LoadScene("TiltWorldScene");
    }


    // Update is called once per frame 
    void Update()
    {

        float size = GetIconSize(pointer.position, transform.position);
        transform.localScale = new Vector3(size, size, size);

        if (dateText != null)
        {
            dateText.text = savedDate;
        }
        if(worldNumText != null)
        {
            
            worldNumText.text = (selectedWorld.objects == null) ? "new world" : selectedWorld.id.ToString();
        } 
        if(btn != null)
        {
            //btn.gameObject.SetActive(!(selectedWorld.objects == null));
        }
        outline.GetComponent<Image>().enabled = (!(selectedWorld.objects == null));
        outline.GetChild(0).GetComponent<Image>().enabled = (!(selectedWorld.objects == null));

    }
}
