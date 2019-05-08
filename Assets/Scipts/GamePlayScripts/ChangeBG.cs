using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBG : MonoBehaviour {


	public GameObject[] bg;
	public Timer timer;
	private int previousTime = 0;
	private int orderID = 0;
	private Vector2 newOffset;

	int i = 0;


	// Use this for initialization
	void Start () {
		
		i = 0;
		bg [0].GetComponent<Renderer>().sortingOrder = orderID;
		bg[0].GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", CityPoints.instance.initOffset);

		Color zeroAlpha1 = bg[1].GetComponent<Renderer>().sharedMaterial.color;
		zeroAlpha1.a = 0f; 
		bg[1].GetComponent<Renderer>().sharedMaterial.color = zeroAlpha1;
		bg[1].GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", CityPoints.instance.initOffset);

		Color zeroAlpha2 = bg[2].GetComponent<Renderer>().sharedMaterial.color;
		zeroAlpha2.a = 0f; 
		bg[2].GetComponent<Renderer>().sharedMaterial.color = zeroAlpha2;
		bg[2].GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", CityPoints.instance.initOffset);

		Color zeroAlpha3 = bg[3].GetComponent<Renderer>().sharedMaterial.color;
		zeroAlpha3.a = 0f; 
		bg[3].GetComponent<Renderer>().sharedMaterial.color = zeroAlpha3;
		bg[3].GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", CityPoints.instance.initOffset);

		Color zeroAlpha4 = bg[4].GetComponent<Renderer>().sharedMaterial.color;
		zeroAlpha4.a = 0f; 
		bg[4].GetComponent<Renderer>().sharedMaterial.color = zeroAlpha4;
		bg[4].GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", CityPoints.instance.initOffset);

		Color zeroAlpha5 = bg[5].GetComponent<Renderer>().sharedMaterial.color;
		zeroAlpha5.a = 0f; 
		bg[5].GetComponent<Renderer>().sharedMaterial.color = zeroAlpha5;
		bg[5].GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", CityPoints.instance.initOffset);


			
		StartCoroutine (CheckTimer ());
	}

	IEnumerator  CheckTimer(){
		while (true) {
			if (!RotateCircle.instance.wrongColor && !RotateCircle.instance.barrier) {
				if (timer.timeInSeconds - previousTime == 10) {
					if (i == bg.Length - 1) {
					
						bg [0].GetComponent<Renderer>().enabled = true;
						orderID += 1;
						bg [0].GetComponent<Renderer>().sortingOrder = orderID;
						newOffset = bg[i].GetComponent<Renderer> ().sharedMaterial.GetTextureOffset("_MainTex");
						bg[0].GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", newOffset);
			
						StartCoroutine (deactivateBG (bg [i]));
						bg[0].GetComponent<FadeCurrentImage> ().FadeIn (1.0f);
					
					} else {
				
						bg [i + 1].GetComponent<Renderer>().enabled = true;
						orderID += 1;
						bg [i + 1].GetComponent<Renderer> ().sortingOrder = orderID;
						newOffset = bg[i].GetComponent<Renderer> ().sharedMaterial.GetTextureOffset("_MainTex");
					
						bg[i + 1].GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", newOffset);
			
						StartCoroutine (deactivateBG (bg [i]));
						bg[i + 1].GetComponent<FadeCurrentImage> ().FadeIn (1.0f);
					
					}

					if (i == bg.Length - 1) {
						i = 0;
					} else {
						i += 1;
					}
					previousTime = timer.timeInSeconds;
				}
			}

			yield return new WaitForSeconds (1);
		
		}
	}

	IEnumerator deactivateBG(GameObject bg){
		yield return new WaitForSeconds(1.0f);
		bg.GetComponent<Renderer>().enabled = false;
	}

}