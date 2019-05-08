using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;

public class SelectMenu : MonoBehaviour {

	public static SelectMenu instance;


	public GameObject[] menus;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else{
			Destroy (gameObject);
		}
	}

	void Start(){
	}

	public void selectMenu(GameObject menu){
		for (int i = 0; i < menus.Length; i++) {
			menus [i].SetActive (false);
		}
		menu.SetActive (true);
		if (menu.name == "Main") {
			ChooseLine.instance.startLine ();
		}
	}
}
