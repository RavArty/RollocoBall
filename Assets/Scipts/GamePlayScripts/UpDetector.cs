using UnityEngine;
using System.Collections;

public class UpDetector : MonoBehaviour {
	
	public GameObject parentLine;
	public GameObject center;				//first part of line
	public GameObject center2;				//second part of line

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			parentLine.GetComponent<PolygonCollider2D> ().enabled = false;

			center.GetComponent<EdgeCollider2D> ().enabled = false;
			center.GetComponent<AreaEffector2D> ().enabled = false;

			center2.GetComponent<EdgeCollider2D> ().enabled = false;
			center2.GetComponent<AreaEffector2D> ().enabled = false;
		}
	}
}
