using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayObjects : MonoBehaviour {

	public static ReplayObjects instance;

	public Text score;
	public Text bestScore;
	public Text totalScore;
	public GameObject replayButton;
	public GameObject rewardVideoButton;


	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}

	void Start () {
		TotalData.LoadTotalFromFile ();
		score.text = TotalData.total.lastScore.ToString ();
		bestScore.text = TotalData.total.bestScore.ToString ();
		totalScore.text = TotalData.total.totalScore.ToString ();

		if (KeepDataOnPlayMode.instance.randomRewardAds != KeepDataOnPlayMode.instance.reloadedTimesRewardVideo) {
			afterAds ();
		} else {
			KeepDataOnPlayMode.instance.reloadedTimesRewardVideo = 0;
			KeepDataOnPlayMode.instance.randomRewardAds = KeepDataOnPlayMode.instance.generateIntRewardAds ();
		}
	}

	public void afterAds(){
		if(KeepDataOnPlayMode.instance.noAds){
			rewardVideoButton.SetActive (false);
			replayButton.transform.localPosition = new Vector2(0, replayButton.transform.localPosition.y);
	
		}else{
	
			rewardVideoButton.SetActive (false);
			replayButton.transform.localPosition = new Vector2(replayButton.transform.localPosition.x, 0);
		}

	}


	

}
