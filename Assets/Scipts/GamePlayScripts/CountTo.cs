using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountTo: MonoBehaviour {

//	public Text totalProgress;
	private int curValue;

	public void fireCountTo(int current, int target, float duration){
		StartCoroutine (CountToFrom (current, target, duration));
	}

	IEnumerator CountToFrom(int current, int target, float duration){
		for (float timer = 0; timer < duration; timer += Time.unscaledDeltaTime) {
			float progress = timer / duration;
			curValue = (int)Mathf.Lerp (current, target, progress);
			gameObject.GetComponent<Text>().text = curValue + "";
			yield return null;
		}
		gameObject.GetComponent<Text>().text = target + "";

	}
}
