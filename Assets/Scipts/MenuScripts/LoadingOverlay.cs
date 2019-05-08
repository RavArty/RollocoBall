using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingOverlay : MonoBehaviour {

	private GameObject loadingOverlay;

	void Awake(){

		loadingOverlay = GameObject.Find("LoadingOverlayCanvas").transform.GetChild(0).gameObject;
	}

	public void loadOverlayTrue(){
		loadingOverlay.SetActive(true);
	}
	public void loadOverlayFalse(){
		loadingOverlay.SetActive(false);
	}

}
