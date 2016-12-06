using UnityEngine;
using System.Collections;

public class PositionByRes : MonoBehaviour {

	public Camera cam;

	// Use this for initialization
	void Awake () {
		if(this.gameObject.tag == "Dx")
			transform.position = new Vector3 (transform.position.x + cam.orthographicSize * cam.aspect +8, transform.position.y, transform.position.z);
		else if(this.gameObject.tag == "Sx")
			transform.position = new Vector3 (transform.position.x - cam.orthographicSize * cam.aspect -8, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
