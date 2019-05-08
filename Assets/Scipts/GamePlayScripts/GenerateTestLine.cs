using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTestLine : MonoBehaviour {

	public GameObject[] lines;
	public List<GameObject> currentLines;
	private Color newColor;
	public Color colorPicker;
	private int randomColor;
	private float screenHeight;
	private float screenWidth;
	private Color32 playerColor;
	private int randomLineIndex;

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

	void Start () {
		playerColor = gameObject.GetComponent<SpriteRenderer> ().color;
		screenHeight = 2.0f * Camera.main.orthographicSize;
		screenWidth = screenHeight * Camera.main.aspect;

		TotalData.LoadTotalFromFile ();
	}

	void FixedUpdate () {
		GenerateLineIfRequired ();
	}

	void GenerateLineIfRequired(){

		List<GameObject> linesToRemove = new List<GameObject> ();
		bool addLineIndicator = true;
		float playerX = transform.position.x;
		float removeLine = playerX - screenWidth;
		float addLine = playerX + screenWidth;
		float farthestLineEndX = 0;
		float farthestLineEndY = 0;

		foreach (var line in currentLines) {
			
			if (line.transform.GetChild(0).transform.childCount == 0) {				//if simple object, do stuff
				if (line.transform.GetChild(3).transform.position.x > addLine) {		//getchild(3) - up trigger
					addLineIndicator = false;
				}

				if (line.transform.GetChild(3).transform.position.x < removeLine) {
					linesToRemove.Add (line);
				}

				if (line.transform.GetChild(3).transform.position.x > farthestLineEndX){
					farthestLineEndY = line.transform.GetChild (3).transform.position.y;
				}


				farthestLineEndX = Mathf.Max (farthestLineEndX, line.transform.GetChild(3).transform.position.x);

			} else if (line.transform.GetChild(0).transform.childCount > 0) {											//if not, find last child and do stuff

				if (line.transform.GetChild (line.transform.childCount - 1).transform.GetChild(3).transform.position.x > addLine) {
					addLineIndicator = false;
				}

				if (line.transform.GetChild (line.transform.childCount - 1).transform.GetChild(3).transform.position.x < removeLine) {
					linesToRemove.Add (line.transform.gameObject);
				}

				if (line.transform.GetChild (line.transform.childCount - 1).transform.GetChild (3).transform.position.x > farthestLineEndX) {
					farthestLineEndY = line.transform.GetChild (line.transform.childCount - 1).transform.GetChild (3).transform.position.y;
				}

				farthestLineEndX = Mathf.Max (farthestLineEndX, 
					line.transform.GetChild (line.transform.childCount - 1).transform.GetChild(3).transform.position.x);

			}
		}
		foreach (var line in linesToRemove) {
			currentLines.Remove (line);
			ObjectPool.current.PoolObject (line);
		}

		if (addLineIndicator) {
			AddLine (farthestLineEndX, farthestLineEndY);
		}
		linesToRemove.Clear ();
	}

	void AddLine(float farthestLineEndX, float farthestLineEndY){


			if (lines [randomLineIndex].transform.GetChild (0).transform.childCount == 0) {			//if simple object

				GameObject line = ObjectPool.current.GetObject (lines [randomLineIndex]);
				line.SetActive (true);
	
				randomColor = Random.Range (0, newColorRange.Length);

				Color32 previousColor;

				if (currentLines [currentLines.Count - 1].transform.GetChild (0).transform.childCount == 0) {
					previousColor = currentLines [currentLines.Count - 1].transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color;
				} else {
					previousColor = currentLines [currentLines.Count - 1].transform.GetChild (currentLines [currentLines.Count - 1].transform.childCount - 1).transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color;
				}
				if ((newColorRange [randomColor].r == previousColor.r) && (newColorRange [randomColor].g == previousColor.g) &&
					(newColorRange [randomColor].b == previousColor.b) && (newColorRange [randomColor].a == previousColor.a)) {

					if (randomColor == newColorRange.Length - 1) {
						randomColor = 0;
					} else
						randomColor += 1;
				}

				line.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange [randomColor];

				if (newColorRange [randomColor].r == playerColor.r && newColorRange [randomColor].g == playerColor.g &&
					newColorRange [randomColor].b == playerColor.b && newColorRange [randomColor].a == playerColor.a) {
								line.transform.GetChild (4).gameObject.SetActive (true);
						if (line.transform.GetChild (4).gameObject.GetComponent<SpriteRenderer> ().enabled) {
										line.transform.GetChild (4).gameObject.GetComponent<CircleCollider2D> ().enabled = true;
						}
				}  

				line.name = lines [randomLineIndex].name;			//delete "Clone" in the name
				line.GetComponent<PostionCotrol> ().detectPosition ();
				line.transform.GetChild (2).transform.position = new Vector2 (farthestLineEndX, farthestLineEndY);
				line.GetComponent<PostionCotrol> ().setPosition ();
				currentLines.Add (line);

			} else if (lines [randomLineIndex].transform.GetChild (0).transform.childCount > 0) {

				GameObject line = ObjectPool.current.GetObject (lines [randomLineIndex]);
				line.SetActive (true);
				line.transform.position = new Vector2 (farthestLineEndX, farthestLineEndY);

				randomColor = Random.Range (0, newColorRange.Length);



				if (currentLines [currentLines.Count - 1].transform.GetChild (0).transform.childCount == 0) {
					previousColor = currentLines [currentLines.Count - 1].transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color;
				} else {
					previousColor = currentLines [currentLines.Count - 1].transform.GetChild (currentLines [currentLines.Count - 1].transform.childCount - 1).gameObject.GetComponent<SpriteRenderer> ().color;
				}



				if ((newColorRange [randomColor].r == previousColor.r) && (newColorRange [randomColor].g == previousColor.g) &&
					(newColorRange [randomColor].b == previousColor.b) && (newColorRange [randomColor].a == previousColor.a)) {

					if (randomColor == newColorRange.Length - 1) {
						randomColor = 0;
					} else
						randomColor += 1;
				}

				line.transform.GetChild (0).transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange [randomColor];

				if (newColorRange [randomColor].r == playerColor.r && newColorRange [randomColor].g == playerColor.g &&
					newColorRange [randomColor].b == playerColor.b && newColorRange [randomColor].a == playerColor.a) {

								line.transform.GetChild (0).transform.GetChild (4).gameObject.SetActive (true);
						if (line.transform.GetChild (0).transform.GetChild (4).gameObject.GetComponent<SpriteRenderer> ().enabled) {
									line.transform.GetChild (0).transform.GetChild (4).gameObject.GetComponent<CircleCollider2D> ().enabled = true;
						}
			
				}  

				for (int i = 1; i < line.transform.childCount; i++) {

		
					randomColor = Random.Range (0, newColorRange.Length);

					if (line.transform.GetChild (i).childCount == 0) {
						//do nothing
					} else {
						previousColor = line.transform.GetChild (i - 1).transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color;
						if ((newColorRange [randomColor].r == previousColor.r) && (newColorRange [randomColor].g == previousColor.g) &&
							(newColorRange [randomColor].b == previousColor.b) && (newColorRange [randomColor].a == previousColor.a)) {

							if (randomColor == newColorRange.Length - 1) {
								randomColor = 0;
							} else
								randomColor += 1;
						}
						line.transform.GetChild (i).transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange [randomColor];

						if (newColorRange [randomColor].r == playerColor.r && newColorRange [randomColor].g == playerColor.g &&
							newColorRange [randomColor].b == playerColor.b && newColorRange [randomColor].a == playerColor.a) {
							if (line.transform.GetChild (i).transform.childCount > 2) {
					
										line.transform.GetChild (i).transform.GetChild (4).gameObject.SetActive (true);
									if (line.transform.GetChild (i).transform.GetChild (4).gameObject.GetComponent<SpriteRenderer> ().enabled) {
												line.transform.GetChild (i).transform.GetChild (4).gameObject.GetComponent<CircleCollider2D> ().enabled = true;
									}
					
							}
						} 
					}
				}

				line.name = lines [randomLineIndex].name;			//delete "Clone" in the name
				currentLines.Add (line);
			}
		}

}
