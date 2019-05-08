using UnityEngine;
using System.Collections;

public class PostionCotrol : MonoBehaviour {

	public  Transform down;

	private Vector3 anchoredPosition;
	private Vector3 rememberDownPos;



	public void detectPosition(){
		rememberDownPos = down.localPosition;
		//Debug.Log ("rememberDownPos: " + rememberDownPos);
		anchoredPosition = transform.position - down.transform.position;
	}

	public void setPosition () {
		transform.position = anchoredPosition + down.position;
		down.localPosition = rememberDownPos;
	}
		

}
