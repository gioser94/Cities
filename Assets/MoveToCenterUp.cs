using UnityEngine;
using System.Collections;

public class MoveToCenterUp : MonoBehaviour {
	public LevelManager levMan;
	public int umbrellaOpened;
	public bool arrived = false;
	public int moveDirection = 0;
	public bool isChanging = false;

	private bool firstTime = true;


	private Vector3 hiddenPosition;

	// Use this for initialization
	void Start () {
		arrived = false;
		hiddenPosition = transform.localPosition;
	}

	// Update is called once per frame
	void Update () {
		
		if (levMan.isCanvasNotOnScreen && firstTime) 
		{
			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3(0.0f,0.0f,10.0f), 10.0f*Time.deltaTime);
			isChanging = true;

			if (transform.localPosition.y <= 0.01f && !arrived) 
			{
				transform.localPosition = new Vector3 (0.0f, 0.0f, 10.0f);
				moveDirection = 0;
				arrived = true;
				firstTime = false;
				isChanging = false;
			}
		}
		if (moveDirection == 1) 
		{
			transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3(0.0f,0.0f,10.0f), 10.0f*Time.deltaTime);
			isChanging = true;
			if (transform.localPosition.y <= 0.01f) 
			{
				transform.localPosition = new Vector3 (0.0f, 0.0f, 10.0f);
				moveDirection = 0;
				arrived = true;
				isChanging = false;
			}
		}
		if (moveDirection == -1) 
		{
			transform.localPosition = Vector3.Lerp (transform.localPosition, hiddenPosition, 10.0f*Time.deltaTime);
			isChanging = true;
			if (transform.localPosition.y >= hiddenPosition.y - 0.01f) 
			{
				transform.localPosition = hiddenPosition;
				moveDirection = 0;
				arrived = false;
				isChanging = false;
			}
		}


	}

	public void SwitchState()
	{
		if (!arrived && !isChanging)
			moveDirection = 1;
		if (arrived && !isChanging) 
		{
			moveDirection = -1;
		}
	}
}
