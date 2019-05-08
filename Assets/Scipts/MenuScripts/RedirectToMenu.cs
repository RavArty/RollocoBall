using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class RedirectToMenu : MonoBehaviour {

	void Awake(){
		if (FB.IsInitialized) {
			FB.ActivateApp();
		} else {
			//Handle FB.Init
			FB.Init( () => {
				FB.ActivateApp();
			});
		}

	}

	void Start(){
		SceneManager.LoadScene ("Menu");	
	}
}
