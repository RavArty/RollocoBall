using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircleHelp : MonoBehaviour {

	public static MoveCircleHelp instance;

	public  Transform center;
	public GameObject hand;
	private bool moveLine = false;
	private Rigidbody2D rb;
	public float speed = 2;
	public GameObject glow;
	public GameObject barier;


	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();

	}

	void FixedUpdate(){
		if (moveLine) {
			// move line horizontally
			rb.MovePosition (new Vector3(transform.position.x, Mathf.Lerp (transform.position.y, center.transform.position.y, Time.deltaTime * 10 * speed), transform.position.z)  + Vector3.right * Time.deltaTime * 10 * speed);
				
		}
	}

	void OnTriggerEnter2D(Collider2D coll){

		if (coll.name == "lineDownFirst") {
	
			moveLine = false;
			StartCoroutine (animateHand ());
		}

		if (coll.name == "lineDown") {

			moveLine = true;
		
		}

	}	public void moveCircle(){
		moveLine = true;
		FadeObject.instance.FadeOut(barier, 0.1f);
		StartCoroutine(FadeOutGlow());
	}

	IEnumerator FadeOutGlow(){
		yield return new WaitForSeconds (0.5f);
		FadeObject.instance.FadeOut(glow, 0.3f);

	}

	IEnumerator animateHand(){
		yield return new WaitForSeconds (0.7f);
		hand.GetComponent<Animator> ().SetTrigger ("Pressed");
	}
}
