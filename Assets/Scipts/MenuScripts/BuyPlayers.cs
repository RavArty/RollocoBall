using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPlayers : MonoBehaviour {


	public static BuyPlayers instance;

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
	// Use this for initialization
	void Start () {
		if (TotalData.total.players.Count == 0) {
			TotalData.Players tempPlayers = null;

			foreach (Transform obj in transform) {
				tempPlayers = new TotalData.Players ();
				tempPlayers.ID = obj.GetComponent<Player> ().ID;
				tempPlayers.price = obj.GetComponent<Player> ().price;
				tempPlayers.isLocked = true;
				if (tempPlayers.ID == 1) {
					tempPlayers.isLocked = false;
					tempPlayers.isActive = true;
				}
				TotalData.total.players.Add (tempPlayers);
			}
		
			TotalData.SaveTotalToFile ();
		}

		if (TotalData.total.players.Count < transform.childCount) {
			int totalPlayers = TotalData.total.players.Count;
			int scenePlayers = transform.childCount;

			TotalData.Players tempPlayers = null;
			for (int i = totalPlayers; i < scenePlayers; i++) {
				tempPlayers = new TotalData.Players ();
				tempPlayers.ID = transform.GetChild (i).GetComponent<Player> ().ID;
				tempPlayers.price = transform.GetChild (i).GetComponent<Player> ().price;
				tempPlayers.isLocked = true;

				TotalData.total.players.Add (tempPlayers);
			}
			TotalData.SaveTotalToFile ();
		}
		TotalData.LoadTotalFromFile ();
		foreach(TotalData.Players player in TotalData.total.players){
			foreach (Transform obj in transform) {
				if (obj.GetComponent<Player> ().ID == player.ID) {
					if (!player.isLocked) {
						obj.GetComponent<Player> ().isLocked = false;
					}
					if (player.isActive) {
						obj.GetComponent<Player> ().isActive = true;
					}
					obj.GetComponent<Player> ().price = player.price;
					obj.GetComponent<Player> ().setPlayer ();
				}
			}
		}
	}

	public void deactivatePreviousActive(int previousActiveID){
	
		foreach (Transform obj in transform) {
			if (obj.GetComponent<Player> ().ID == previousActiveID) {
	
				FadeImage.instance.FadeOut (obj.GetComponent<Player>().frame, 0.2f);
			}
		}
	}
}
