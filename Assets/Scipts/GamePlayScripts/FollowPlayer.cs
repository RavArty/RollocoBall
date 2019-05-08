using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {



	public GameObject player;





	public float xMargin = 1f;
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f;
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	private float distanceToTarget;
	private float targetX = 0;
	private float targetY = 0;
	private float previousX = -4.6f;
	private float freezeX = 0;
	private bool isMoveCameraX = true;


	void Start(){
		distanceToTarget = (transform.position.x - player.transform.position.x) ;

	}
	bool CheckYMargin()
	{
	
		return Mathf.Abs(transform.position.y - player.transform.position.y) > yMargin;

	}

	bool CheckXMargin()
	{

		return Mathf.Abs(transform.position.x - player.transform.position.x) > xMargin;

	}
		

	void FixedUpdate ()
	{
			TrackPlayer ();
	}


	void TrackPlayer ()
	{

	
		if (player.transform.position.x < previousX) {
			isMoveCameraX = false;
		
				freezeX = transform.position.x;
		
		} else {
			if (player.transform.position.x > transform.position.x) {
				isMoveCameraX = true;
			}
		}

		previousX = player.transform.position.x;

		targetX = transform.position.x;
		targetY = transform.position.y;

		if (isMoveCameraX) {
			if (CheckXMargin ()) {
				targetX = Mathf.Lerp (transform.position.x, player.transform.position.x, xSmooth * Time.deltaTime);
			}
		}

		if(CheckYMargin()){
			targetY = Mathf.Lerp(transform.position.y, player.transform.position.y, ySmooth * Time.deltaTime);
		}

		if (isMoveCameraX) {
			transform.position = new Vector3 (targetX, targetY, -10);
		} else {
			transform.position = new Vector3 (freezeX, targetY, -10);
		}
	}

}
