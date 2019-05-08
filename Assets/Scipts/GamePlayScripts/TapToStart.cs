using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToStart : MonoBehaviour {

	public static TapToStart instance;

	public Timer timer;
	public GameObject activateObj;
	[HideInInspector]
	public bool start = false;
	[HideInInspector]
	public bool startForHelp = false;


	void Awake(){
		if (instance == null) {
			instance = this;
		}else{
			Destroy (gameObject);
		}
	}


	void Update () {
		
		if (Input.GetButton ("Jump")) {
			if (gameObject) {
				activateObj.SetActive (true);
				StartCoroutine (Deactivate ());
			}
		//	Deactivate ();

		}
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

		if (Input.touchCount > 0) {
			//Store the first touch detected.
			Touch myTouch = Input.touches [0];

			//Check if the phase of that touch equals Began
			if (myTouch.phase == TouchPhase.Began  || myTouch.phase == TouchPhase.Stationary) {
				if (hit.collider != null) {
					if (hit.collider.gameObject.tag != "Button") {

						activateObj.SetActive (true);
						StartCoroutine (Deactivate ());
					}
				} else {
					activateObj.SetActive (true);
					StartCoroutine (Deactivate ());
				}
			}
		}

	}

	IEnumerator Deactivate(){
		yield return new WaitForSeconds (0.3f);
		start = true;
		timer.StartTimer ();
		FadeObject.instance.FadeOut (gameObject, 0.2f);
		StartCoroutine (Deactivate2 ());
	}

	IEnumerator Deactivate2(){
		yield return new WaitForSeconds (0.3f);
		gameObject.SetActive (false);
	}



}
