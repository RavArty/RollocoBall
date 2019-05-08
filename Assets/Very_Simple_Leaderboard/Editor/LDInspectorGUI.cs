
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System;

namespace AppAdvisory.social
{


	[CustomEditor(typeof(LEADERBOARDIDS))]
	public class LDInspectorGUI : Editor
	{

		void SetScriptingSymbol(string symbol, BuildTargetGroup target, bool isActivate)
		{
////			if(target == BuildTargetGroup.Unknown)
////				return;
////
////			var type = target.GetType();
////			var memInfo = type.GetMember(target.ToString());
////			var attributes = memInfo[0].GetCustomAttributes(typeof(BuildTargetGroup), false);
////			bool continueBTG = attributes.Length > 0;
////
////			if(!continueBTG)
////				return;
//			
//			var s = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);
//
//			s = s.Replace(symbol + ";","");
//
//			s = s.Replace(symbol,"");
//
//			if(isActivate)
//				s = symbol + ";" + s;
//
//			PlayerSettings.SetScriptingDefineSymbolsForGroup(target,s);

			var s = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);

			if(isActivate && (s.Contains(symbol) || s.Contains(symbol + ";")))
				return;
			//			
			s = s.Replace(symbol + ";","");

			s = s.Replace(symbol,"");

			if(isActivate)
				s = symbol + ";" + s;

			PlayerSettings.SetScriptingDefineSymbolsForGroup(target,s);
		}

		public bool Enable_iOS
		{
			get
			{
				bool _bool = t.ENABLE_IOS;

				return _bool;
			}

			set
			{
				bool _bool = t.ENABLE_IOS;

				if(_bool == value)
					return;

				t.ENABLE_IOS = value;

				SetScriptingSymbol("VSLEADERBOARD_ENABLE_IOS", BuildTargetGroup.iOS, value);
			}
		}

		public bool Enable_Android
		{
			get
			{
				bool _bool = t.ENABLE_ANDROID;

				return _bool;
			}

			set
			{
				bool _bool = t.ENABLE_ANDROID;

				if(_bool == value)
					return;

				t.ENABLE_ANDROID = value;

				SetScriptingSymbol("VSLEADERBOARD_ENABLE_ANDROID", BuildTargetGroup.Android, value);
			}
		}

		LEADERBOARDIDS t;

		public override void OnInspectorGUI()
		{
			t = (LEADERBOARDIDS)target;

			if(t.FIRST_TIME)
			{
				Debug.Log("*********** APP_ADVISORY_FIRST_TIME_LEADERBORD ***********");

				PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, SetString("VSLEADERBOARD"));

				PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, SetString("VSLEADERBOARD"));

				t.FIRST_TIME = false;
			}

			Enable_iOS = EditorGUILayout.BeginToggleGroup(new GUIContent("Enable iOS Game Center   [?]", "Activate if you want to use Game Center Leaderboard"), Enable_iOS);
			EditorGUILayout.EndToggleGroup();

			Enable_Android = EditorGUILayout.BeginToggleGroup(new GUIContent("Enable Android Google Play Game Services   [?]", "Activate if you want to use Google Play Game Services Leaderboard"), Enable_Android);
			EditorGUILayout.EndToggleGroup();

			#if VSLEADERBOARD_ENABLE_IOS
			var stringIos = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
			if(!stringIos.Contains("APPADVISORY_LEADERBOARD"))
			{
			stringIos = "APPADVISORY_LEADERBOARD" + ";" + stringIos;

			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS,stringIos);
			}
			#endif

			#if VSLEADERBOARD_ENABLE_ANDROID
			var stringAndroid = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
			if(!stringAndroid.Contains("APPADVISORY_LEADERBOARD"))
			{
			stringAndroid = "APPADVISORY_LEADERBOARD" + ";" + stringAndroid;

			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android,stringAndroid);
			}
			#endif

			#if VSLEADERBOARD_ENABLE_ANDROID
			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button("GET\nGoogle Play Game\nSDK",  GUILayout.Width(150), GUILayout.Height(50)))
			{
			Application.OpenURL("https://github.com/playgameservices/play-games-plugin-for-unity");
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			#endif

			#if VSLEADERBOARD_ENABLE_IOS
			EditorGUILayout.LabelField(new GUIContent("Game Center Leaderboard Id   [?]", "Find it on Itunes Connect console"));
			t.LEADERBOARDID_IOS = EditorGUILayout.TextField(t.LEADERBOARDID_IOS);
			#endif


			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			#if VSLEADERBOARD_ENABLE_ANDROID
			EditorGUILayout.LabelField(new GUIContent("Google Play Game Leaderboard Id   [?]", "Find it on Google Play Game console"));
			t.LEADERBOARDID_ANDROID = EditorGUILayout.TextField(t.LEADERBOARDID_ANDROID);
			#endif

			if (GUI.changed)
			{
				EditorUtility.SetDirty(t); 
			}
		}

		string SetString(string stringIOS)
		{
			stringIOS = stringIOS.Replace(stringIOS + ";","");

			stringIOS = stringIOS.Replace(stringIOS,"");

			return stringIOS;
		}

	}
}