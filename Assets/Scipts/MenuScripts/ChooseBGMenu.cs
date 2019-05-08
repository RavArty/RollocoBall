using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBGMenu : MonoBehaviour {

	public GameObject[] bg;
	// Use this for initialization
	void Start () {
		bg [KeepDataOnPlayMode.instance.chooseBGMenu].SetActive (true);
	}
}
