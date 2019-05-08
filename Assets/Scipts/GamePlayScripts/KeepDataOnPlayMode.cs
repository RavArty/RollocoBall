// Keep data between scenes

using UnityEngine;
using System.Collections;
using Facebook.Unity;

[DisallowMultipleComponent]
public class KeepDataOnPlayMode : MonoBehaviour {

	public static KeepDataOnPlayMode instance = null;
//	[HideInInspector]
	public bool 	firstExecutioin = true;	
	public bool 	reloadedLevel = false;
	public int 		reloadedTimes = 0;
	public int 		reloadedTimesRewardVideo = 0;
	public bool		noAds;
	public bool 	isMusicOn;
	public bool 	isSoundOn;
	public int randomAds = 0;
	public int randomRewardAds = 0;
	public int chooseBGMenu = 0;

	[HideInInspector]
	public bool wordlScene = false;

	void Awake () {

		#if UNITY_IOS
		if (FB.IsInitialized) {
			FB.ActivateApp();
		} else {
			//Handle FB.Init
			FB.Init( () => {
				FB.ActivateApp();
			});
		}
		#endif

		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);

		}
		DontDestroyOnLoad (gameObject);
		#if UNITY_IOS
		Application.targetFrameRate = 60;
		#endif


	}
	void Start(){
		randomAds = generateIntAds ();
		randomRewardAds = generateIntRewardAds ();
		chooseBGMenu = Random.Range (1, 5);
	}

	public int generateIntAds(){
		int randomAd = Random.Range (3, 4);
		return randomAd;
	}

	public int generateIntRewardAds(){
		int randomAd = Random.Range (3, 6);
		return randomAd;
	}
}
