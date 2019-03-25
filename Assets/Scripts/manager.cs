using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class manager : MonoBehaviour {

	[SerializeField] GameObject mainUI;
	[SerializeField] GameObject looseScreen;
	[SerializeField] Text scoreTxt;
	[SerializeField] Text scoreTxtLoose;
	[SerializeField] Camera cam = null;
	[SerializeField] GameObject tutorial;

	float timer;
	int score;
	int diff;
	float speed = 20f;
	float speedModifier = 0f;

	[SerializeField] bool debugBypass = false;

	private static bool created = false;

    #if UNITY_IOS
    private string gameId = "2844356";
    #elif UNITY_ANDROID
    private string gameId = "2844357";
	#else
	private string gameId = null;
    #endif

	private void Start()
	{
		Advertisement.Initialize(gameId);
		if (!created){
			DontDestroyOnLoad(this.gameObject);
			created = true;
		}
		else
			Destroy(this.gameObject);
		if (debugBypass || PlayerPrefs.GetInt("tuto", 0) == 0){
			mainUI.SetActive(false);
			tutorial.SetActive(true);
			PlayerPrefs.SetInt("tuto", 1);
			speed = 0f;
		}
		PlayerPrefs.SetInt("Level0", 1);
	}

	private void Update()
	{
		//Debug.Log("end = " + end + "/ tuto = " + PlayerPrefs.GetInt("tuto", 0) + "/bypass = " + debugBypass + "/scene=" + SceneManager.GetActiveScene().name);
		if (end || PlayerPrefs.GetInt("tuto", 0) == 0 || debugBypass || SceneManager.GetActiveScene().name == "MainMenu")
			{ scoreTxt.gameObject.SetActive(false) ; return ;}
		if(!cam){ cam = GameObject.Find("Main Camera").GetComponent<Camera>(); } // if level just start find that stupid camera
		timer += Time.deltaTime;
		score = Mathf.RoundToInt(timer * 1000);
		scoreTxt.text = score.ToString();
		diff = Mathf.RoundToInt(timer / 20);
		if (speedModifier > 0 ){
			speedModifier -= Time.deltaTime * 2;
			cam.fieldOfView = cam.fieldOfView > 60 ? cam.fieldOfView - 0.1f : 60 ;
		} else if (speedModifier <= 0) {
			speedModifier = 0 ;
			cam.fieldOfView = 60;
		}
		speed += Time.deltaTime;
		if (!end && !_pause && !scoreTxt.gameObject.activeSelf)
			scoreTxt.gameObject.SetActive(true);
	}

	public void reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		speed = 20f;
		end = false;	
	}

	public void mainMenu(){
		endLevelScreen.SetActive(false);
		mainUI.SetActive(true);
		looseScreen.SetActive(false);
		SceneManager.LoadScene(0);
		speed = 20f;
		end = false;
	}

	bool _pause = false;
	public void pause(){
		_pause = !_pause;
		Time.timeScale = _pause ? 0f : 1f;
	}

	public bool end = false;
	public void loose(){
		if (score > PlayerPrefs.GetInt("highscore", 0) && SceneManager.GetActiveScene().name == "infiniteMode")
			PlayerPrefs.SetInt("highscore", score);
		cam = null;
		end = true;
		speed = 0f;
		speedModifier = 0f;
		mainUI.SetActive(false);
		looseScreen.SetActive(true);
		scoreTxtLoose.text = scoreTxt.text;
		timer = 0;
	}

	public int getdifficulties() {
		return diff;
	}

	public int getTimeInt() {
		return Mathf.FloorToInt(timer);
	}

	public float getSpeed() {
		return speed + speedModifier;
	}

	public void hyperdrive(float speedMod) {
		speedModifier = speedMod + 40f + diff / 2 * 10;
		cam.fieldOfView = 70f;
	}

	[SerializeField] GameObject endLevelScreen;
	public void endLevel(){
		timer = 0f;
		mainUI.SetActive(false);
		endLevelScreen.SetActive(true);
		PlayerPrefs.SetInt("Level" + SceneManager.GetActiveScene().name, 1);
		Debug.Log("Validate level" + SceneManager.GetActiveScene().name);
		Debug.Log("PlayerPref value check : " + PlayerPrefs.GetInt("Level" + SceneManager.GetActiveScene().name));
	}

	public void nextLevel(){
		mainUI.SetActive(true);
		endLevelScreen.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	[SerializeField]List<GameObject> LevelButtonList;
	public void levelCheck(){
		/*Debug.Log("button0 =" + LevelButtonList[0]);
		if (LevelButtonList[0] == null ){
			for (int i = 0; i < GameObject.Find("Levels").transform.childCount; i++)
			{
				LevelButtonList[i] = GameObject.Find("Levels").transform.GetChild(i).gameObject;
			}
		} */
		for (int i = 0; i < LevelButtonList.Count; i++)
		{
			if (PlayerPrefs.GetInt("Level" + i, 0) == 0)
			{
				LevelButtonList[i].GetComponent<Image>().fillCenter = true;
				LevelButtonList[i].GetComponent<Image>().color = new Color(173, 173, 173, 169);
				LevelButtonList[i].GetComponent<Button>().interactable = false;
			}
			else{
				LevelButtonList[i].GetComponent<Image>().fillCenter = false;
				LevelButtonList[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
				LevelButtonList[i].GetComponent<Button>().interactable = true;
			}
		}
	}
}
