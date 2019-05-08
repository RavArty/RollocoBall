using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeed : MonoBehaviour {

	public Timer timer;
	private int previousTime = 0;
	Gradient gradient = new Gradient();

	[HideInInspector]
	public float increasedStartWidth;
	[HideInInspector]
	public float increasedEndWidth;

	// Use this for initialization
	void Start () {
		StartCoroutine (CheckTimer ());
	}

	IEnumerator  CheckTimer(){
		while (true) {

			if (timer.timeInSeconds - previousTime == 5) {
				//increase speed;
				if (RotateCircle.instance.speed < 10.1f) {
					RotateCircle.instance.speed += 0.5f;
					previousTime = timer.timeInSeconds;
				}
			}

			yield return new WaitForSeconds (1);
		}
	}

	IEnumerator  disableWhiteTrail(){
		yield return new WaitForSeconds (1);
		transform.GetChild (11).GetComponent<TrailRenderer> ().enabled = false;
		transform.GetChild (GetComponent<ChangeColor> ().enabledTrail).GetComponent<TrailRenderer> ().enabled = true;


	}

	IEnumerator increaseTrail(){

		float alpha = 1.0f;
		Gradient gradient2 = new Gradient();
		gradient2.SetKeys(
			new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 0.25f),
				new GradientColorKey(Color.white, 0.5f), new GradientColorKey(Color.white, 0.75f), new GradientColorKey(Color.white, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 0.25f), 
				new GradientAlphaKey(alpha, 0.5f), new GradientAlphaKey(0.9f, 0.75f),  new GradientAlphaKey(0.0f, 1.0f)}

		);

		transform.GetChild (GetComponent<ChangeColor> ().enabledTrail).GetComponent<TrailRenderer> ().colorGradient = gradient2;

		for(float t = 0; t < 1; t+=Time.deltaTime*10){

			transform.GetChild (GetComponent<ChangeColor> ().enabledTrail).GetComponent<TrailRenderer> ().endWidth = Mathf.Lerp(GetComponent<ChangeColor>().endWidth, increasedEndWidth, t);

			yield return null;

		}

		transform.GetChild (GetComponent<ChangeColor> ().enabledTrail).GetComponent<TrailRenderer> ().colorGradient = gradient2;

		StartCoroutine (startDecreasingTrail ());

	}

	IEnumerator startDecreasingTrail(){
		yield return new WaitForSeconds (0.5f);
		StartCoroutine (decreaseTrail ());
	}

	IEnumerator decreaseTrail(){

		for(float t = 0; t < 1; t+=Time.deltaTime*10){
			transform.GetChild (GetComponent<ChangeColor> ().enabledTrail).GetComponent<TrailRenderer> ().colorGradient = gradient;
			transform.GetChild (GetComponent<ChangeColor> ().enabledTrail).GetComponent<TrailRenderer> ().endWidth = Mathf.Lerp(increasedEndWidth, increasedEndWidth/3, t);
			yield return null;

		}

	}


}
