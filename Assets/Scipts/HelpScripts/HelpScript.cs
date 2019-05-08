using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScript : MonoBehaviour {

	public static HelpScript instance;


	public Transform startPos;
	public GameObject circle;
	public GameObject lineDown;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else{
			Destroy (gameObject);
		}
	}
	public void startHelp(){
		circle.transform.position = startPos.position;
		lineDown.SetActive (true);
	}
}
