using UnityEngine;
using UnityEngine.UI;

public class RadialProgress : MonoBehaviour {
	public GameObject LoadingText;
	public Text ProgressIndicator;
	public Image LoadingBar;
	public float currentValue;
	public float speed;

    public Transform circle2;

	// Use this for initialization
	void Start () {



    }
	
	// Update is called once per frame
	void Update () {

        //circle2.transform.position = new Vector3(Mathf.Sin(currentValue * 6.28f), Mathf.Cos(currentValue * 6.28f), 0f) * 6;
        //float circleAngle = currentValue * 360;

        //print(currentValue*0.01f*360);
        circle2.localEulerAngles = new Vector3(0, 0, -(currentValue * 0.01f * 360));

  //      if (currentValue < 100) {
		//	currentValue += speed * Time.deltaTime;
		//	//ProgressIndicator.text = ((int)currentValue).ToString () + "%";
		//	//LoadingText.SetActive (true);

		//}

		LoadingBar.fillAmount = currentValue / 100;
	}
}
