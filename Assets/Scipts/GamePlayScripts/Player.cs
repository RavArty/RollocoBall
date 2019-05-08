using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour {
//	[HideInInspector]
	public int ID;

//	[HideInInspector]
	public GameObject bgPlayer;

//	[HideInInspector]
	public GameObject Lock;

//	[HideInInspector]
	public int price;

	public GameObject priceObj;

	public GameObject frame;
	public bool isLocked = true;
	public bool isActive = false;
	private bool unlockOnce = false;

	void Awake(){
		try{
			ID = int.Parse(name.Split('-')[1]);
			bgPlayer = transform.GetChild(0).gameObject;
			Lock = transform.GetChild(1).gameObject;
			priceObj = transform.GetChild(2).gameObject;
			priceObj.GetComponent<Text>().text = price.ToString();
			frame = transform.GetChild(3).gameObject;


		}catch (Exception ex){
			Debug.Log ("invalid palyer name");
			Debug.Log (ex);
		}

	}


	public void setPlayer(){
		if (!isLocked) {

			FadeImage.instance.FadeOut(Lock, 0.0f);
			priceObj.SetActive (false);
			unlockOnce = true;
		}
		if (isActive) {

			FadeImage.instance.FadeIn (frame, 0.0f);
		}
	}

	public void activatePlayer(int previousActiveID){
		isActive = true;

	
		if (!unlockOnce) {
			unlockOnce = true;
			FadeImage.instance.FadeOut(Lock, 0.2f);
			priceObj.GetComponent<Animator> ().SetTrigger ("Fade");	
		}

		FadeImage.instance.FadeIn (frame, 0.2f);

		BuyPlayers.instance.deactivatePreviousActive (previousActiveID);
	
	}
}
