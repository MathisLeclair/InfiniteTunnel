using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class MainMenu : MonoBehaviour {


 	#if UNITY_IOS
    private string gameId = "2844356";
    #elif UNITY_ANDROID
    private string gameId = "2844357";
	#elif UNITY_EDITOR
	private string gameId = "2844357";
	#elif UNITY_STANDALONE
	private string gameId = null;
    #endif

	/*Id for android
	since the define don't seem to work
	
	private string gameId = "2844357";*/

	[SerializeField]Text highScore;	
	void Start () {
		Advertisement.Initialize(gameId);
		if (Advertisement.isInitialized)
			Debug.Log("Ad ready");
	}
	
	// Update is called once per frame
	void Update () {
		highScore.text = PlayerPrefs.GetInt("highscore", 666).ToString();
	}

	public void loadEndlessMode(){
		SceneManager.LoadScene("infiniteMode");
	}

	public void Ad(){
		Debug.Log("trying to show ad");
		Debug.Log("isready? = " + Advertisement.IsReady());
		if (Advertisement.IsReady()){
			Debug.Log("Showing ad");
			Advertisement.Show("rewardedVideo");
		}
	}

	public void loadLevel(int lvl){
		SceneManager.LoadScene(1 + lvl);
	}

	void HandleAdResult(ShowResult result){}
}
