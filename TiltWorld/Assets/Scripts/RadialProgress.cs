using UnityEngine;
using UnityEngine.UI;

public class RadialProgress : MonoBehaviour {
	public GameObject LoadingText;
	public Text ProgressIndicator;
	public Image LoadingBar;
	public float currentValue;
	public float speed;

    public Transform circle2;
    bool circleIsUp;

	// Use this for initialization
	void Start () {



    }

    public void ToggleSelectCircle(Vector3 pos, bool unwind)
    {
        if(!circleIsUp)
        {
            circleIsUp = unwind;
            currentValue = 100;
            GetComponentInParent<Animator>().SetBool("circleIn", true);
            transform.gameObject.transform.position = pos;
        }
        GetComponentInParent<Animator>().SetBool("circleIn", true);

    }

    public void ToggleIncompatibleCircle(Vector3 pos)
    {
        GetComponentInParent<Animator>().SetBool("xIn", true);
        transform.gameObject.transform.position = pos;
    }

    public void ToggleSelectCircleDown()
    {
        GetComponentInParent<Animator>().SetBool("circleIn", false);
        GetComponentInParent<Animator>().SetBool("xIn", false);
        circleIsUp = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(circleIsUp)
        {
            if (currentValue > 0)
            {
                currentValue -= 69f * Time.deltaTime;

            }
            else
            {
                GetComponentInParent<Animator>().SetBool("circleIn", false);
                circleIsUp = false;
            }
        }
        circle2.localEulerAngles = new Vector3(0, 0, -(currentValue * 0.01f * 360));

		LoadingBar.fillAmount = currentValue / 100;
	}
}
