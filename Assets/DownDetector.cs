using UnityEngine;
using System.Collections;

public class DownDetector : MonoBehaviour {

	public GameObject parentLine;
	public GameObject center;				//first part of line
	public GameObject center2;				//second part of line
	public GameObject nextLine;
	public GameObject previousLine;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
//			Debug.Log ("activate");
		//	parentLine.GetComponent<PolygonCollider2D> ().enabled = true;
		//	center.GetComponent<AreaEffector2D> ().enabled = true;
		//	center.GetComponent<AreaEffector2D> ().forceMagnitude *= 2;
		//	center.GetComponent<EdgeCollider2D> ().enabled = true;

		//	center2.GetComponent<AreaEffector2D> ().enabled = true;
		///	center2.GetComponent<AreaEffector2D> ().forceMagnitude *= 2;
		//	center2.GetComponent<EdgeCollider2D> ().enabled = true;
		}
	}
}