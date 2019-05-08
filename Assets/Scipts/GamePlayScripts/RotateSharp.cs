using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSharp : MonoBehaviour {

	private float speed = 50.0f;

	void Update(){
		transform.RotateAround (transform.position, Vector3.forward, Time.deltaTime  * speed);
	}
}
