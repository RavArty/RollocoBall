using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;


public class TotalData {

	public static Total total;
	//	public static int coins = 0;


	/// <summary>
	/// The name of the file.
	/// </summary>
	private static string fileName = "rollocoball.bin";

	/// <summary>
	/// The path of the file.
	/// </summary>
	private static string path;

	/// <summary>
	/// Whether the Worlds data is empty or null.
	/// </summary>
	private static bool isNullOrEmpty;



	///LevelData is class used for persistent loading and saving
	[System.Serializable]
	public class Total
	{
		public bool firstGame = true;
		public int totalScore = 0;
		public int lastScore = 0;
		public int bestScore = 0;
		public bool noAds = false;
		public bool isSoundOn = true;
		public bool isMusicOn = true;
		//	public Sprite playerSprite;
		public int playerID = 1;
		//	public HoldPlayers players = new HoldPlayers();
		public List<Players> players = new List<Players>();

		public Players FindPlayerByID(int ID){
			foreach (Players player in players) {
				if (player.ID == ID) {
					return player;
				}
			}
			return null;
		}

	}



	[System.Serializable]
	public class Players{
		public int ID = 0;
		public bool isActive = false;
		public bool isLocked = true;
		public int price = 0;
	}

	/// <summary>
	/// Save the worlds data to the file.
	/// </summary>
	/// <param name="worldsData">Worlds data.</param>
	public static void SaveTotalToFile ()
	{
		Debug.Log ("Saving Total Data");
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (path)) {
			Debug.Log ("Null or Empty path -TotalData-");
			return;
		}

		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (path, FileMode.Create);
			bf.Serialize (file, TotalData.total);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}
	}

	/// <summary>
	/// Load the worlds data from the file.
	/// </summary>
	/// <returns>The Worlds data.</returns>
	public static void LoadTotalFromFile ()
	{
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (path)) {
			Debug.Log ("Null or Empty path");
			//	return null;
		}
		if (!File.Exists (path)) {
			Debug.Log (path + " is not exists");
			TotalData.total = new Total ();
			TotalData.total.firstGame = true;
			TotalData.total.totalScore = 1;
			TotalData.total.bestScore = 0;
			TotalData.total.isMusicOn = true;
			TotalData.total.isSoundOn = true;
			TotalData.total.noAds = false;
			//	TotalData.totalData.lastScore = 0;
			//	TotalData.totalData.players = ObtainPlayersInfo ();
			//	TotalData.totalData.players = null;
			SaveTotalToFile();
			//	CreateFile();
			//	return null;
		}

		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (path, FileMode.Open);
			//			Debug.Log("opened file");
			TotalData.total = (Total)bf.Deserialize (file);
			//			Debug.Log("totalData: " + totalData);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}

		//	return TotalData.total;
	}
	
	/// <summary>
	/// Settings up the path of the file ,relative to the current platform.
	/// </summary>
	private static void SettingUpFilePath ()
	{
		if (Application.platform == RuntimePlatform.Android) {//Android Platform
			path = Application.persistentDataPath;
		} else if (Application.platform == RuntimePlatform.IPhonePlayer) {//IOS Platform
			///Get iPhone Documents Path
			path = Application.persistentDataPath;
			//	string fileBasePath = dataPath.Substring (0, dataPath.Length - 5);
			//	path = fileBasePath.Substring (0, fileBasePath.LastIndexOf ('/')) + "/Documents";
		} else {//Others
			//			Debug.Log("Application.dataPath: " + Application.dataPath);
			path = Application.dataPath;
		}

		path += "/" + fileName;
	}

	private static void CreateFile(){
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (path)) {
			Debug.Log ("Null or Empty path -TotalData-");
			return;
		}

		FileStream file = null;
		try {
			//	Debug.Log("create file");
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (path, FileMode.CreateNew);
			bf.Serialize (file, TotalData.total);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}
	}
}
