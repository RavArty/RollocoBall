using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FadeObject.instance.FadeOut (gameObject, 0);
	}
	

}
