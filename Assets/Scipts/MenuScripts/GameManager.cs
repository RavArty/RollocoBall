using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	private AsyncOperation async;
	[HideInInspector]
	public float progressBar = 0;

	void Awake(){
		if (instance == null) {
			instance = this;
		}else{
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	
	}

	public void loadGame(string sceneName){
		StartCoroutine ("LoadSceneAsync", sceneName);
	}

	IEnumerator LoadSceneAsync(string sceneName){
	
		if (!string.IsNullOrEmpty (sceneName)) {
	
			async = SceneManager.LoadSceneAsync(sceneName);

			while (!async.isDone) {
				progressBar = async.progress;
				if(GameObject.Find("progressBar")){
					GameObject.Find("progressBar").GetComponent<Image>().fillAmount = progressBar;
				}
				
				yield return 0;

			}
		}


	}

	public void loadMainMenu(string sceneName){
		StartCoroutine ("LoadSceneAsync", sceneName);


	}

	public void pauseGame(){
		if (Time.timeScale > 0) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;

		}
	}
		

	public void selectPlayer(GameObject player){
	
		if (TotalData.total.totalScore >= player.GetComponent<Player> ().price) {
			TotalData.total.totalScore -= player.GetComponent<Player> ().price;
            TotalData.total.playerID = player.GetComponent<Player> ().ID;
		}
	}
}
