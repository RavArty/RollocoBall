using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AppAdvisory.social;

public class Score : MonoBehaviour {

	public static Score instance;

	public int score = 0;


	void Awake(){
		if (instance == null) {
			instance = this;
		}else{
			Destroy (gameObject);
		}
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif

	}

	public void changeScore(int inScore){

		score = score + inScore;
		if (score > 999) {
			transform.localPosition = new Vector2 (transform.localPosition.x + 17.0f, transform.position.y);
		}
		gameObject.GetComponent<Text>().text = score.ToString();

	}

	public void saveScore(){
		TotalData.LoadTotalFromFile ();
		TotalData.total.lastScore = score;

		if (score > TotalData.total.bestScore) {
			TotalData.total.bestScore = score;
		}
		TotalData.total.totalScore += score;
		TotalData.SaveTotalToFile ();

	}
}
