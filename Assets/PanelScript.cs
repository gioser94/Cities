using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelScript : MonoBehaviour {

	public GameObject[] positioner;
	public int[] sceneID;

	List<int> usedValues = new List<int>();
	List<int> numTo10 = new List<int>();
	private GameObject[] selected;
	private GameObject mainPanel;
	private string currentString;


	// Use this for initialization
	void Start() {
		selected = new GameObject[10];

		mainPanel = GameObject.FindGameObjectWithTag ("MainPanel");
		usedValues.Add (sceneID [0]);
		usedValues.Add (sceneID [1]);
		usedValues.Add (sceneID [2]);
		usedValues.Add (sceneID [3]);
		usedValues.Add (UniqueRandomInt(0, 24));
		usedValues.Add (UniqueRandomInt(0, 24));
		usedValues.Add (UniqueRandomInt(0, 24));
		usedValues.Add (UniqueRandomInt(0, 24));
		usedValues.Add (UniqueRandomInt(0, 24));
		usedValues.Add (UniqueRandomInt(0, 24));

		for(int i = 0; i < 10; i++)
			numTo10.Add (UniqueInt ());



		for (int i = 0; i < 10; i++) 
		{
			currentString = "Image" + (usedValues [i].ToString ());
			selected [i] = GameObject.FindGameObjectWithTag (currentString);
			selected [i].transform.SetParent(positioner [numTo10[i]].transform);
			selected [i].gameObject.transform.localPosition = Vector2.zero;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int UniqueRandomInt(int min, int max)
	{
		int val = Random.Range(min, max);
		while(usedValues.Contains(val))
		{
			val = Random.Range(min, max);
		}

		return val;
	}

	public int UniqueInt()
	{
		int val = Random.Range(0, 10);
		while(numTo10.Contains(val))
		{
			val = Random.Range(0, 10);
		}

		return val;
	}
}
