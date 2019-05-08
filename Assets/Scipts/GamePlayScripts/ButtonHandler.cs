using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

	private Animator anim;
	private bool exit = false;
	private bool pressed = false;

	void Awake(){
		anim = GetComponent<Animator> ();
	}
	public void pointerDown(){
		anim.SetTrigger ("Pressed");
		exit = false;
		pressed = true;

	}

	public void pointerExit(){
		if (pressed) {
			pressed = false;
			anim.SetTrigger ("Exit");
			exit = true;
		}
	}

	public void pointerUp(){
		if (!exit) {
			anim.SetTrigger ("Up");
			pressed = false;
		}
	}

}
