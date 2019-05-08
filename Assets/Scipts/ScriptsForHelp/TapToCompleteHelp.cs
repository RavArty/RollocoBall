using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToCompleteHelp : MonoBehaviour {

	public static TapToCompleteHelp instance;

	public GameObject activateObj;
	public GameObject barier;
	public GameObject glow;
	public GameObject barDestroyPrefab;
	public bool bar = true;


	void Awake(){
		FadeObject.instance.FadeOut (gameObject, 0);
		if (instance == null) {
			instance = this;
		}else{
			Destroy (gameObject);
		}
	}



	public void FadeInHand(){
		FadeObject.instance.FadeIn (gameObject, 0.2f);
	}
	void Update () {

		if (Input.GetButton ("Jump")) {
			if (gameObject) {
				if (TapToStart.instance.startForHelp) {
					activateObj.SetActive (true);
					StartCoroutine (Deactivate ());
				}
			}

		}
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

		if (Input.touchCount > 0) {
			//Store the first touch detected.
			Touch myTouch = Input.touches [0];

			//Check if the phase of that touch equals Began
			if (myTouch.phase == TouchPhase.Began  || myTouch.phase == TouchPhase.Stationary) {
				if (hit.collider != null) {
					if (hit.collider.gameObject.tag != "Button") {
						if (TapToStart.instance.startForHelp) {

							activateObj.SetActive (true);
							StartCoroutine (Deactivate ());
						}
					}
				} else {
					if (TapToStart.instance.startForHelp) {
						activateObj.SetActive (true);
						StartCoroutine (Deactivate ());
					}
				}
			}
		}

	}

	IEnumerator Deactivate(){
		if (bar) {
			bar = false;
			Score.instance.changeScore (1);
			GameObject destroyEffect = ObjectPool.current.GetObject (barDestroyPrefab);
			destroyEffect.transform.position = barier.transform.position;
			destroyEffect.SetActive (true);
			destroyEffect.GetComponent<PlaySound> ().playSound ();
			destroyEffect.GetComponent<Deactivate> ().startDeactivation ();
		}
		FadeObject.instance.FadeOut (barier, 0.1f);
		FadeObject.instance.FadeOut (glow, 0.1f);
		yield return new WaitForSeconds (0.3f);
		TapToStart.instance.start = true;
		FadeObject.instance.FadeOut (gameObject, 0.2f);
		StartCoroutine (Deactivate2 ());
	}

	IEnumerator Deactivate2(){
		yield return new WaitForSeconds (0.3f);
		gameObject.SetActive (false);
	}
}
