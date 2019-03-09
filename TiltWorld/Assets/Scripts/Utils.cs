using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

	public static void AddOrUpdateDictionaryEntry(Dictionary<GameObject, float> dict, GameObject key, float value)
	{
		if (dict.ContainsKey(key))
		{
			dict[key] = value;
		}
		else
		{
			dict.Add(key, value);
		}
	}

	public static float GetDistanceBetweenObjs(Transform objA, Transform objB)
	{
		Vector3 directionToTarget = objA.position - objB.position;
		float dSqrToTarget = directionToTarget.sqrMagnitude;

		return dSqrToTarget;
	}
	public static bool AnimatorIsPlaying(Animator animator){

		return animator.GetCurrentAnimatorStateInfo(0).length >
			animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	public static GameObject FindParentWithTag(GameObject childObject, string tag)
	{
		Transform t = childObject.transform;
		while (t.parent != null)
		{
			if (t.parent.tag == tag)
			{
				return t.parent.gameObject;
			}
			t = t.parent.transform;
		}
		return null; // Could not find a parent with given tag.
	}
	public static Transform GetClosestObj (GameObject[] objectsG, Transform thisObj)
	{
		Transform[] objects = new Transform[objectsG.Length];
		for(int i=0; i<objects.Length;i++)
		{
			objects [i] = objectsG [i].gameObject.transform;
		}
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = thisObj.position;

		foreach(Transform potentialTarget in objects)
		{
			Vector3 directionToTarget = potentialTarget.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if(dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget;
			}
		}

		return bestTarget;
	}
    public static bool CheckIsFacing(Transform transformA, Transform transformB)
    {
        return (Vector3.Angle(transformA.forward, transformB.position - transformA.position) < 10);
       
    }
	public static AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol, GameObject gameObj) { 
		AudioSource newAudio = gameObj.AddComponent<AudioSource>();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}
}
public static class TransformDeepChildExtension
{
	//Breadth-first search
	public static Transform FindDeepChild(this Transform aParent, string aName)
	{
		var result = aParent.Find(aName);
		if (result != null)
			return result;
		foreach(Transform child in aParent)
		{
			result = child.FindDeepChild(aName);
			if (result != null)
				return result;
		}
		return null;
	}
}


//------------------------------------------------------------------*****************************************

//Misc code
//
//var old_pos : float;
//function Start(){
//	old_pos = transform.position.x;
//}
//function Update(){
//	if(old_pos < transform.position.x){
//		print("moving right");
//	}
//	if(old_pos > transform.position.x){
//		print("moving left");
//	}
//	old_pos = transform.position.x;
//}
// Instantiate(prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
//mainCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
//	bool checkToSpawnPipe()
//	{
//		if (rightCanSpawnPipe > 2) {
//			return true;
//		}
//		return false;
//	}

//	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//	if (Physics.Raycast(ray, 100))
//		print("Hit something!");
//     if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 10)) - extends 10 units forward

// if(Physics.Raycast(this.transform.position, Vector3.right, out hit, 1f))
//	otherBlurCanvasAnim.SetInteger ("instructionsSeg", 10);
//	otherBlurCanvasAnim.SetBool ("folFound", false);
//
//	IEnumerator waitForTest()
//	{
//		yield return new WaitForSeconds (1f);
//		getAllObjectsAndKill ();
//	}
//		GameObject.FindGameObjectWithTag ("InGameHelp");
//		GameObject[] objects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
//		//GameObject[] objects = GameObject.FindGameObjectsWithTag("Block");
//		foreach (GameObject o in objects) {
