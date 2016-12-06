using UnityEngine;
using System.Collections;

public class MovementCharachter : MonoBehaviour {

	public float bounceHeight = 0.0f;
	public float velocity = 0.0f;
	public int dir = 1;
	public MoveToCenter panel;

	private float initialY;
	private Animator charAnim;
	private Animator umbrellaAnim;


	// Use this for initialization
	void Start () {
		umbrellaAnim = this.GetComponentInChildren<Animator> ();
		initialY = transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3 (transform.localPosition.x + dir * velocity*Time.deltaTime*10, Mathf.Abs (bounceHeight*Mathf.Sin (transform.localPosition.x)) + initialY, transform.localPosition.z);
	}

	public void OnMouseDown()
	{
		if (!umbrellaAnim.GetBool ("isOpened"))
		{
			panel.umbrellaOpened++;
			AudioSource audio = this.GetComponent<AudioSource>();
			audio.Play();
		}
		
		umbrellaAnim.SetBool ("isOpened", true);


	}

	public void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Dx" || coll.gameObject.tag == "Sx") 
		{
			dir = dir * (-1);
			this.gameObject.GetComponent<SpriteRenderer> ().flipX = !this.gameObject.GetComponent<SpriteRenderer> ().flipX;
			//this.gameObject.GetComponentInChildren<SpriteRenderer> ().flipX = !this.gameObject.GetComponentInChildren<SpriteRenderer> ().flipX;
			transform.GetChild (0).transform.localPosition = new Vector3(-transform.GetChild (0).transform.localPosition.x, transform.GetChild (0).transform.localPosition.y, transform.GetChild (0).transform.localPosition.z);
			transform.GetChild (0).transform.localEulerAngles = -transform.GetChild (0).transform.localEulerAngles;
			//this.GetComponentInChildren<Transform> ().localPosition = -this.GetComponentInChildren<Transform> ().localPosition;
			//this.GetComponentInChildren<Transform> ().localEulerAngles = -this.GetComponentInChildren<Transform> ().localEulerAngles;

		}
	}
}
