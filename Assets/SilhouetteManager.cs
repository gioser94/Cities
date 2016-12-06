using UnityEngine;
using System.Collections;

public class SilhouetteManager : MonoBehaviour {

	public AudioClip[] feedbackAudioClip;
	public AudioSource[] allOtherAudio;

	public int ID;
	public static int count;
	private AudioSource audioSource;
	private Animator anim;
	private bool canPlayAudio = true;
	private FeedbackAnimation feed;
	private bool playVoice = true;

	bool correct;

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
		audioSource = this.gameObject.GetComponent<AudioSource> ();
		feed = this.gameObject.GetComponent<FeedbackAnimation>();
		count = 0;
	}

	// Update is called once per frame
	void Update () {
		if (anim.GetBool ("isPositioned") && canPlayAudio) {
			MakeOtherStop ();
			audioSource.clip = feedbackAudioClip [0];
			audioSource.Play();
			canPlayAudio = false;
		} 
		else if (anim.GetBool ("isPositioned") && !canPlayAudio) 
		{
			if (!audioSource.isPlaying && playVoice) 
			{
				
				audioSource.clip = feedbackAudioClip [3];
				audioSource.Play ();
				playVoice = false;
			}
		}

		else if (!anim.GetBool ("isPositioned")) 
		{
			if(feed.correct == -1 && canPlayAudio)
			{
				
				audioSource.clip = feedbackAudioClip[1];
				audioSource.Play();
				canPlayAudio = false;
			}
		}

		if (feed.correct == 0 && !audioSource.isPlaying) 
		{
			canPlayAudio = true;
		}
		if(LevelManager.isCompleted)
		{
			playVoice = false;
		}

	}

	void OnMouseDown()
	{
		
		if ( !audioSource.isPlaying && feedbackAudioClip[2] != null && !playVoice) 
		{
			audioSource.clip = feedbackAudioClip [2];
			audioSource.Play ();
		}		
	}

	void MakeOtherStop()
	{
		allOtherAudio [0].Stop ();
		allOtherAudio [1].Stop ();
		allOtherAudio [2].Stop ();

	}


}
