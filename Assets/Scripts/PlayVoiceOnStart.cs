using UnityEngine;
using System.Collections;

public class PlayVoiceOnStart : MonoBehaviour {

	public AudioClip[] audio;
	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<AudioSource> ().clip = audio [0];
		this.gameObject.GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
