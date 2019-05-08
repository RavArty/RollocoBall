using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLine : MonoBehaviour {

	public static ChooseLine instance;

	public GameObject[] lines;
	public GameObject circle;
	private int randomLine;
	private int countLines = 0;
	private int randomColor;
	private int circleRandomColor;
	Color32 previousColor;

	private Color32[] newColorRange = new Color32[] {
		new Color32 (0, 88, 255, 255),
		new Color32 (255, 0, 7, 255),
		new Color32(255, 237, 0, 255), 
		new Color32(157, 255, 0, 255),
		new Color32(0, 235, 255, 255),
		new Color32(255, 0, 247, 255),
		new Color32(207, 255, 0, 255),
		new Color32(119, 0, 253, 255),
		new Color32(255, 166, 0, 255),
		new Color32(0, 255, 181, 255)};


	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}


	void Start(){
		

		for (int i = 1; i < circle.transform.childCount; i++) {
			circle.transform.GetChild (i).gameObject.GetComponent<TrailRenderer>().enabled = false;
		}
		for (int i = 0; i < lines.Length; i++) {
			lines [i].SetActive (false);
		}
	
		randomLine = Random.Range (0, lines.Length);
		lines [randomLine].SetActive (true);
		circle.transform.position = lines[randomLine].transform.GetChild(0).transform.GetChild(2).position;
	
		generateColor ();
	
	}

	public void startLine(){
	
		for (int i = 0; i < lines.Length; i++) {
			lines [i].SetActive (false);
		}
		for (int i = 1; i < circle.transform.childCount; i++) {
			circle.transform.GetChild (i).gameObject.GetComponent<TrailRenderer>().enabled = false;
		}



		randomLine = Random.Range (0, lines.Length);
		lines [randomLine].SetActive (true);
		generateColor ();
		circle.transform.position = lines[randomLine].transform.GetChild(0).transform.GetChild(2).position;
	
		moveDirection ();
	}
	public void moveDirection(){
	
		if (lines [randomLine].transform.GetChild (0).transform.GetChild(2).name == "lineDown") {
		    RotateCircle.instance.center = lines[randomLine].transform.GetChild (0).transform.GetChild(5);
			RotateCircle.instance.move = false;
			RotateCircle.instance.moveLine = true;
			RotateCircle.instance.moveLineUp = false;
			RotateCircle.instance.moveLineVDown = false;
		}

		if (lines [randomLine].transform.GetChild (0).transform.GetChild(2).name == "lineVDown") {
			RotateCircle.instance.center = lines[randomLine].transform.GetChild(5);
			RotateCircle.instance.move = false;
			RotateCircle.instance.moveLine = false;
			RotateCircle.instance.moveLineUp = false;
			RotateCircle.instance.moveLineVDown = true;
		}
	}

	IEnumerator enableTrail(){
		yield return new WaitForSeconds (0.3f);
	
		circle.transform.GetChild (circleRandomColor + 1).GetComponent<TrailRenderer> ().sortingLayerName = "Player";
		circle.transform.GetChild (circleRandomColor + 1).GetComponent<TrailRenderer> ().enabled = true;


	}

	void generateColor(){

	
		circleRandomColor = Random.Range (9, 9);
		circle.GetComponent<SpriteRenderer> ().color = newColorRange [circleRandomColor];
	
		randomColor = Random.Range (0, newColorRange.Length);
		lines[randomLine].transform.GetChild (0).transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange [randomColor];

		for (int i = 1; i < lines[randomLine].transform.childCount; i++) {

			countLines += 1;
			randomColor = Random.Range (0, newColorRange.Length);

			if (lines[randomLine].transform.GetChild (i).childCount == 0) {
				//do nothing
			} else {
				previousColor = lines[randomLine].transform.GetChild (i - 1).transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color;
				if ((newColorRange [randomColor].r == previousColor.r) && (newColorRange [randomColor].g == previousColor.g) &&
					(newColorRange [randomColor].b == previousColor.b) && (newColorRange [randomColor].a == previousColor.a)) {

					if (randomColor == newColorRange.Length - 1) {
						randomColor = 0;
					} else
						randomColor += 1;
				}
				lines[randomLine].transform.GetChild (i).transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange [randomColor];


			}
		}
		StartCoroutine (enableTrail ());

	}


}
