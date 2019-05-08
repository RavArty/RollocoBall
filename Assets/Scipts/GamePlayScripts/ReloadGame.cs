using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour {

	public static ReloadGame instance;

	void Awake(){
		if (instance == null) {
			instance = this;
		}else{
			Destroy (gameObject);
		}
	}

	public void reloadGame(){
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	public void endGame(){
		StartCoroutine (endGameIEnumerator ());
	}

	IEnumerator reloadGameIEnumerator(){
		yield return new WaitForSeconds (2);

		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	IEnumerator endGameIEnumerator(){
		yield return new WaitForSeconds (1);
		GetComponent<ButtonSelector> ().MainMenu ();
	}
}
