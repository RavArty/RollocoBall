using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGlow : MonoBehaviour {

	public GameObject activateLine;
	public GameObject barier;

	void OnTriggerEnter2D(Collider2D coll){
		FadeObject.instance.FadeIn (activateLine, 0.2f);

	}
}
