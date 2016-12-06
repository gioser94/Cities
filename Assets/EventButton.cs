using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventButton : MonoBehaviour {

	public MoveToCenter panel;
	public MoveToCenterUp panelUp;
	public bool useUp;
	public bool mustSee = false;
	public float velocity;

	private bool isMoving  = false, isHidden = true;
	private Vector2 seenPosition;
	private Vector2 unSeenPosition;
	private Button button;
	private RectTransform buttonRect;


	// Use this for initialization
	void Start () {
		seenPosition = new Vector2 (84.0f, 254.0f);
		buttonRect = this.GetComponent<RectTransform>();
		unSeenPosition = buttonRect.anchoredPosition;
		button = this.GetComponent<Button>();
		if(useUp)
			panel = null;
		else
			panelUp = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(mustSee && isHidden && isMoving)
		{
			
			buttonRect.anchoredPosition = Vector2.MoveTowards(buttonRect.anchoredPosition, seenPosition, 100*Time.deltaTime*velocity);
			button.interactable = false;
			if(buttonRect.anchoredPosition.x >= seenPosition.x - 2.0f)
			{
				buttonRect.anchoredPosition = seenPosition;
				isMoving = false;
				isHidden = false;

			}
		}
		if(!mustSee && !isHidden && isMoving)
		{
			buttonRect.anchoredPosition = Vector2.MoveTowards(buttonRect.anchoredPosition, unSeenPosition, 100*Time.deltaTime*velocity);
			button.interactable = false;
			if(buttonRect.anchoredPosition.x <= unSeenPosition.x + 2.0f)
			{
				buttonRect.anchoredPosition = unSeenPosition;
				isMoving = false;
				isHidden = true;

			}
		}
		if (panelUp != null) {
			if (!isMoving && !panelUp.isChanging && !isHidden)
				button.interactable = true;
		} else
		{
			if (!isMoving && !panel.isChanging && !isHidden)
				button.interactable = true;
		}

	}

	public void changeBoolAnim()
	{
		if(!isMoving)
		{
			if(useUp && !panelUp.isChanging){
				mustSee = !mustSee;
				isMoving = true;
			}
			if(!useUp && !panel.isChanging){
				mustSee = !mustSee;
				isMoving = true;
			}

		}

	}

}
