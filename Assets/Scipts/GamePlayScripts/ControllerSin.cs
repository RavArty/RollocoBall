using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ControllerSin : MonoBehaviour {

	public GameObject[] lines;
	public GameObject 	ball;
	public List<GameObject> currentLines;
	private int[] rotateLine = new int[]{ 0, 90, 180, 270};
	private int changeOrder = 1;
	private Color newColor;
	public Color colorPicker;
	private Color32[] newColorRange = new Color32[]{
												 new Color32(53, 123, 255, 255),
												 new Color32(255, 63, 68, 255), 
												 new Color32(255, 243, 89, 255), 
												 new Color32(191, 246, 103, 255),
												 new Color32(128, 245, 255, 255),
												 new Color32(245, 60, 239, 255),
												 new Color32(245, 60, 239, 255),
												 new Color32(180, 213, 36, 255),
												 new Color32(135, 123, 163, 255),
												 new Color32(255, 174, 24, 255),
												 new Color32(85, 187, 211, 176)};

	void Start(){
		GenerateLines ();
	}

	public void ReloadGame(){
	//	if (Input.GetKeyDown (KeyCode.R)) {
			Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.name);
		
	}
	void GenerateLines(){
		for (int i = 0; i <= 150; i++) {

			changeOrder++;
			if (changeOrder > 4) {
				changeOrder = 1;
			}

			if (changeOrder == 1) {
				int randomColor = Random.Range(0, newColorRange.Length);
				newColor.r = colorPicker.r + randomColor;
				newColor.g = colorPicker.g + randomColor;
				newColor.b = colorPicker.b + randomColor;
				GameObject line = (GameObject)Instantiate(lines[0]);
				line.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange[randomColor];
				if (line.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color == Color.blue) {
				
				}
				line.GetComponent<PostionCotrol> ().detectPosition ();
				int number = currentLines.Count;
				GameObject last = currentLines [number - 1];
				line.transform.GetChild (1).transform.position = last.transform.GetChild (2).transform.position;
				line.GetComponent<PostionCotrol> ().setPosition ();
				currentLines.Add (line);

			}else if (changeOrder == 2) {

				int randomColor = Random.Range(0, newColorRange.Length);
				newColor.r = colorPicker.r + randomColor;
				newColor.g = colorPicker.g + randomColor;
				newColor.b = colorPicker.b + randomColor;
				GameObject line = (GameObject)Instantiate(lines[1]);
				line.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange[randomColor];
				if (line.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color == Color.blue) {
				
				}
			
				line.GetComponent<PostionCotrol> ().detectPosition ();
				int number = currentLines.Count;
				GameObject last = currentLines [number - 1];
				line.transform.GetChild (1).transform.position = last.transform.GetChild (2).transform.position;
				line.GetComponent<PostionCotrol> ().setPosition ();
				currentLines.Add (line);

			}else if (changeOrder == 3) {

				int randomColor = Random.Range(0, newColorRange.Length);
				newColor.r = colorPicker.r + randomColor;
				newColor.g = colorPicker.g + randomColor;
				newColor.b = colorPicker.b + randomColor;
				GameObject line = (GameObject)Instantiate(lines[2]);
				line.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange[randomColor];
				if (line.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color == Color.blue) {
				
				}
			
				line.GetComponent<PostionCotrol> ().detectPosition ();
				int number = currentLines.Count;
				GameObject last = currentLines [number - 1];
				line.transform.GetChild (1).transform.position = last.transform.GetChild (2).transform.position;
				line.GetComponent<PostionCotrol> ().setPosition ();
				currentLines.Add (line);

			}else if (changeOrder == 4) {

				int randomColor = Random.Range(0, newColorRange.Length);
				newColor.r = colorPicker.r + randomColor;
				newColor.g = colorPicker.g + randomColor;
				newColor.b = colorPicker.b + randomColor;
				GameObject line = (GameObject)Instantiate(lines[3]);
				line.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange[randomColor];
				
				if (line.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color == Color.blue) {
				
				}
			
				line.GetComponent<PostionCotrol> ().detectPosition ();
				int number = currentLines.Count;
				GameObject last = currentLines [number - 1];
				line.transform.GetChild (1).transform.position = last.transform.GetChild (2).transform.position;
				line.GetComponent<PostionCotrol> ().setPosition ();
				currentLines.Add (line);
			}
				

		}
	}
}
