using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

	public static ChangeColor instance;

	public GameObject line;

	[HideInInspector]
	public int enabledTrail = 1000;
	[HideInInspector]
	public float startWidth = 0.0f;
	[HideInInspector]
	public float endWidth = 0.0f;
	[HideInInspector]
	public Vector2 position;

	private int randomColor;
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
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
		TotalData.LoadTotalFromFile ();
		if (TotalData.total.firstGame) {
			transform.GetComponent<SpriteRenderer> ().color = newColorRange [0];
			transform.GetChild (1).GetComponent<TrailRenderer> ().enabled = true;
			enabledTrail = 1;
		}else{
			randomColor = Random.Range (0, newColorRange.Length);
			transform.GetComponent<SpriteRenderer> ().color = newColorRange [randomColor];
		
		}

		if (randomColor == newColorRange.Length - 1) {
			line.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = newColorRange [0];
		} else {
			line.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = newColorRange [randomColor + 1];
		}
	}

	public void changeTrailWidth(string name){
	
		if (name == "arrow") {
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 1.05f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 0.5f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (-1.22f, 0.0f);
			startWidth = 1.05f;
			endWidth = 0.5f;
			position = new Vector2 (-1.22f, 0.0f);
			StartCoroutine(enableTrail());

		}else if (name == "circle" || name == "bat" || name == "bear" || name == "bird"
			|| name == "plane") {
		
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 2.4f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 1.0f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (0.0f, 0.0f);
			startWidth = 2.4f;
			endWidth = 1.0f;
			position = new Vector2 (0.0f, 0.0f);
			StartCoroutine(enableTrail());

		}else if (name == "fish"){
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 1.57f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 0.5f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (0.55f, 0f);
			startWidth = 1.57f;
			endWidth = 0.5f;
			position = new Vector2 (0.55f, 0f);
			StartCoroutine(enableTrail());

		}else if (name == "duck") {
		
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 2.0f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 1.0f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (0.0f, 0.0f);
			startWidth = 2.0f;
			endWidth = 1.0f;
			position = new Vector2 (0.0f, 0.0f);
			StartCoroutine(enableTrail());

		}else if (name == "f1") {
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 2.4f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 1.0f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (-1.0f, 0.0f);
			startWidth = 2.4f;
			endWidth = 1.0f;
			position = new Vector2 (-1.0f, 0.0f);
			StartCoroutine(enableTrail());

		}else if (name == "rocket") {
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 1.47f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 0.5f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (0.29f, 0.0f);
			startWidth = 1.47f;
			endWidth = 0.5f;
			position = new Vector2 (0.29f, 0.0f);
			StartCoroutine(enableTrail());

		}else if (name == "shark") {
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 2.088f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 0.5f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (0.3f, 0.1f);
			startWidth = 2.088f;
			endWidth = 0.5f;
			position = new Vector2 (0.3f, 0.1f);
			StartCoroutine(enableTrail());

		}else if (name == "submarine"){
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 1.2f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 0.5f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (0.2f, 0.0f);
			startWidth = 1.2f;
			endWidth = 0.5f;
			position = new Vector2 (0.2f, 0.0f);
			StartCoroutine(enableTrail());

		}else if (name == "train"){
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 1.7f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 0.5f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (-2.0f, -0.4f);
			startWidth = 1.7f;
			endWidth = 0.5f;
			position = new Vector2 (-2.0f, -0.4f);
			StartCoroutine(enableTrail());

		}else if (name == "car"){
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 1.1f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 0.5f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (-1.35f, -0.1f);
			startWidth = 1.1f;
			endWidth = 0.5f;
			position = new Vector2 (-1.35f, -0.01f);
			StartCoroutine(enableTrail());

		}else if (name == "bull" ){
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 2.23f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 1.0f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (0.9f, 0.1f);
			startWidth = 2.23f;
			endWidth = 1.0f;
			position = new Vector2 (0.9f, 0.1f);
			StartCoroutine(enableTrail());

		}else if (name == "deltaplane") {
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().startWidth = 2.4f;
			transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().endWidth = 1.0f;
			transform.GetChild (randomColor + 1).localPosition = new Vector2 (-0.3f, 0.0f);
			startWidth = 2.4f;
			endWidth = 1.0f;
			position = new Vector2 (-0.3f, 0.0f);
			StartCoroutine(enableTrail());

		}

	}	
	IEnumerator enableTrail(){

		yield return new WaitForSeconds (0.3f);

		transform.GetChild (randomColor + 1).GetComponent<TrailRenderer> ().enabled = true;
		enabledTrail = randomColor + 1;

	}
}
