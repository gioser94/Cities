using UnityEngine;
using System.Collections;

public class TaichiEvent : MonoBehaviour {

	public Animator otherAnim1, otherAnim2;
	public MoveToCenter panel;

	private Animator mainAnim;
	private int i;
	private bool canContinue;
	private bool firstTime;

	// Use this for initialization
	void Start () {
		mainAnim = this.gameObject.GetComponent<Animator> ();
		otherAnim1.SetInteger ("status", 0);
		otherAnim2.SetInteger ("status", 0);
		i = -1;
		canContinue = false;
		firstTime = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (panel.arrived && !firstTime) 
		{
			canContinue = true;
			firstTime = true;
		}
		if (canContinue) 
		{
			Invoke ("NextOtherAnim", 1.0f);
			canContinue = false;
		}
	}

	public void NextOtherAnim()
	{
		i++;
		otherAnim1.SetTrigger ("nextAnim");
		otherAnim1.SetInteger ("status", i);
		otherAnim2.SetTrigger ("nextAnim");
		otherAnim2.SetInteger ("status", i);
	}

	public void OnMouseDown()
	{
		if (!canContinue) 
		{
			NextMainAnim ();
		}
	
	}

	public void NextMainAnim()
	{
		mainAnim.SetTrigger ("nextAnim");
		canContinue = true;
	}
}
