using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScore : MonoBehaviour {

	public static ShopScore instance;

	void Awake(){
		if (instance == null) {
			instance = this;
		}else{
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
		transform.GetComponent<Text>().text = TotalData.total.totalScore.ToString();
		if (TotalData.total.totalScore > 999) {
			transform.localPosition = new Vector2 (transform.localPosition.x + 17.0f, transform.localPosition.y);
		}
	}



	public void notEnoughMoney(){
		transform.GetComponent<Animator> ().SetTrigger ("Bounce");
	}

	public void checkScore(int current, int target, float duration){
		gameObject.GetComponent<CountTo> ().fireCountTo (current, target, duration);
	
	}

}
