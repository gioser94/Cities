using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IngrandimentoAnim : MonoBehaviour {

	private Click click;
	public Vector3 ratio;
	public float speed = 0.2f;

	private Vector3 initialRatio = new Vector3(1.0f, 1.0f, 1.0f);
	public Vector3 def = new Vector3 (3.0f, 3.0f, 3.0f);

	// Use this for initialization
	void Start () {
		click = this.gameObject.GetComponent<Click> ();


	}
	
	// Update is called once per frame
	void Update () {
		if (click.isSelected == 1 && !click.isPositioned) {
			this.gameObject.GetComponent<RectTransform> ().localScale = Vector3.Lerp (this.gameObject.GetComponent<RectTransform> ().localScale, ratio + def, speed*70.0f*Time.deltaTime);
		} 
		else if (click.isSelected == 2) {
			this.gameObject.GetComponent<RectTransform> ().localScale = Vector3.Lerp (this.gameObject.GetComponent<RectTransform> ().localScale, initialRatio, speed*70.0f*Time.deltaTime);
		} 
		else if (click.isSelected == 3) 
		{
			this.gameObject.GetComponent<RectTransform> ().localScale = Vector3.Lerp (this.gameObject.GetComponent<RectTransform> ().localScale, initialRatio, speed*70.0f*Time.deltaTime);
			this.gameObject.GetComponent<Image>().color = new Color(0.0f,0.0f,0.0f, 87.0f/255);
		}
	}
}
