using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

	// Use this for initialization
	public void playSound () {
		if (KeepDataOnPlayMode.instance.isSoundOn) {
			GetComponent<AudioSource> ().Play ();
		}
	}
	

}
