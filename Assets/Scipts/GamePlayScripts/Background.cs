using UnityEngine;

//This script controls the scrolling of the background
public class Background : MonoBehaviour
{
	public float speed = 0.1f;			//Speed of the scrolling
	public Timer timer;
	private bool firstLaunch = true;
	private float timeCount = 0.0f;

	void Start(){
		GetComponent<MeshRenderer> ().sortingLayerName = "BGCity";
		GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", CityPoints.instance.initOffset);
	}
	void Update ()
	{
		if (TapToStart.instance.start) {
			//Keep looping between 0 and 1
			timeCount += Time.deltaTime;
			float x = Mathf.Repeat (timeCount * speed, 1);
		
			//Create the offset
			Vector2 offset = new Vector2 (x + CityPoints.instance.initOffset.x, 0);
			//Apply the offset to the material
			GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", offset);
		}
	}
}