using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSettings : MonoBehaviour {
	public static TrailSettings instance;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}


	public void ChangeTrailWidth(GameObject basicObj, GameObject obj){
		if (basicObj.name == "ARROW") {

			obj.GetComponent<TrailRenderer> ().startWidth = 1.5f;
			obj.GetComponent<TrailRenderer> ().endWidth = 0.5f;
		}

	}
}
