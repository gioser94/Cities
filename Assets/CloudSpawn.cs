using UnityEngine;
using System.Collections;

public class CloudSpawn : MonoBehaviour {

	public Animator anim;
	public Collider2D coll;
	public float speed;

	private bool positioning = false;
	private Vector2 finalPosition;
	private Vector2 outScreen;

	// Use this for initialization
	void Awake () {
		int num = Random.Range (-1, 2);
		if (num > 0)
			num = 1;
		else
			num = -1;

		finalPosition = this.transform.localPosition;
		this.transform.localPosition =  new Vector2(num*(Camera.main.orthographicSize * Camera.main.aspect + 10.0f), this.transform.localPosition.y);
		outScreen = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && coll == Physics2D.OverlapPoint ((Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition)) && anim.GetBool("isPositioned")) 
		{
			positioning = !positioning;
		}

		if (!positioning) {
			this.transform.localPosition = Vector2.MoveTowards (this.transform.localPosition, outScreen, speed * 10 * Time.deltaTime);
		} 
		else 
		{
			this.transform.localPosition = Vector2.MoveTowards (this.transform.localPosition, finalPosition, speed * 10 * Time.deltaTime);
		}
	}
}
