using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ScrollSnaps;
using UnityEngine.UI;

public class MenuObjectSelect : MonoBehaviour
{

    public Transform pointer;
    public Transform contentBox;

    bool scaledIn;
  
    /* Constants */
    const float S = 1.5f; // The maximum size you want to get when closest
    const float D = 350.0f; // The distance where you start to scale
    const float E = 2.5f; // The distance where the object will not scale more (i.e. reached the maximum)

    float GetIconSize(Vector2 pointer, Vector2 icon)
    {
        // Get the value between 0 and 1 from the distance between
        float factor = Mathf.InverseLerp(D, E, Vector2.Distance(pointer, icon));

        // Return the interpolated value size depending on the distance
        return Mathf.Lerp(0.1f, S, factor);
    }
    OmniDirectionalScrollSnap omni;

    // Start is called before the first frame update
    void Start()
    {
        omni = GetComponent<OmniDirectionalScrollSnap>();
        pointer = GetComponent<Image>().transform;

    }


    // Update is called once per frame
    void Update()
    {

            foreach (Transform t in contentBox)
            {
                float size = GetIconSize(pointer.position, t.position);
                t.localScale = new Vector3(size, size, size);

            if (t == omni.closestItem.transform)
            {
                t.GetComponent<MenuObject>().anim.SetBool("menuIn", true);
            } else {
                t.GetComponent<MenuObject>().anim.SetBool("menuIn", false);
            }

            }

       
        //float size = GetIconSize(pointer.position, omni.closestItem.position);
        //omni.closestItem.transform.localScale = new Vector2(size, size);
    }
}
