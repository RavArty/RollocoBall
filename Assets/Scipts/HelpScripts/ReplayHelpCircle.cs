using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayHelpCircle : MonoBehaviour {


	public Transform startPos;
	public GameObject barier;

	void OnTriggerEnter2D(Collider2D coll){
		coll.transform.GetChild (1).gameObject.SetActive (false);
		coll.gameObject.transform.position = startPos.position;
		FadeObject.instance.FadeIn (barier, 0.2f);

	}
}