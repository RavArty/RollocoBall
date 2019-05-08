using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ShowNativeAds : MonoBehaviour {
	
	public static ShowNativeAds instance;
	[HideInInspector]
	public NativeExpressAdView nativeExpressAdView;

	#if UNITY_EDITOR
	string adUnitId = "unused";
	#elif UNITY_ANDROID
//	string adUnitId = "ca-app-pub-3940256099942544/1072772517";
	string adUnitId = "ca-app-pub-7534818044257511/4138197441";
	#elif UNITY_IOS
	string adUnitId = "ca-app-pub-7534818044257511/9121310248";
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
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}

	void Start(){
	preloadNativeAd ();
	}


	public void preloadNativeAd(){
	TotalData.LoadTotalFromFile ();
		if (TotalData.total.noAds) {
//			nativeExpressAdView.Hide ();
		} else {
			nativeExpressAdView = new NativeExpressAdView (adUnitId, AdSize.MediumRectangle, AdPosition.BottomRight);
			AdRequest request = new AdRequest.Builder ().Build ();
			nativeExpressAdView.LoadAd (request);
			nativeExpressAdView.Hide ();
		}
	}

	public void showNativeAds(){
		nativeExpressAdView.Show ();
	}
}