using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FeedbackAnimation : MonoBehaviour {

	public Color color1, color2, color3;
	public Color colorRight1, colorRight2, colorRight3;
	public Color colorWrong1, colorWrong2, colorWrong3;
	public Vector3 defScale;
	public float speed = 1.0f;
	public GameObject second;
	public GameObject third;
	public int status = 0;
	public int correct = 0;
	public AnimationClip animaz;
	public LevelManager levMan;
	public int orderLeftToRight = 0;

	private GameObject thisGameObj;
	private Vector3 startMarker;
	private Vector3 middleMarker;
	private Vector3 endMarker;
	private Animator anim;
	private bool hasAdded = false;
	private bool waiting = false;
	private bool feedbackTriggered = false;
	private SilhouetteManager silMan;
	private bool areActive = true;



	// Use this for initialization
	void Start () {
		
		anim = this.GetComponent<Animator> ();
		thisGameObj = this.gameObject;
		startMarker = transform.localScale;
		middleMarker = transform.localScale + defScale;

		endMarker = transform.localScale + 2 * defScale;

	}

	// Update is called once per frame
	void Update () {


		if (!anim.GetBool ("isPositioned") && (!LevelManager.isCompleted || !LevelManager.isCompletedFirstTime)) {

			if (Click.currentID == -1 && correct == 0)
				status = -1;

			if (status > 0) {
				if (status == 1 && correct == 0) {
					this.gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (this.gameObject.GetComponent<SpriteRenderer> ().color, color1, 70*speed*Time.deltaTime);
					second.GetComponent<SpriteRenderer> ().color = color2;
					second.transform.localScale = Vector3.Lerp (second.transform.localScale, middleMarker, 70*speed*Time.deltaTime);
					third.GetComponent<SpriteRenderer> ().color = color3;
					third.transform.localScale = Vector3.Lerp (third.transform.localScale, endMarker, 0.5f * 70*speed*Time.deltaTime);
				}
			} else {
				if (status == -1 && correct == 0) {
					this.gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (this.gameObject.GetComponent<SpriteRenderer> ().color, Color.white, 70*speed*Time.deltaTime);
					second.transform.localScale = Vector3.Lerp (second.transform.localScale, startMarker, 70*speed*Time.deltaTime);
					third.transform.localScale = Vector3.Lerp (third.transform.localScale, startMarker, 0.5f * 70*speed*Time.deltaTime);
					if (second.transform.localScale.x < this.transform.localScale.x + 0.03f && third.transform.localScale.x < this.transform.localScale.x + 0.03f) {
						third.transform.localScale = startMarker;
						second.transform.localScale = startMarker;
						this.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
						third.GetComponent<SpriteRenderer> ().color = Color.white;
						second.GetComponent<SpriteRenderer> ().color = Color.white;
					}
				}
			}

			if (correct == 1) {
				this.gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (this.gameObject.GetComponent<SpriteRenderer> ().color, colorRight1, 2 * 70*speed*Time.deltaTime);
				second.GetComponent<SpriteRenderer> ().color = Color.Lerp (second.GetComponent<SpriteRenderer> ().color, colorRight2, 2 * 70*speed*Time.deltaTime);
				third.GetComponent<SpriteRenderer> ().color = Color.Lerp (third.GetComponent<SpriteRenderer> ().color, colorRight3, 2 * 70*speed*Time.deltaTime);
				second.transform.localScale = Vector3.Lerp (second.transform.localScale, startMarker, 2 * 70*speed*Time.deltaTime);
				third.transform.localScale = Vector3.Lerp (third.transform.localScale, startMarker, 1.5f * 70*speed*Time.deltaTime);
				if (second.transform.localScale.x < this.transform.localScale.x + 0.03f && third.transform.localScale.x < this.transform.localScale.x + 0.03f) {
					this.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
					anim.SetBool ("isPositioned", true);
					if (SilhouetteManager.count < levMan.numBuildings - 1) {
						SilhouetteManager.count++;
					} else if (SilhouetteManager.count == levMan.numBuildings - 1) 
					{
						if(!waiting)
						{
							waiting = true;
							Invoke ("AddOneToCount", 1.0f);
						}
					}

					//this.gameObject.GetComponent<SpriteRenderer> ().sprite = this.gameObject.GetComponent<SilhouetteManager>().sprite;
					Destroy (second);
					Destroy (third);
					areActive = false;

				}
			} 


			else if (correct == -1) {
				this.gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (this.gameObject.GetComponent<SpriteRenderer> ().color, colorWrong1, 70*speed*Time.deltaTime);
				second.GetComponent<SpriteRenderer> ().color = Color.Lerp (second.GetComponent<SpriteRenderer> ().color, colorWrong2, 70*speed*Time.deltaTime);
				third.GetComponent<SpriteRenderer> ().color = Color.Lerp (third.GetComponent<SpriteRenderer> ().color, colorWrong3, 70*speed*Time.deltaTime);
				second.transform.localScale = Vector3.Lerp (second.transform.localScale, startMarker, 70*speed*Time.deltaTime);
				third.transform.localScale = Vector3.Lerp (third.transform.localScale, startMarker, 0.5f * 70*speed*Time.deltaTime);
				if (second.transform.localScale.x < this.transform.localScale.x + 0.03f && third.transform.localScale.x < this.transform.localScale.x + 0.03f) {
					third.transform.localScale = startMarker;
					second.transform.localScale = startMarker;
					this.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
					third.GetComponent<SpriteRenderer> ().color = Color.white;
					second.GetComponent<SpriteRenderer> ().color = Color.white;
					correct = 0;
				}
			}
		}

		if ((LevelManager.isCompleted || LevelManager.isCompletedFirstTime) && !hasAdded) 
		{


			if (orderLeftToRight == levMan.buildingsFeedback && !feedbackTriggered) 
			{
				feedbackTriggered = true;
				Invoke ("AddToFeed", 0.1f);
			}


		}

	}



	public void AddOneToCount()
	{
		SilhouetteManager.count++;

	}

	public void AddToFeed()
	{
		levMan.buildingsFeedback++;
		if(anim.GetBool("isPositioned"))
			anim.SetBool ("isPositioned", true);
		anim.Play (animaz.name, 0, 0.0f);
		hasAdded = true;
		if (areActive) 
		{
			Destroy (second);
			second = null;
			Destroy (third);
			third = null;
			areActive = false;
		}

	}

	public void OnMouseDown()
	{
		Debug.Log ("Pointer down");
		if (anim.GetBool ("isPositioned") || LevelManager.isCompleted) 
		{
			anim.SetTrigger ("isTouched");
		}
	}
}
