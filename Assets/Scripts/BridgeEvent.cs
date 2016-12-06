using UnityEngine;
using System.Collections;

public class BridgeEvent : MonoBehaviour {

	public float velocity;
	public Camera cam;
	public float padding;
	public MoveToCenterUp panel;

	private Transform people;
	private float target0, target1;
	private bool canMove = true;
	private bool toRight = false;
	private Animator anim;
	private float multiplierVelocity = 1.0f;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = this.GetComponent<AudioSource> ();
		anim = this.gameObject.GetComponent<Animator> ();
		people = this.transform;
		target0 = cam.orthographicSize * cam.aspect + padding;
		target1 = -target0;
		if (people.localScale.x == 1)
			toRight = true;

		multiplierVelocity = Random.Range (0.8f, 1.5f);
		anim.speed = multiplierVelocity;


	}
	
	// Update is called once per frame
	void Update () {
		if (panel.arrived && !panel.isChanging && canMove) 
		{
			if(toRight)
				people.localPosition = Vector3.MoveTowards (people.localPosition, new Vector2 (target0, people.localPosition.y),  70.0f*Time.deltaTime * velocity*multiplierVelocity);
			else
				people.localPosition = Vector3.MoveTowards (people.localPosition, new Vector2 (target1, people.localPosition.y),  70.0f*Time.deltaTime * velocity*multiplierVelocity);
		}

		if (!panel.arrived && panel.isChanging && !canMove) 
		{
			canMove = true;
			anim.speed = multiplierVelocity;

		}

		if (panel.arrived && !panel.isChanging && Input.GetMouseButtonDown (0)) 
		{
			if (canMove) 
			{
				anim.speed = 0.0f;
				canMove = false;
			} 
			else 
			{
				anim.speed = multiplierVelocity;
				canMove = true;
			}

		}

		if (toRight && people.localPosition.x >= target0 - 0.5f)
		{
			people.localScale = new Vector3 (-1.0f, 1.0f, 1.0f);
			multiplierVelocity = Random.Range (0.8f, 1.5f);
			anim.speed = multiplierVelocity;
			toRight = false;
		}
		if (!toRight && people.localPosition.x <= target1 + 0.5f)
		{
			people.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			multiplierVelocity = Random.Range (0.8f, 1.5f);
			anim.speed = multiplierVelocity;
			toRight = true;
		}
	}

	public void PlayAudio(AudioClip file)
	{
		if (!audio.isPlaying && panel.arrived && !panel.isChanging) {
			audio.clip = file;
			audio.Play ();
		}
	}
}
