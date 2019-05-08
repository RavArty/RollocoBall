using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MusicSound : MonoBehaviour {

	public static MusicSound instance;

	[HideInInspector]
	public AudioSource[] audioSources;


	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad(gameObject);
		audioSources = GetComponents<AudioSource> ();
	}

	void Start(){
	
		if(KeepDataOnPlayMode.instance.isMusicOn){
		
			audioSources [1].Play ();	
		} else {
			audioSources [1].Stop ();
		}
	}

	public void PlayMusic(){
		audioSources [1].Play ();	
	}

	public void StopMusic(){
		audioSources [1].Stop ();	
	}

	public void ClickButtonSound(){
	
		if(KeepDataOnPlayMode.instance.isSoundOn){
	
			audioSources [0].Play ();
		} else {
			audioSources [0].Pause ();
		}
	}

		
}
