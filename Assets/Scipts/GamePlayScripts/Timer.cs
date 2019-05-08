﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Timer : MonoBehaviour
{
		/// <summary>
		/// Text Component
		/// </summary>
		public Text uiText;

		/// <summary>
		/// The time in seconds.
		/// </summary>
		[HideInInspector]
		public int
				timeInSeconds;

		/// <summary>
		/// Whether the Timer is running
		/// </summary>
	private  bool isRunning;

		void Awake ()
		{
				if (uiText == null) {
						uiText = GetComponent<Text> ();
				}
				///Start the Timer
		//		Start ();
		}

		/// <summary>
		/// Start the Timer.
		/// </summary>
		public void StartTimer ()
		{
//		Debug.Log ("start timer");
				if (!isRunning) {
						isRunning = true;
						timeInSeconds = 0;
						StartCoroutine ("Wait");
				}
		}

		/// <summary>
		/// Stop the Timer.
		/// </summary>
		public void Stop ()
		{
				if (isRunning) {
						isRunning = false;
						StopCoroutine ("Wait");
				}
		}

		public void Restart(){
		if (!isRunning) {
			isRunning = true;
			StartCoroutine ("Wait");
		}
		}

		/// <summary>
		/// Reset the timer.
		/// </summary>
		public void Reset ()
		{
				Stop ();
				StartTimer ();
		}

		/// <summary>
		/// Wait Coroutine.
		/// </summary>
		private IEnumerator Wait ()
		{
		
				while (isRunning) {
						ApplyTime ();
		//	Debug.Log ("wait");
						yield return new WaitForSeconds (1);
						timeInSeconds++;
				}
		}

		/// <summary>
		/// Applies the time into TextMesh Component.
		/// </summary>
		private void ApplyTime ()
		{
				if (uiText == null) {
						return;
				}
				int mins = timeInSeconds / 60;
				int seconds = timeInSeconds % 60;

				uiText.text = ": " + GetNumberWithZeroFormat (mins) + ":" + GetNumberWithZeroFormat (seconds);
		}

		/// <summary>
		/// Get the number with zero format.
		/// </summary>
		/// <returns>The number with zero format.</returns>
		/// <param name="number">Ineger Number.</param>
		public static string GetNumberWithZeroFormat (int number)
		{
				string strNumber = "";
				if (number < 10) {
						strNumber += "0";
				}
				strNumber += number;
		
				return strNumber;
		}
}