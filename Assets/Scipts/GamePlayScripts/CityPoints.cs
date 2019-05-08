using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityPoints : MonoBehaviour {

	public static CityPoints instance;
	[HideInInspector]
	public  Vector2 initOffset;
	[HideInInspector]
	public  Vector2[] initOffsetArray = {new Vector2(1.71f, 0.0f),
		new Vector2(0.0f, 0.0f),
		new Vector2(1.3f, 0.0f)};

	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		int i = Random.Range (0, initOffsetArray.Length);
		initOffset = initOffsetArray [i];
	}
}
