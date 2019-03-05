using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManagerSelector : MonoBehaviour {
	
	private Canvas canvas;
	private bool beforeProjection = false;
	private float tmpNearClipPlane = -1;
    private int cameraXSpan = 0;
	private int cameraYSpan = 0;
    private int cameraZSpan = 0; 
	
	void Start(){
        cameraXSpan = transform.Find("Panel").Find("GameController").GetComponent<ScrollRectSnap>().cameraXSpan;
		cameraYSpan = transform.Find ("Panel").Find ("GameController").GetComponent<ScrollRectSnap> ().cameraYSpan;
        cameraZSpan = transform.Find("Panel").Find("GameController").GetComponent<ScrollRectSnap>().cameraZSpan;
		canvas = gameObject.GetComponent<Canvas>();
		canvas.enabled = false;
		transform.Find ("Panel").gameObject.SetActive (false);
        clickCanvas();
	}

	public void clickCanvas(){
		canvas.enabled = !canvas.enabled;
		GameObject panel = transform.Find ("Panel").gameObject;
        if (cameraXSpan > 0 || cameraYSpan > 0 || cameraZSpan > 0) {
			Vector3 camPos = Camera.main.transform.position;
			if (canvas.enabled) {
                Camera.main.transform.position = new Vector3(camPos.x += cameraXSpan, camPos.y += cameraYSpan, camPos.z += cameraZSpan);
			} else {
                Camera.main.transform.position = new Vector3(camPos.x -= cameraXSpan, camPos.y -= cameraYSpan, camPos.z -= cameraZSpan);
			}
		}

		panel.SetActive (canvas.enabled);

		//Change the render mode if needed
		if(canvas.enabled && Camera.main.orthographic == false){
			Camera.main.orthographic = true;
			beforeProjection = true;
		}
		if(beforeProjection && canvas.enabled == false){
			Camera.main.orthographic = false;
			beforeProjection = false;
		}


		//Store and restore the near plane
		if(canvas.enabled && Camera.main.nearClipPlane > -200){
			tmpNearClipPlane = Camera.main.nearClipPlane;
			Camera.main.nearClipPlane = -200;
		}
		if(canvas.enabled == false && tmpNearClipPlane != -1){
			Camera.main.nearClipPlane = tmpNearClipPlane;
		}
	}

    private void Update()
    {
        if(Input.GetKey("space"))
        {
            clickCanvas();
        }
    }
}