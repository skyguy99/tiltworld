using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuObject : MonoBehaviour
{
	public string objName;
	Text headline;
    TextMeshPro subtext;
    PlayerController player;
    public Animator anim;
    public ObjectController myObject;
    Transform container;
    public Transform platContainer;
    UIManager uIManager;
   
    public string[] objectsThatProduceThis;

    Vector3 rot = new Vector3(17.1f, -129f, 21f);

    MenuObjectSelect menuObjectSelect;
    public Material[] originMats;
    public bool isLocked = true;

    bool hasSet;


    Dictionary<string, Vector3> customSizes = new Dictionary<string, Vector3>();

    void Awake()
    {
        uIManager = GameObject.FindObjectOfType<UIManager>();
        menuObjectSelect = GameObject.FindObjectOfType<MenuObjectSelect>();
        anim = GetComponent<Animator>();
        player = GameObject.FindObjectOfType<PlayerController>();
        platContainer = Instantiate(menuObjectSelect.platform);
        platContainer.parent = transform;
        platContainer.localPosition = menuObjectSelect.platform.localPosition;
        platContainer.rotation = menuObjectSelect.platform.rotation;

        foreach (Transform t in transform)
        {
            if (t.name == "Title")
            {
                headline = t.GetComponent<Text>();
            }
            else if (t.name == "Subtitle")
            {
                subtext = t.GetComponent<TextMeshPro>();
            }
            else if (t.name == "Object")
            {
                container = t;

            } else {
                platContainer = t;
            }

        }


        //Custom rots
        customSizes["arrow"] = new Vector3(41929.72f, 41929.72f, 41929.72f);
    }

    public void SetObject(ObjectController o)
    {

        myObject = o;
        Transform c = c = container.GetChild(0);
        //SET object item

        GameObject go = container.GetChild(0).gameObject;
        if (!hasSet)
        {
            hasSet = true;
            go = Instantiate(o.gameObject, c.position, Quaternion.identity);
            go.transform.parent = container;
            go.SetActive(true);
            go.transform.localScale = c.localScale;

            Destroy(go.GetComponent<Rigidbody>());
            if(go.GetComponent<VideoPlayerMaterial>() != null)
            {
                Destroy(go.GetComponent<VideoPlayerMaterial>());
            }
            Destroy(go.GetComponent<ObjectController>());
            Destroy(c.gameObject);
            go.transform.eulerAngles = rot;
        }

        for (int i = 0; i < platContainer.childCount; i++)
        {
            platContainer.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            platContainer.GetChild(i).gameObject.SetActive(i == myObject.world);
        }


        objName = o.objName;
        headline.text = isLocked ? "??" : objName.ToUpper();

        if (CheckForCustomSize(objName) != Vector3.zero)
        {
            go.transform.localScale = CheckForCustomSize(objName);
        }

        if (player.combineObjects.ContainsKey(objName))
        {
         
            string text = "(" + player.combineObjects[objName][0].ToUpper() + " + " + player.combineObjects[objName][1].ToUpper() + ")";
            subtext.text = text;
        }

        subtext.transform.localPosition = new Vector3(subtext.transform.localPosition.x, subtext.transform.localPosition.y, -135f);

        go.transform.eulerAngles = rot;

        //Materials --------------

        if(originMats.Length == 0)
        {
            originMats = go.transform.GetComponent<Renderer>().materials;
        }
        for (int i = 0; i < platContainer.childCount; i++)
        {
            platContainer.GetChild(i).GetComponent<Renderer>().material = isLocked ? menuObjectSelect.blackMat : menuObjectSelect.platform.GetChild(i).GetComponent<Renderer>().material;
        }
    }

     Vector3 CheckForCustomSize(string id)
    {
        if(customSizes.ContainsKey(id))
        {
            return customSizes[id];
        }
        return Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        foreach(ObjectController o in uIManager.objectsThatWereCombined)
        {
            if(o.objName == myObject.objName)
            {
                isLocked = false;
            }
        }

        for (int i = 0; i < platContainer.childCount; i++)
        { 
            platContainer.GetChild(i).GetComponent<Renderer>().material = isLocked ? menuObjectSelect.blackMat : menuObjectSelect.platMats[myObject.world];
        }

        headline.text = isLocked ? "??" : objName.ToUpper();

        if (container.transform.childCount > 0)
        {
            Material[] allMats = container.GetChild(0).gameObject.GetComponent<Renderer>().materials;
            for (int i = 0; i < allMats.Length; i++)
            {
               if(originMats.Length == 0)
                {
                    allMats[i] = isLocked ? menuObjectSelect.blackMat : menuObjectSelect.platMats[myObject.world];
                } else
                {
                    allMats[i] = isLocked ? menuObjectSelect.blackMat : originMats[i];
                }
                //allMats[i] = originMats[i];
            }
            container.GetChild(0).gameObject.GetComponent<Renderer>().materials = allMats;
       
            if (container.GetChild(0).eulerAngles != rot)
            {
                container.GetChild(0).eulerAngles = rot;
            }
        }

    }
}
