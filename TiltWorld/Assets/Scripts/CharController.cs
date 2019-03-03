using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float speed = 1f;

    void Start()
    {
        Input.multiTouchEnabled = false;
    }


    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f));
            transform.Translate(Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime) - transform.position);

        } 
        if(Input.touchCount > 0)
        {
            print("TOUCH!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject.FindObjectOfType<iOSHapticFeedback>().Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
    }
}
