using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCurrentImage : MonoBehaviour {



	private  Transform[] fadeObj;
	private  float alphaValue;

	private float fadingOutSpeed;
	private Image[] rendererObjects;
	private Color newColor;



	public  void FadeIn(float fadeTime){


		if (fadeTime != 0) {
			fadingOutSpeed = 1.0f / fadeTime;
		} else {
			fadingOutSpeed = 0;
		}

		StartCoroutine (FadeInCoroutine (0.0f));


	}

	public  void FadeOut(float fadeTime){

		if(fadeTime != 0){
			fadingOutSpeed = 1.0f / fadeTime;
		}else{fadingOutSpeed = 0;}

			StartCoroutine (FadeOutCoroutine (1.0f));

	}

	IEnumerator FadeInCoroutine(float alphaValue) { 
		while( alphaValue < 1.0f){
			if(fadingOutSpeed == 0){
				alphaValue = 1.0f;
		
			}else{
				alphaValue += Time.deltaTime * fadingOutSpeed;

			}
	
			newColor = GetComponent<Renderer>().sharedMaterial.color;
			newColor.a = alphaValue; 
			GetComponent<Renderer>().sharedMaterial.color = newColor;
		
			yield return null;
		}
		newColor.a = 1.0f; 
	
		GetComponent<Renderer>().sharedMaterial.color = newColor;
	}


	IEnumerator FadeOutCoroutine(float alphaValue) { 
		
		while( alphaValue > 0.0f){
			if(fadingOutSpeed == 0){
				alphaValue = 0.0f;
			}else{
				alphaValue -= Time.deltaTime * fadingOutSpeed;
			}
	
			newColor = GetComponent<Renderer>().sharedMaterial.color;
				newColor.a = alphaValue; 
			GetComponent<Renderer>().sharedMaterial.color = newColor;
		
			yield return null;
		}
		newColor.a = 0.0f; 
	
		GetComponent<Renderer>().sharedMaterial.color = newColor;
	}
}
