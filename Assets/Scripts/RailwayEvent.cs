using UnityEngine;
using System.Collections;

public class RailwayEvent : MonoBehaviour {

	public Camera cam;
	public GameObject panel;
	public float offset;
	public float speed;
	public float trainVelocity;
	public MoveToCenter railRoad;
	public MoveToCenterUp train;
	public float padding;
	public Animator anim;
	public AudioSource audioS;

	private Collider2D coll;
	private bool isInteractable = false;
	private bool giveVelocity = false;
	private bool isVisible = true;
	private float screenSize;


	// Use this for initialization
	void Start () {
		screenSize = cam.orthographicSize * cam.aspect;
		coll = this.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButton(0) && isInteractable && isVisible ) 
		{
			if( coll == Physics2D.OverlapPoint( (Vector2)cam.ScreenToWorldPoint(Input.mousePosition)))
				transform.position = Vector3.Lerp (transform.position, new Vector2 (cam.ScreenToWorldPoint (Input.mousePosition).x, Mathf.Tan (Mathf.Deg2Rad * 21.0f) * (cam.ScreenToWorldPoint (Input.mousePosition).x) + offset), speed*Time.deltaTime*70.0f);

		}
		if(Input.touchCount > 0 && isInteractable && isVisible)
		{
			if(coll == Physics2D.OverlapPoint( (Vector2)cam.ScreenToWorldPoint(Input.GetTouch(0).position)))
				transform.position = Vector3.Lerp (transform.position, new Vector2 (cam.ScreenToWorldPoint (Input.mousePosition).x, Mathf.Tan (Mathf.Deg2Rad * 21.0f) * (cam.ScreenToWorldPoint (Input.mousePosition).x) + offset), speed*Time.deltaTime*70.0f);
		}	

		if(Input.GetMouseButtonUp(0) && isInteractable && isVisible)
		{
			if( coll == Physics2D.OverlapPoint( (Vector2)cam.ScreenToWorldPoint(Input.mousePosition)))
				giveVelocity = true;
		}

		if(transform.localPosition.x <= 1.0f && transform.localPosition.x >= -1.0f && !audioS.isPlaying)
		{
			audioS.Play();
			anim.SetTrigger("inCenter");
		}

		if (giveVelocity && isInteractable) 
		{
			transform.localPosition = Vector2.MoveTowards (transform.localPosition, new Vector2 (45.4f, 15.0f), trainVelocity * Time.deltaTime * 70.0f);
		}

		if (railRoad.arrived && train.arrived && !railRoad.isChanging && !train.isChanging) {
			if(!isInteractable)
				ChangeInteractionTrue ();
		} else 
		{
			if(isInteractable)
				ChangeInteractionFalse ();
		}

		if(transform.localPosition.x >= screenSize + padding && isVisible)
		{
			isVisible = false;
			giveVelocity = false;
			transform.localPosition = new Vector2 (-(screenSize + padding), Mathf.Tan (Mathf.Deg2Rad * 21.0f) * (-(screenSize + padding))-2.3f);
		}
		if (!isVisible && isInteractable) 
		{
			transform.localPosition = Vector2.MoveTowards (transform.localPosition, new Vector2 (-11.3f, -6.8f), 2*trainVelocity * Time.deltaTime * 70.0f);
			if (transform.localPosition.x >= -12f) 
			{
				isVisible = true;
			}
		}

	}

	public void ChangeInteractionTrue()
	{
		isInteractable = true;
	}

	public void ChangeInteractionFalse()
	{
		isInteractable = false;
	}
}
