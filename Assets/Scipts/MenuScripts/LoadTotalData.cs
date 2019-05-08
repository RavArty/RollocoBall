using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTotalData : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
		TotalData.LoadTotalFromFile ();
		KeepDataOnPlayMode.instance.noAds = TotalData.total.noAds;
		KeepDataOnPlayMode.instance.isMusicOn = TotalData.total.isMusicOn;
		KeepDataOnPlayMode.instance.isSoundOn = TotalData.total.isSoundOn;
	}
	

}
