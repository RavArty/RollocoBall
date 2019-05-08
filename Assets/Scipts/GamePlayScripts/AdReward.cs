using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdReward : MonoBehaviour {

	public GameObject totalScore;
	private int mediumScore;

	void Awake(){
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}
	public void ShowRewardedAd()
	{
		MusicSound.instance.ClickButtonSound ();
	//	Debug.Log ("ShowRewardedAd");
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
            // Reward for watching ads    
			Debug.Log ("The ad was successfully shown.");
			TotalData.LoadTotalFromFile ();
			mediumScore = TotalData.total.totalScore;
			TotalData.total.totalScore += 20;
			totalScore.GetComponent<CountTo> ().fireCountTo (mediumScore, TotalData.total.totalScore, 1.0f);
			TotalData.SaveTotalToFile ();
			ReplayObjects.instance.afterAds ();
		
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}
