using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPurchase : MonoBehaviour {

	private int mediumScore;

	void Awake(){
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}
	public void checkEligibilityForPurchase(){
		MusicSound.instance.ClickButtonSound ();
		TotalData.LoadTotalFromFile ();
		if (!transform.GetComponent<Player> ().isLocked) {
			changeActivePlayerUnLocked ();
		} else {
			if (TotalData.total.totalScore < transform.GetComponent<Player> ().price) {
				ShopScore.instance.notEnoughMoney ();
			} else {
				if (!transform.GetComponent<Player> ().isLocked) {
					return;
				}
				mediumScore = TotalData.total.totalScore;
				TotalData.total.totalScore -= transform.GetComponent<Player> ().price;
				transform.GetComponent<Player> ().isLocked = false;
				changeActivePlayer ();
			}
		}
	}

	void changeActivePlayer(){

		int previousActiveID = 10000;

		foreach (TotalData.Players player in TotalData.total.players) {
			if (player.ID == transform.GetComponent<Player> ().ID) {
				player.isLocked = false;
				player.isActive = true;
			} else if(player.isActive && player.ID != transform.GetComponent<Player> ().ID){
				player.isActive = false;
				previousActiveID = player.ID;
	
			}
		}
		TotalData.SaveTotalToFile ();
		ShopScore.instance.checkScore (mediumScore, TotalData.total.totalScore, 1.0f);

		transform.GetComponent<Player> ().activatePlayer (previousActiveID);
	}

	void changeActivePlayerUnLocked(){

		int previousActiveID = 10000;

		foreach (TotalData.Players player in TotalData.total.players) {
			if (player.ID == transform.GetComponent<Player> ().ID) {
				player.isLocked = false;
				player.isActive = true;
			} else if(player.isActive && player.ID != transform.GetComponent<Player> ().ID){
				player.isActive = false;
				previousActiveID = player.ID;
				
			}
		}
		TotalData.SaveTotalToFile ();
	
		transform.GetComponent<Player> ().activatePlayer (previousActiveID);
	}


}
