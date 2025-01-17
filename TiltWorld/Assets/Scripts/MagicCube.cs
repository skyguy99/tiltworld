using UnityEngine;

public class MagicCube : MonoBehaviour
{
    public bool IsRed;

	private void Start()
	{
		//IsRed = GameDataController.GetState(this);
		UpdateColor();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
			{
				IsRed = !IsRed;
				UpdateColor();
				//GameDataController.SetState(this, IsRed);
			}
		}
	}

	private void UpdateColor()
	{
		//GetComponent<MeshRenderer>().material.color = IsRed ? Color.red : Color.green;
	}
}