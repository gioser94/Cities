using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {

	public GameObject[] parts;
	public Camera cam;
	public float padding;
	public float velocity;	
	
	private AudioSource audio;
	private Vector2 posLeft, posRight;
	private int status;
	private bool isVisible = false;

	// Use this for initialization
	void Start () {
		status = 0;
		posLeft = new Vector2( -(cam.orthographicSize * cam.aspect + padding), this.transform.localPosition.y);
		posRight = new Vector2(cam.orthographicSize * cam.aspect + padding, this.transform.localPosition.y);
		if(this.GetComponent<AudioSource>() != null)
			audio = this.GetComponent<AudioSource>();
		else
			audio = null;

		parts[0].GetComponent<Animator>().speed = 0.0f;

		this.GetComponent<SpriteRenderer>().color = new Color (1.0f,1.0f,1.0f, 0.0f);
		for (int i= 0; i < parts.Length; i++)
			parts[i].GetComponent<SpriteRenderer>().color = new Color (1.0f,1.0f,1.0f, 0.0f);
	}

	// Update is called once per frame
	void Update () {
		if( isVisible && transform.localScale.x == 1.0f)
		{
			if(status == 1)
				transform.localPosition = Vector2.MoveTowards(transform.localPosition, posRight, velocity*Time.deltaTime*70.0f);
			else if(status == -1)
				status = 1;

			if(transform.localPosition.x >= posRight.x - 2.0f){
				transform.localScale = new Vector3 (-1.0f, 1.0f, 1.0f);

			}
		}
		if(isVisible && transform.localScale.x == -1.0f)
		{
			if(status == -1)
				transform.localPosition = Vector2.MoveTowards(transform.localPosition, posLeft, velocity*Time.deltaTime*70.0f);
			else if(status == 1)
				status = -1;

			if(transform.localPosition.x <= posLeft.x + 1.0f)
				transform.localScale = new Vector2 (1.0f, 1.0f);
		}

		if(!isVisible && (LevelManager.isCompleted || LevelManager.isCompletedFirstTime))
		{
			
			MakeVisible();
		}



	}

	public void OnMouseDown()
	{
		
		if(isVisible && status == 0){
			status = (int)transform.localScale.x;
			parts[0].GetComponent<Animator>().speed = 1.0f;
			if(audio != null)
				if(!audio.isPlaying)
					audio.Play();
		}
		else if(isVisible && status != 0)
		{
			status = 0;
			parts[0].GetComponent<Animator>().speed = 0.0f;
		}
	}

	public void MakeVisible()
	{
		this.GetComponent<SpriteRenderer>().color = Color.Lerp(this.GetComponent<SpriteRenderer>().color, Color.white, 7.0f*Time.deltaTime);
		for(int i = 0; i< parts.Length; i++)
			parts[i].GetComponent<SpriteRenderer>().color = Color.Lerp(parts[i].GetComponent<SpriteRenderer>().color, Color.white, 7.0f*Time.deltaTime);

		if(this.GetComponent<SpriteRenderer>().color.a >= 0.99f)
		{
			this.GetComponent<SpriteRenderer>().color = Color.white;
			for(int i = 0; i < parts.Length; i++)
				parts[i].GetComponent<SpriteRenderer>().color = Color.white;
			isVisible = true;
		}
	}
}
