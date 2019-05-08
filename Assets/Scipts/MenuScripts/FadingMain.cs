using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadingMain : MonoBehaviour {

	public static FadingMain instance;
	public SpriteRenderer fadeOutTexture;
	 
	public float fadeSpeed = 1.0f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = -1; 
	private bool sceneStarting = true;
	private float time = 0f;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		if (KeepDataOnPlayMode.instance.firstExecutioin){
			KeepDataOnPlayMode.instance.firstExecutioin = false;
			ClearScreenFirst ();
		}else{
			ClearScreen();
		}

	}

	public void ClearScreenFirst(){
		fadeOutTexture.color = Color.black;
		fadeOutTexture.transform.localScale = new Vector3(30, 30, 1);
		StartCoroutine(ClearScreenRoutineFirst());
	}

	public void ClearScreen(){
		fadeOutTexture.color = Color.black;
		fadeOutTexture.transform.localScale = new Vector3(30, 30, 1);
		StartCoroutine(ClearScreenRoutine());
	}

	IEnumerator ClearScreenRoutineFirst(){

		time = 0.0f;
		yield return null;
		while (time <= 1.0f)
		{
			fadeOutTexture.color = Color.Lerp(fadeOutTexture.color, Color.clear, time);
			
			time += Time.unscaledDeltaTime * fadeSpeed;
			yield return null;
		}
		fadeOutTexture.color = Color.clear;
		
	}
	IEnumerator ClearScreenRoutine(){

		time = 0.0f;
		yield return null;
		while (time <= 1.0f)
		{
			fadeOutTexture.color = Color.Lerp(fadeOutTexture.color, Color.clear, time);
		
			time += Time.fixedDeltaTime * fadeSpeed;
			yield return null;
		}
		fadeOutTexture.color = Color.clear;
	
	}

	public void FillScreen(){
		fadeOutTexture.color = Color.clear;
		fadeOutTexture.transform.localScale = new Vector3(30, 30, 0);
		StartCoroutine(FillScreenRoutine());
	}

	IEnumerator FillScreenRoutine(){

		time = 0.0f;
		yield return null;
		while (time <= 1.0f)
		{
			fadeOutTexture.color = Color.Lerp(fadeOutTexture.color, Color.black, time);

			time += Time.fixedDeltaTime * (1.0f / fadeSpeed);
			yield return null;
		}
		fadeOutTexture.color = Color.black;
		
	}
}
