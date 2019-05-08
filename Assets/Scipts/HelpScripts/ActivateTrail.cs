using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrail : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
	//	coll.transform.GetChild (0).gameObject.SetActive (true);
		StartCoroutine(activateTrail(coll.gameObject));
	}

	IEnumerator activateTrail(GameObject obj){
		yield return new WaitForSeconds (0.29f);
		obj.transform.GetChild (1).gameObject.SetActive (true);
	}
}
