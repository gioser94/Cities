using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;   

public class Click : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IPointerUpHandler {

	public static int currentID = -1;
	public static GameObject gameObj = null;
	public int f, g;
	public ScrollRect scroll;
	public int scrollObjID;
	public FeedbackAnimation feedBack;
	public int isSelected = 0;
	public bool isPositioned = false;
	public float vel;


	private Vector3 mousePos;
	private Vector3 objPos;
	private int drag = -1;
	private int onSceneObjID;


	void Update () {
		
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		
		// TODO da cambiare con Touch!!!
		if (Input.mousePosition.y > mousePos.y + f && (Input.mousePosition.x <= mousePos.x + g && Input.mousePosition.x >= mousePos.x - g))
		{
			if (!isPositioned) {
				drag = 1;
				isSelected = 1;
			} else
				drag = 0;
		}
		else
			drag = 0;
	}

	public void OnPointerDown (PointerEventData eventData) 
	{
		Selector.isSelecting = true;
		mousePos = Input.mousePosition; //TODO Da cambiare con Touch!!!
		objPos = transform.localPosition;

	}

	public void OnDrag (PointerEventData eventData)
	{
		mousePos = Input.mousePosition;	//TODO Da cambiare con touch!!!
		if (Input.GetMouseButton (0)) {   //TODO Da cambiare con touch!!!
			if (drag == 1 && !isPositioned)
				this.transform.position = Input.mousePosition; //TODO Da cambiare con touch!!!
			else if(drag == 0)
			{
				Selector.isSelecting = false;
				
				if (scroll.horizontalNormalizedPosition >= -0.04f && scroll.horizontalNormalizedPosition <= 1.04f) 
				{
					scroll.horizontalNormalizedPosition -= 15*Time.deltaTime* vel*eventData.delta.x / ((float)Screen.width);
				}

			} 
		}
			
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (drag == 1 && scrollObjID != currentID) 
		{
			drag = -1;
			isSelected = 2;

			if (gameObj != null) 
			{
				
				gameObj.GetComponent<FeedbackAnimation> ().correct = -1;
			}
			transform.localPosition = objPos;
			currentID = -1;

			Selector.isSelecting = false;
		}
		else if (drag == 1 && scrollObjID == currentID) {
			
			isPositioned = true;
			isSelected = 3;

			if (gameObj != null) 
			{
				
				gameObj.GetComponent<FeedbackAnimation> ().correct = 1;
			}
			currentID = -1;
			transform.localPosition = objPos;
			drag = -1;
			Selector.isSelecting = false;
		} else
			Selector.isSelecting = false;
	}


}
