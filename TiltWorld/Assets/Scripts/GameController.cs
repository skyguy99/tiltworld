using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.VR;
//using GoogleVR;
//using Gvr.Internal;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //GvrViewer.Instance.VRModeEnabled

        //StartCoroutine(LoadDevice("None"));VRSettings.enabled = false;


    }
	
	// Update is called once per frame
	void Update () {
		
	}



    IEnumerator LoadDevice(string newDevice)
    {

        UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        UnityEngine.XR.XRSettings.enabled = true;
    }
}
