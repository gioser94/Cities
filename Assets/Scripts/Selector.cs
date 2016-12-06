using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Selector : MonoBehaviour {

	public Camera cam;
	public Canvas canvas;
	public static bool isSelecting = false;


	private Vector3 pos;




	SilhouetteManager sil;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (LevelManager.isCompleted || LevelManager.isCompletedFirstTime) 
		{
			this.gameObject.SetActive (false);
		}
		pos = Input.mousePosition;
		pos.z = 10.0f;
		pos = cam.ScreenToWorldPoint (pos);
		//Debug.Log ("Mouse Position: " + pos);  
		if (isSelecting) 
		{
			transform.position = pos; 
		}

	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		
		sil = coll.gameObject.GetComponent<SilhouetteManager> ();
		Click.currentID = sil.ID;
		Click.gameObj = coll.gameObject;
		coll.gameObject.GetComponent<FeedbackAnimation> ().status = 1;

	}

	void OnTriggerExit2D(Collider2D coll)
	{
		coll.gameObject.GetComponent<FeedbackAnimation> ().status = -1;
		sil = null;
		Click.currentID = -1;
		Click.gameObj = null;
	}
		
}  
