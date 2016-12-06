using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	
	public static bool isCompleted = false;
	public bool isCanvasNotOnScreen;
	public int buildingsFeedback = 0;
	public int numBuildings = 4;
	public RectTransform rectTrans;
	public static bool isCompletedFirstTime;

	private Scene[] scene = null;
	private static bool friscoCompleted = false, rioCompleted = false, londonCompleted = false, shanghaiCompleted = false, veniceCompleted = false;
	private Scene currentScene;
	private Scene sceneLondon, sceneRio, sceneVenice, sceneShanghai, sceneFrisco;



	private Vector3 arrivo;

	void Awake()
	{
		Application.targetFrameRate = 30;
	}
	// Use this for initialization
	void Start () {
		buildingsFeedback = 0;
		arrivo = rectTrans.transform.localPosition - new Vector3 (0.0f, 200.0f, 0.0f);
		currentScene = SceneManager.GetActiveScene ();
		sceneLondon = SceneManager.GetSceneByName ("London");
		sceneRio = SceneManager.GetSceneByName ("Rio");
		sceneVenice = SceneManager.GetSceneByName ("Venice");
		sceneShanghai = SceneManager.GetSceneByName ("Shanghai");
		sceneFrisco = SceneManager.GetSceneByName ("Frisco");
		isCompleted = (sceneLondon == currentScene && londonCompleted) || (sceneRio == currentScene && rioCompleted) || (sceneVenice == currentScene && veniceCompleted) || (sceneShanghai == currentScene && shanghaiCompleted) || (sceneFrisco == currentScene && friscoCompleted);
		if (isCompleted) 
		{
			
			GameObject.FindGameObjectWithTag ("LowerButton").GetComponent<EventButton> ().changeBoolAnim();
			rectTrans.transform.localPosition = arrivo;

		}
	}
	
	// Update is called once per frame
	void Update () {


		if (SilhouetteManager.count == numBuildings && !isCompletedFirstTime && !isCompleted) 
		{
			if (currentScene.name == "London")
				londonCompleted = true;
			else if (currentScene.name == "Rio")
				rioCompleted = true;
			else if (currentScene.name == "Venice")
				veniceCompleted = true;
			else if (currentScene.name == "Shanghai")
				shanghaiCompleted = true;
			else if (currentScene.name == "Frisco")
				friscoCompleted = true;
			
			isCompletedFirstTime = true;

			GameObject.FindGameObjectWithTag ("UpperButton").GetComponent<EventButton> ().changeBoolAnim();
		}
		if (isCompletedFirstTime && !isCanvasNotOnScreen && buildingsFeedback == numBuildings) {
			
			rectTrans.transform.localPosition = Vector3.Lerp (rectTrans.transform.localPosition, arrivo, 10.0f*Time.deltaTime);
			if (rectTrans.transform.localPosition.y <= arrivo.y + 0.2f)
				isCanvasNotOnScreen = true;


		}



		if (Input.GetKey (KeyCode.Escape))
			ReturnMenu ();
	}

	public void ReturnMenu()
	{
		SilhouetteManager.count = 0;
		isCompleted = false;
		isCompletedFirstTime = false;
		SceneManager.LoadScene ("StartMenu");
	}
}
