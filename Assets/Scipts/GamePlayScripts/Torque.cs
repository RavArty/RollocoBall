using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torque : MonoBehaviour {

	public float timecounter = 0;
	public Transform obj;
	public float speed;
	public Vector3 point;
	public Rigidbody2D rb;
	void Start() {
		rb = GetComponent<Rigidbody2D>();

	}

	void FixedUpdate() {

		timecounter += Time.deltaTime * speed;
		float x = obj.position.x + Mathf.Cos (timecounter) * Vector3.Distance(transform.position, obj.position);
		float y = obj.position.y + Mathf.Sin (timecounter) * Vector3.Distance(transform.position, obj.position);
       
		rb.MovePosition (new Vector3 (x, y, 0) + Vector3.forward * Time.deltaTime * 10 * speed);

	}


}
