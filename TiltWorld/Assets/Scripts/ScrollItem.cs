using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollItem : MonoBehaviour
{

    public int worldNum;
    public string savedDate;
    public Text dateText;

    Animator anim;
    public Transform pointer;
    public World selectedWorld;

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
        anim = GetComponent<Animator>();
        pointer = GameObject.FindGameObjectWithTag("DirectionalScroll").GetComponent<Image>().transform;
    }

    public void EnterWorld()
    {
        print("LOADING: " + selectedWorld.id);
        PlayerPrefs.SetInt("loadedWorld", selectedWorld.id);
      
        SceneManager.LoadScene("TiltWorldScene");
    }

    // Update is called once per frame 
    void Update()
    {

        float size = GetIconSize(pointer.position, transform.position);
        transform.localScale = new Vector3(size, size, size);

        dateText.text = savedDate;
    }
}
