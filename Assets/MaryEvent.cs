using UnityEngine;
using System.Collections;

public class MaryEvent : MonoBehaviour {

	public Camera cam;
	public Transform hingeRigid;
	public float vel;
	public float lerpVel;
	public MoveToCenter panel;

	private Vector3 difference;
	private Rigidbody2D rigidBody2D;
	private Vector2 previousPos;
	private bool getPos = true;
	private HingeJoint2D hinge;
	private bool isVisible = false;
	private AudioSource audio;



	// Use this for initialization
	void Start () {
		rigidBody2D = this.gameObject.GetComponent<Rigidbody2D> ();
		//rigidBody2D.velocity = new Vector2 (0.0f, vel);
		hinge = this.gameObject.GetComponent<HingeJoint2D> ();
		audio = this.GetComponent<AudioSource>(); 
	}
	
	// Update is called once per frame
	void Update () {
		if (panel.umbrellaOpened >= 7 && !isVisible) 
		{
			MakeVisible ();
		}
		if (this.transform.localPosition.y >= cam.orthographicSize + 5.0f) 
		{
			MakeInvisible ();
		}

	}

	public void OnMouseDrag()
	{
		if (this.gameObject.GetComponent<Animator> ().GetBool ("isTouched")) 
		{
			hingeRigid.gameObject.GetComponent<Rigidbody2D> ().MovePosition ((Vector2)cam.ScreenToWorldPoint (Input.mousePosition));
			hinge.connectedAnchor = new Vector2 (0.0f, 0.0f);
			rigidBody2D.gravityScale = -4.0f;
		}

	}

	public void OnMouseDown()
	{
		if (!this.gameObject.GetComponent<Animator> ().GetBool ("isTouched"))
			this.gameObject.GetComponent<Animator> ().SetBool ("isTouched", true);
		else 
		{
			hinge.anchor = (Vector2)transform.InverseTransformPoint (cam.ScreenToWorldPoint (Input.mousePosition));
			rigidBody2D.velocity = new Vector2 (0.0f, 0.0f);
		}
	}

	public void OnMouseUp()
	{
		rigidBody2D.gravityScale = -1.0f;
	}



	public void MakeVisible()
	{
		if(!audio.isPlaying)
		{
			audio.Play();
		}
		this.gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp (this.gameObject.GetComponent<SpriteRenderer> ().color, new Color (Color.white.r, Color.white.g, Color.white.b, 1.0f), lerpVel*70.0f*Time.deltaTime);
		if (this.gameObject.GetComponent<SpriteRenderer> ().color.a >= 0.99f) 
		{
			this.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
			isVisible = true;

		}
	}

	public void ChangeKinematic()
	{

		rigidBody2D.isKinematic = !rigidBody2D.isKinematic;
		hingeRigid.gameObject.GetComponent<Rigidbody2D> ().isKinematic = !hingeRigid.gameObject.GetComponent<Rigidbody2D> ().isKinematic;
	}

	void MakeInvisible()
	{
		if(isVisible)
		{
			ChangeKinematic ();
			this.gameObject.GetComponent<SpriteRenderer> ().color = new Color (Color.white.r, Color.white.g, Color.white.b, 0.0f);
			hingeRigid.localPosition = new Vector3 (0.91f, -1.52f, 0.0f);
			this.transform.localPosition = Vector3.zero;
			this.transform.eulerAngles = new Vector3 (0.0f, 0.0f, 0.0f);
			hinge.anchor = new Vector2 (0.91f, -1.52f);
			this.gameObject.GetComponent<Animator> ().SetBool ("isTouched", false);
			this.gameObject.GetComponent<Animator> ().Play ("Mary_idle");
			isVisible = false;
		}

	}
}
