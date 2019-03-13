using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas canvas;
    public Transform objectShowContainer;
    public Transform overallObjectsContainer;
    PlayerController player;

    float deltaTime;

    void Start()
    {
        foreach (Transform child in objectShowContainer)
        {
            child.gameObject.SetActive(false);
        }
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    public void ShowObjectCreated(string name)
    {
        foreach(Transform child in objectShowContainer)
        {
            if(child.GetComponent<ObjectController>().objName == name)
            {
                //print("SHOW " + name);
                child.gameObject.SetActive(true);
                overallObjectsContainer.GetComponent<Animator>().SetBool("up", true);

            }
        }

        StartCoroutine(BackToNoObject());
    }

    IEnumerator BackToNoObject()
    {
        yield return new WaitForSeconds(7f);
        overallObjectsContainer.GetComponent<Animator>().SetBool("up", false);

        yield return new WaitForSeconds(1f);
        foreach (Transform child in objectShowContainer)
        {
            child.gameObject.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in objectShowContainer)
        {
            if (child.GetComponent<ObjectController>().objName == name)
            {
                transform.rotation = new Quaternion(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w);
            }

        }

        //TESTING
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        print("fps: " + fps);
    }
}
