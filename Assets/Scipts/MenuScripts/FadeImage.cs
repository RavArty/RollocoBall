using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour {

	public static FadeImage instance;

	private  Transform[] fadeObj;
	private  float alphaValue;

	private float fadingOutSpeed;
	private Image[] rendererObjects;
	private Color newColor;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}

	}

	public  void FadeIn(GameObject obj, float fadeTime){
		rendererObjects = obj.GetComponentsInChildren<Image> ();
	

		if (fadeTime != 0) {
			fadingOutSpeed = 1.0f / fadeTime;
		} else {
			fadingOutSpeed = 0;
		}
		for (int i = 0; i < rendererObjects.Length; i++) {
			StartCoroutine (FadeIn (rendererObjects [i], rendererObjects [i].GetComponent<Image> ().material.color.a));
		}

	}

	public  void FadeOut(GameObject obj, float fadeTime){
		rendererObjects = obj.GetComponentsInChildren<Image>();

		if(fadeTime != 0){
			fadingOutSpeed = 1.0f / fadeTime;
		}else{fadingOutSpeed = 0;}
		for(int i = 0; i < rendererObjects.Length; i++){
		
			StartCoroutine(FadeOut(rendererObjects[i], rendererObjects[i].GetComponent<Image>().material.color.a));
		}
	}

	IEnumerator FadeIn(Image obj, float alphaValue) { 
	
		while( alphaValue < 1.0f){
			if(fadingOutSpeed == 0){
				alphaValue = 1.0f;

			}else{
				alphaValue += Time.deltaTime * fadingOutSpeed;
	
			}
			if(obj != null){
				newColor = obj.GetComponent<Image>().material.color;
				newColor.a = Mathf.Max ( newColor.a, alphaValue ); 
				obj.GetComponent<Image>().color = newColor;
				
			}
			yield return null;
		}
		newColor.a = 1.0f; 
		if(obj != null)
			obj.GetComponent<Image>().color = newColor;
	
	}


	IEnumerator FadeOut(Image obj, float alphaValue) { 

		while( alphaValue > 0.0f){
			if(fadingOutSpeed == 0){
				alphaValue = 0.0f;
			}else{
				alphaValue -= Time.deltaTime * fadingOutSpeed;
			}
			if(obj != null){
				newColor = obj.GetComponent<Image>().material.color;
				newColor.a = Mathf.Min ( newColor.a, alphaValue ); 
				obj.GetComponent<Image>().color = newColor;
				
			}
			yield return null;
		}
		newColor.a = 0.0f; 
		if(obj != null)
			obj.GetComponent<Image>().color = newColor;
		
	}
}
