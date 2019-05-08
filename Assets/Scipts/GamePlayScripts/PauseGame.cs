using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	public void Pause(){
		if (Time.timeScale > 0) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}
}
