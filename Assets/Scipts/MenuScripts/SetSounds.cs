using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSounds : MonoBehaviour {

	public static SetSounds instance;

	public Sprite soundsOff;
	public Sprite musicOff;
	public GameObject soundBttn;
	public GameObject musicBttn;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else{
			Destroy (gameObject);
		}
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}

	public void startSettings(){
		TotalData.LoadTotalFromFile ();
	
		if (!TotalData.total.isSoundOn) {
			soundBttn.GetComponent<Button> ().image.overrideSprite = soundsOff;
		}

		if (!TotalData.total.isMusicOn) {
			musicBttn.GetComponent<Button> ().image.overrideSprite = musicOff;
		}
	}

	public void pressedMusicBttn(){
		MusicSound.instance.ClickButtonSound ();
		TotalData.LoadTotalFromFile ();
		if (TotalData.total.isMusicOn) {
			OffMusic ();
		}else{
			OnMusic ();
		}
	}

	void OffMusic(){
		musicBttn.GetComponent<Animator> ().SetTrigger ("Off");
		TotalData.total.isMusicOn = false;
		KeepDataOnPlayMode.instance.isMusicOn = false;
		MusicSound.instance.StopMusic ();
		TotalData.SaveTotalToFile ();
	}

	void OnMusic(){
		musicBttn.GetComponent<Animator> ().SetTrigger ("On");
		musicBttn.GetComponent<Button> ().image.overrideSprite = null;
		MusicSound.instance.PlayMusic ();
		TotalData.total.isMusicOn = true;
		KeepDataOnPlayMode.instance.isMusicOn = true;
		TotalData.SaveTotalToFile ();
	}

	public void pressedSoundBttn(){
		MusicSound.instance.ClickButtonSound ();
		TotalData.LoadTotalFromFile ();
		if (TotalData.total.isSoundOn) {
			OffSound ();
		}else{
			OnSound ();
		}
		TotalData.SaveTotalToFile ();
	}

	void OffSound(){
		soundBttn.GetComponent<Animator> ().SetTrigger ("Off");
		TotalData.total.isSoundOn = false;
		KeepDataOnPlayMode.instance.isSoundOn = false;
		TotalData.SaveTotalToFile ();
	}

	void OnSound(){
		soundBttn.GetComponent<Animator> ().SetTrigger ("On");
		soundBttn.GetComponent<Button> ().image.overrideSprite = null;
		TotalData.total.isSoundOn = true;
		KeepDataOnPlayMode.instance.isSoundOn = true;
		TotalData.SaveTotalToFile ();
	}
}
