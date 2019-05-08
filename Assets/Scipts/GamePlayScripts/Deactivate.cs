using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour {


	public void startDeactivation(){
		StartCoroutine (deactivateObject ());
	}

	IEnumerator deactivateObject(){
		yield return new WaitForSeconds (1.0f);
		ObjectPool.current.PoolObject (gameObject);
	}
}
