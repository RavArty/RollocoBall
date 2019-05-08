using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteProject;
using AppAdvisory.social;

public class ButtonSelector : MonoBehaviour {

	public GameObject menu;		//menu that you want to go
	public GameObject menuAds;	//menu for replay to show native ads;
	public GameObject pause;



	public void MainMenu(){
		MusicSound.instance.ClickButtonSound ();
		SelectMenu.instance.selectMenu (menu);
	
		FadingMain.instance.ClearScreen ();
	}

	public void ReplayMenu(){
	
		if (KeepDataOnPlayMode.instance.noAds) {
			SelectMenu.instance.selectMenu (menu);
		}else if(!KeepDataOnPlayMode.instance.noAds){
			ShowNativeAds.instance.showNativeAds ();
			SelectMenu.instance.selectMenu (menuAds);
		}
		FadingMain.instance.ClearScreen ();
	}
		
	public void Help(){
		MusicSound.instance.ClickButtonSound ();
		SelectMenu.instance.selectMenu (menu);
		HelpScript.instance.startHelp ();
		FadingMain.instance.ClearScreen ();
	}

	public void Settings(){
		MusicSound.instance.ClickButtonSound ();
		SelectMenu.instance.selectMenu (menu);
		SetSounds.instance.startSettings ();
		FadingMain.instance.ClearScreen ();
	}

	public void Store(){
		MusicSound.instance.ClickButtonSound ();
		SelectMenu.instance.selectMenu (menu);
		FadingMain.instance.ClearScreen ();
	}

	public void PlayGame(){
		if (!KeepDataOnPlayMode.instance.noAds) {
			ShowBannerAd.instance.bannerView.Hide ();
		}
		MusicSound.instance.ClickButtonSound ();
	
		GetComponent<LoadingOverlay>().loadOverlayTrue();
		RotateCircle.instance.move = false;
		RotateCircle.instance.moveLine = false;
		RotateCircle.instance.moveLineUp = false;
		RotateCircle.instance.moveLineVDown = false;
		GameManager.instance.loadGame ("Main");
	}

	public void PauseGame(){
		MusicSound.instance.ClickButtonSound ();
	
		pause.transform.GetChild(0).transform.localScale = new Vector3(30, 30, 1);
		if (TapToStart.instance.start) {
			if (Time.timeScale > 0) {
				RotateCircle.instance.pausedGame = true;
				pause.GetComponent<Animator> ().SetBool ("isPaused", true);
			} else {
				RotateCircle.instance.pausedGame = false;
				pause.GetComponent<Animator> ().SetBool ("isPaused", false);

			}
			GameManager.instance.pauseGame ();
		}
	}

	public void stopGame(){
			Time.timeScale = 0;
	}




	public void moveCircleAfterAnimation(){
		MoveCircleHelp.instance.moveCircle ();
	}

	public void MainMenuAfterGame(){
		MusicSound.instance.ClickButtonSound (); 
		GetComponent<LoadingOverlay>().loadOverlayTrue();
	
		if (!KeepDataOnPlayMode.instance.noAds) {
			ShowNativeAds.instance.nativeExpressAdView.Hide ();
			ShowBannerAd.instance.bannerView.Show ();
		}
		GameManager.instance.loadMainMenu ("Menu");
	}

	public void EndGame(){
		MusicSound.instance.ClickButtonSound ();
	
		if (!KeepDataOnPlayMode.instance.noAds) {
			ShowNativeAds.instance.nativeExpressAdView.Hide ();
		}
		ReloadGame.instance.reloadGame ();
	}

	public void Leaderboard(){
		MusicSound.instance.ClickButtonSound ();
		LeaderboardManager.ShowLeaderboardUI ();
		LeaderboardManager.ReportScore(TotalData.total.bestScore);
	}

	public void HomeAfterShop(){
		MusicSound.instance.ClickButtonSound ();
		GetComponent<Animator> ().SetTrigger ("Up");
	}

	public void Feedback(){
		MusicSound.instance.ClickButtonSound ();
		#if UNITY_ANDROID
		Application.OpenURL("market://details?id=com.artykov.rollocoball");
		#elif UNITY_IOS
		Application.OpenURL("itms-apps://itunes.apple.com/app/id1191930248");
		#endif
	}



}
