using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
	void Start(){
		iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));

        //iTween.MoveTo(gameObject, iTween.Hash("position", Vector3(-1,2,gameObject.transform.position.z), "time",1.5, "easetype", "easeinoutquart"));
	}
}

