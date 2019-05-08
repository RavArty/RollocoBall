using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersToChoose : MonoBehaviour {


	public Sprite[] players;
	[HideInInspector]
	public int id;

	void Awake(){
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}
	void Start(){
		TotalData.LoadTotalFromFile ();
		if (TotalData.total.players.Count == 0) {
			id = 1;
		} else {
			foreach (TotalData.Players player in TotalData.total.players) {
				if (player.isActive) {
					id = player.ID;	

				}
			}
		}

		transform.GetComponent<SpriteRenderer> ().sprite = players [(id * 2) - 2];
		transform.GetChild(0).transform.GetComponent<SpriteRenderer> ().sprite = players [((id * 2) - 2) + 1];
		ChangeColor.instance.changeTrailWidth (transform.GetComponent<SpriteRenderer> ().sprite.name);
	}
}
