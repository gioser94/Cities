using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject[] screens;
	public Animator anim;

	private int ID;
	// Use this for initialization
	void Start () {
		CheckScreen ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevel(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}

	public void AddID()
	{
		ID++;
		CheckScreen();
	}

	void CheckScreen()
	{
		for (int i = 0; i < screens.Length; i++) 
		{
			if (i == ID) {
				screens [i].SetActive (true);
			}
			else
				screens[i].SetActive(false);
		}
	}

	public void SetBoolAnim()
	{
		anim.SetBool ("canStart", true);
	}
}
