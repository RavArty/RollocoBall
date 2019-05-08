using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ShowBannerAd : MonoBehaviour {
	public static ShowBannerAd instance;
	[HideInInspector]
	public BannerView bannerView;

	#if UNITY_EDITOR
	string adUnitId = "unused";
	#elif UNITY_ANDROID
	string adUnitId = "ca-app-pub-7534818044257511/2661464246";
	#elif UNITY_IOS
	string adUnitId = "ca-app-pub-7534818044257511/7644577047";
	#else
	string adUnitId = "unexpected_platform";
	#endif

	void Awake (){
		if (instance == null) {
			instance = this;	
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	void Start(){
		showBannerAd ();
	}


	public void showBannerAd(){

	if(KeepDataOnPlayMode.instance.noAds){

		} else {
			// Create a 320x50 banner at the top of the screen.
			bannerView = new BannerView (adUnitId, AdSize.Banner, AdPosition.Bottom);
			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder ().Build ();
			//		// Load the banner with the request.
			bannerView.LoadAd (request);
		}

	}

}
