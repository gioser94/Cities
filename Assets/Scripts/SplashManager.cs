using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour {

	public Image logoImage, panelImage;

	private Animator anim;
	private float time;
	private bool canFade = false, canStart = true;


	// Use this for initialization
	void Start () {
		anim = logoImage.gameObject.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (canStart) 
		{
			Invoke ("StartLogo", 0.3f);
			Invoke ("Fade", 3.0f);
			canStart = false;
		}

		if (canFade) 
		{
			panelImage.color = Color.Lerp (panelImage.color, Color.white, 5f*Time.deltaTime);
		}

		if (panelImage.color == Color.white) {
			LoadMenuScene ();
		}
	}

	void StartLogo()
	{
		if(!anim.GetBool("startLogo"))
			anim.SetBool ("startLogo", true);


	}

	void Fade()
	{
		canFade = true;
	}

	void LoadMenuScene()
	{
		SceneManager.LoadScene ("StartMenu");
	}

}
