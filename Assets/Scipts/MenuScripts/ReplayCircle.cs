using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayCircle : MonoBehaviour {


	public Transform startPos;
	public Transform centerPos;

	public bool move;
	public bool moveLine;
	public bool moveLineUp;
	public bool moveLineVDown;
	private int child = 0;


	void OnTriggerEnter2D(Collider2D coll){
	
		coll.gameObject.transform.position = startPos.position;
	
		RotateCircle.instance.center = centerPos;
		RotateCircle.instance.move = move;
		RotateCircle.instance.moveLine = moveLine;
		RotateCircle.instance.moveLineUp = moveLineUp;
		RotateCircle.instance.moveLineVDown = moveLineVDown;

		for (int i = 1; i < coll.transform.childCount; i++) {
			if (coll.transform.GetChild (i).GetComponent<TrailRenderer> ().enabled) {
				child = i;
				coll.transform.GetChild (i).GetComponent<TrailRenderer> ().enabled = false;
			}
		}
		StartCoroutine (enableTrail (coll.gameObject));

	}

	IEnumerator enableTrail(GameObject obj){
		yield return new WaitForSeconds (0.29f);
	
		obj.transform.GetChild (child).GetComponent<TrailRenderer> ().enabled = true;

	}
}
