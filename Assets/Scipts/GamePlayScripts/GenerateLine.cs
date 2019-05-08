using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateLine : MonoBehaviour {

	public int startBarriers = 15;
	private int randomBarrier = 0;
	public Timer timer;
	public GameObject[] lines;
	public GameObject firstLine;
	private float screenHeight;
	private float screenWidth;
	private int randomLineIndex;
	private Color newColor;
	public Color colorPicker;
	private int randomColor;
	private Color32 playerColor;
	private int countLines = 0;
	public GameObject containerObject;
	public GameObject gameObjectsHolder;

	private int lowCount = 5;
	private int upCount = 16;

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

	public List<GameObject> currentLines;

	void Awake(){
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}

	void Start () {
		randomBarrier = Random.Range (lowCount, upCount);
		playerColor = gameObject.GetComponent<SpriteRenderer> ().color;
		screenHeight = 2.0f * Camera.main.orthographicSize;
		screenWidth = screenHeight * Camera.main.aspect;

		containerObject = new GameObject("Lines");
		containerObject.transform.parent = gameObjectsHolder.transform;
		TotalData.LoadTotalFromFile ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		GenerateLineIfRequired ();
	}
//-------------------------------------------------------------------------------------------------------------
//*************************************************************************************************************
//-------------------------------------------------------------------------------------------------------------
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

				if (line.transform.GetChild (3).transform.position.x > farthestLineEndX) {
					farthestLineEndY = line.transform.GetChild (3).transform.position.y;
				} else if (line.name == lines [15].name || line.name == lines [16].name) {
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

//-------------------------------------------------------------------------------------------------------------
//*************************************************************************************************************
//-------------------------------------------------------------------------------------------------------------
	void AddLine(float farthestLineEndX, float farthestLineEndY){

		if (TotalData.total.firstGame) {
			lineForFirstGame (farthestLineEndX, farthestLineEndY);

		} else {
			putApropriateLine ();

			if (lines [randomLineIndex].transform.GetChild (0).transform.childCount == 0) {			//if simple object

				GameObject line = ObjectPool.current.GetObject (lines [randomLineIndex]);
				line.SetActive (true);
				line.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Lines";
				line.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "Lines";
				line.transform.parent = containerObject.transform;
				countLines += 1;
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

				if (newColorRange [randomColor].r == playerColor.r && newColorRange [randomColor].g == playerColor.g &&
					newColorRange [randomColor].b == playerColor.b && newColorRange [randomColor].a == playerColor.a) {
					if (countLines != randomBarrier) {
						if (randomColor == newColorRange.Length - 1) {
							randomColor = 0;
						} else {
							randomColor += 1;
						}
					
					}
				}  
				if (countLines == randomBarrier) {
					countLines = 0;
					runRandomBarrier ();
					line.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = playerColor;
					line.transform.GetChild (4).gameObject.SetActive (true);
					if (line.transform.GetChild (4).gameObject.GetComponent<SpriteRenderer> ().enabled) {
						line.transform.GetChild (4).gameObject.GetComponent<CircleCollider2D> ().enabled = true;
					}
				} else {
					line.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange [randomColor];
				}

				line.name = lines [randomLineIndex].name;			//delete "Clone" in the name
				line.GetComponent<PostionCotrol> ().detectPosition ();
				line.transform.GetChild (2).transform.position = new Vector2 (farthestLineEndX, farthestLineEndY);
				line.GetComponent<PostionCotrol> ().setPosition ();
				currentLines.Add (line);

			} else if (lines [randomLineIndex].transform.GetChild (0).transform.childCount > 0) {

				GameObject line = ObjectPool.current.GetObject (lines [randomLineIndex]);
				line.SetActive (true);
				line.transform.parent = containerObject.transform;
				line.transform.position = new Vector2 (farthestLineEndX, farthestLineEndY);
				countLines += 1;
				randomColor = Random.Range (0, newColorRange.Length);



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

				if (newColorRange [randomColor].r == playerColor.r && newColorRange [randomColor].g == playerColor.g &&
					newColorRange [randomColor].b == playerColor.b && newColorRange [randomColor].a == playerColor.a) {
					if (countLines != randomBarrier) {
						if (randomColor == newColorRange.Length - 1) {
							randomColor = 0;
						} else {
							randomColor += 1;
						}

					}
				}  
		
				if (countLines == randomBarrier) {
					countLines = 0;
					runRandomBarrier ();
					line.transform.GetChild (0).transform.GetChild (0).GetComponent<SpriteRenderer> ().color = playerColor;
					line.transform.GetChild (0).transform.GetChild (4).gameObject.SetActive (true);
					if (line.transform.GetChild (0).transform.GetChild (4).gameObject.GetComponent<SpriteRenderer> ().enabled) {
						line.transform.GetChild (0).transform.GetChild (4).gameObject.GetComponent<CircleCollider2D> ().enabled = true;
					}
				} else {
					line.transform.GetChild (0).transform.GetChild (0).GetComponent<SpriteRenderer> ().color = newColorRange [randomColor];
				}

				line.transform.GetChild (0).transform.GetChild (0).GetComponent<SpriteRenderer>().sortingLayerName = "Lines";
				line.transform.GetChild (0).transform.GetChild (1).GetComponent<SpriteRenderer>().sortingLayerName = "Lines";

				for (int i = 1; i < line.transform.childCount; i++) {

					countLines += 1;
					randomColor = Random.Range (0, newColorRange.Length);

		
						previousColor = line.transform.GetChild (i - 1).transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color;
						if ((newColorRange [randomColor].r == previousColor.r) && (newColorRange [randomColor].g == previousColor.g) &&
						   (newColorRange [randomColor].b == previousColor.b) && (newColorRange [randomColor].a == previousColor.a)) {

							if (randomColor == newColorRange.Length - 1) {
								randomColor = 0;
							} else
								randomColor += 1;
						}

					if (newColorRange [randomColor].r == playerColor.r && newColorRange [randomColor].g == playerColor.g &&
						newColorRange [randomColor].b == playerColor.b && newColorRange [randomColor].a == playerColor.a) {
						if (countLines != randomBarrier) {
							if (randomColor == newColorRange.Length - 1) {
								randomColor = 0;
							} else {
								randomColor += 1;
							}

						}
					}  
					if (countLines == randomBarrier) {
						countLines = 0;
						runRandomBarrier ();
						line.transform.GetChild (i).transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = playerColor;
						line.transform.GetChild (i).transform.GetChild (4).gameObject.SetActive (true);
						if (line.transform.GetChild (i).transform.GetChild (4).gameObject.GetComponent<SpriteRenderer> ().enabled) {
							line.transform.GetChild (i).transform.GetChild (4).gameObject.GetComponent<CircleCollider2D> ().enabled = true;
						}
					} else {
						line.transform.GetChild (i).transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange [randomColor];
					}
					//	line.transform.GetChild (i).transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = newColorRange [randomColor];
						line.transform.GetChild (i).transform.GetChild (0).GetComponent<SpriteRenderer>().sortingLayerName = "Lines";
						line.transform.GetChild (i).transform.GetChild (1).GetComponent<SpriteRenderer>().sortingLayerName = "Lines";

				}

				line.name = lines [randomLineIndex].name;			//delete "Clone" in the name
				currentLines.Add (line);
			}
		}

	}
//-------------------------------------------------------------------------------------------------------------
//*************************************************************************************************************
//-------------------------------------------------------------------------------------------------------------
	void putApropriateLine(){
		if (currentLines [currentLines.Count - 1].name == lines [0].name) {
			do {
				randomLineIndex = Random.Range (0, lines.Length);
				if (randomLineIndex != 0 && randomLineIndex != 3 && randomLineIndex != 4 && randomLineIndex != 7
					&& randomLineIndex != 10 && randomLineIndex != 11 && randomLineIndex != 15 && randomLineIndex != 16
					&& randomLineIndex != 19 && randomLineIndex != 20) {
					break;
				}
			} while(true);

		} else if (currentLines [currentLines.Count - 1].name == lines [1].name || currentLines [currentLines.Count - 1].name == lines [19].name) {
			do {
				randomLineIndex = Random.Range (0, lines.Length);
				if (randomLineIndex != 0 && randomLineIndex != 1 && randomLineIndex != 2 &&
				   randomLineIndex != 4 && randomLineIndex != 6 && randomLineIndex != 5 && randomLineIndex != 8
				   && randomLineIndex != 9 && randomLineIndex != 10 && randomLineIndex != 11 && randomLineIndex != 12
					&& randomLineIndex != 13 && randomLineIndex != 14 && randomLineIndex != 16 && randomLineIndex != 17
					&& randomLineIndex != 18 && randomLineIndex != 21 && randomLineIndex != 22 && randomLineIndex != 23) {
					break;
				}
			} while(true);

		} else if (currentLines [currentLines.Count - 1].name == lines [2].name || currentLines [currentLines.Count - 1].name == lines [10].name) {
			do {
				randomLineIndex = Random.Range (0, lines.Length);
				if (randomLineIndex != 1 && randomLineIndex != 2 && randomLineIndex != 3 &&
				   randomLineIndex != 5 && randomLineIndex != 6 && randomLineIndex != 7 && randomLineIndex != 8
				   && randomLineIndex != 9 && randomLineIndex != 12 && randomLineIndex != 13 && randomLineIndex != 14
					&& randomLineIndex != 15 && randomLineIndex != 17 && randomLineIndex != 18 && randomLineIndex != 19
					&& randomLineIndex != 20 && randomLineIndex != 21 && randomLineIndex != 22 && randomLineIndex != 23) {
					break;
				}
			} while(true);

		} else if (currentLines [currentLines.Count - 1].name == lines [3].name) {
			do {
				randomLineIndex = Random.Range (0, lines.Length);
				if (randomLineIndex != 0 && randomLineIndex != 3 &&
				   randomLineIndex != 4 && randomLineIndex != 7 && randomLineIndex != 10 && randomLineIndex != 11
					&& randomLineIndex != 15 && randomLineIndex != 16 && randomLineIndex != 19 && randomLineIndex != 20) {
					break;
				}
			} while(true);

		} else if (currentLines [currentLines.Count - 1].name == lines [4].name) {
			do {
				randomLineIndex = Random.Range (0, lines.Length);
				if (randomLineIndex != 7 && randomLineIndex != 4 &&
				   randomLineIndex != 0 && randomLineIndex != 3 && randomLineIndex != 10 && randomLineIndex != 11
					&& randomLineIndex != 11 && randomLineIndex != 15 && randomLineIndex != 16 && randomLineIndex != 19
					&& randomLineIndex != 20) {
					break;
				}
			} while(true);

		} else if (currentLines [currentLines.Count - 1].name == lines [5].name || currentLines [currentLines.Count - 1].name == lines [19].name) {
			do {
				randomLineIndex = Random.Range (0, lines.Length);
				if (randomLineIndex != 4 && randomLineIndex != 6 && randomLineIndex != 5 &&
				   randomLineIndex != 0 && randomLineIndex != 1 && randomLineIndex != 2 && randomLineIndex != 8
				   && randomLineIndex != 9 && randomLineIndex != 10 && randomLineIndex != 11 && randomLineIndex != 12
					&& randomLineIndex != 13 && randomLineIndex != 14&& randomLineIndex != 16 && randomLineIndex != 17
					&& randomLineIndex != 18 && randomLineIndex != 21 && randomLineIndex != 22 && randomLineIndex != 23) {
					break;
				}
			} while(true);

		} else if (currentLines [currentLines.Count - 1].name == lines [6].name || currentLines [currentLines.Count - 1].name == lines [10].name) {
			do {
				randomLineIndex = Random.Range (0, lines.Length);
				if (randomLineIndex != 5 && randomLineIndex != 6 && randomLineIndex != 7 &&
				   randomLineIndex != 1 && randomLineIndex != 2 && randomLineIndex != 3 && randomLineIndex != 8
				   && randomLineIndex != 9 && randomLineIndex != 12 && randomLineIndex != 13 && randomLineIndex != 14
					&& randomLineIndex != 15 && randomLineIndex != 17 && randomLineIndex != 18 && randomLineIndex != 19
					&& randomLineIndex != 20 && randomLineIndex != 21 && randomLineIndex != 22 && randomLineIndex != 23) {
					break;
				}
			} while(true);

		} else if (currentLines [currentLines.Count - 1].name == lines [7].name) {
			do {
				randomLineIndex = Random.Range (0, lines.Length);
				if (randomLineIndex != 4 && randomLineIndex != 7 &&
				   randomLineIndex != 0 && randomLineIndex != 3 && randomLineIndex != 10 && randomLineIndex != 11
					&& randomLineIndex != 15 && randomLineIndex != 16 && randomLineIndex != 19 && randomLineIndex != 20) {
					break;
				}
			} while(true);

		} else if (currentLines [currentLines.Count - 1].name == lines [8].name || currentLines [currentLines.Count - 1].name == lines [9].name
		          || currentLines [currentLines.Count - 1].name == lines [11].name || currentLines [currentLines.Count - 1].name == lines [12].name
		          || currentLines [currentLines.Count - 1].name == lines [13].name || currentLines [currentLines.Count - 1].name == lines [14].name
			|| currentLines [currentLines.Count - 1].name == lines [17].name || currentLines [currentLines.Count - 1].name == lines [18].name
			|| currentLines [currentLines.Count - 1].name == lines [20].name  || currentLines [currentLines.Count - 1].name == lines [21].name 
			|| currentLines [currentLines.Count - 1].name == lines [22].name || currentLines [currentLines.Count - 1].name == lines [23].name
			|| currentLines [currentLines.Count - 1].name == "complexObjFirst") {
			do {
				randomLineIndex = Random.Range (0, lines.Length);
				if (randomLineIndex != 0 && randomLineIndex != 3 &&
				   randomLineIndex != 4 && randomLineIndex != 7 && randomLineIndex != 10 && randomLineIndex != 11
					&& randomLineIndex != 15 && randomLineIndex != 16 && randomLineIndex != 19
					&& randomLineIndex != 20) {
					break;
				}
			} while(true);

		} else if (currentLines [currentLines.Count - 1].name == lines [15].name) {
			do {
				randomLineIndex = Random.Range (0, lines.Length);
				if (randomLineIndex != 0 && randomLineIndex != 1 && randomLineIndex != 2 && randomLineIndex != 4 && randomLineIndex != 5 && randomLineIndex != 6
					&& randomLineIndex != 8 && randomLineIndex != 9 && randomLineIndex != 10 && randomLineIndex != 11
					&& randomLineIndex != 12 && randomLineIndex != 13 && randomLineIndex != 14 && randomLineIndex != 16
					&& randomLineIndex != 17 && randomLineIndex != 18 && randomLineIndex != 21 && randomLineIndex != 22
					&& randomLineIndex != 23) {
					break;
				}
			} while(true);

		}else if (currentLines [currentLines.Count - 1].name == lines [16].name) {
			do {
				randomLineIndex = Random.Range (0, lines.Length);
				if (randomLineIndex != 1 && randomLineIndex != 2
					&& randomLineIndex != 3 && randomLineIndex != 5 && randomLineIndex != 6 && randomLineIndex != 7
					&& randomLineIndex != 8 && randomLineIndex != 9 && randomLineIndex != 11 && randomLineIndex != 12
					&& randomLineIndex != 13 && randomLineIndex != 14 && randomLineIndex != 15 && randomLineIndex != 17
					&& randomLineIndex != 18 && randomLineIndex != 19 && randomLineIndex != 20 && randomLineIndex != 21
					&& randomLineIndex != 22 && randomLineIndex != 23) {
					break;
				}
			} while(true);

		}else {
			randomLineIndex = Random.Range (0, lines.Length);
		}

	}
//-------------------------------------------------------------------------------------------------------------
//*************************************************************************************************************
//-------------------------------------------------------------------------------------------------------------
	void lineForFirstGame(float farthestLineEndX, float farthestLineEndY){

		GameObject line = ObjectPool.current.GetObject (firstLine);
		line.SetActive (true);
		line.transform.parent = containerObject.transform;

		line.transform.position = new Vector2 (farthestLineEndX, farthestLineEndY);
		line.name = firstLine.name;			//delete "Clone" in the name

		currentLines.Add (line);
		TotalData.total.firstGame = false;
		TotalData.SaveTotalToFile ();
	}
//-------------------------------------------------------------------------------------------------------------
//*************************************************************************************************************
//-------------------------------------------------------------------------------------------------------------
	void runRandomBarrier(){
		randomBarrier = Random.Range (lowCount, upCount);
	
	}
}
