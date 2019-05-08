using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateComplexObj : MonoBehaviour {

	public GameObject[] lines;
	public GameObject containerObject;


	
	void Start () {

		containerObject = new GameObject("Lines");
		GenerateComplexLine();
	}
	
	void GenerateComplexLine(){
		float farthestLineEndX = 0;
		float farthestLineEndY = 0;

		for (int i = 0; i < lines.Length; i++) {
			GameObject line = (GameObject)Instantiate (lines [i]);
		
			if (i == 15) {
					line.transform.rotation = new Quaternion(line.transform.rotation.x,
						transform.rotation.y,0,transform.rotation.w);  
			}
			if (i == 16) {  
			
				line.transform.Rotate(0,0, -90);
	
			}

			line.GetComponent<PostionCotrol> ().detectPosition ();
			line.transform.GetChild (2).transform.position = new Vector2 (farthestLineEndX, farthestLineEndY);
			line.GetComponent<PostionCotrol> ().setPosition ();

			if (i == 2) {
			
			}

			farthestLineEndY = line.transform.GetChild (3).transform.position.y;
			farthestLineEndX = line.transform.GetChild(3).transform.position.x;
			
		}
	}
}
