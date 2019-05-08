using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class RotateCircle : MonoBehaviour {

	public static RotateCircle instance;


	public bool isGame = true;
	public Sprite firstGameSprite;
	public Transform cameraPos;
	public GameObject destroyPrefab;
	public GameObject barDestroyPrefab;
	[HideInInspector]
	public Vector3 savedCameraPos;
	public float speed = 2;
	public  Transform center;
	private  Transform radius;
	[HideInInspector]
	public bool move = false;
	[HideInInspector]
	public bool moveLine = false;
	[HideInInspector]
	public bool moveLineUp = false;
	[HideInInspector]
	public bool moveLineVDown = false;
	private int sign = 1;
	public Vector3 size;
	private Rigidbody2D rb;
	public bool barrier = false;
	public bool wrongColor = false;
	private bool jumped = false;
	private float timecounter;
	private Vector3 dir;
	private float angle;
	private float rad;
	private int plus90 = 0;    //if "1" +90, "-1" -90
	private bool firstTimeDetector = false;
	private bool isSavedOnce = false;
	public bool pausedGame = false;




	private bool firstDetect = false;
	// Use this for initialization


	void Awake(){
		if (instance == null) {
			instance = this;
		}else{
			Destroy (gameObject);
		}
	}

	void Start () {
		rb = GetComponent<Rigidbody2D>();

	}

	public void stopPlayer(){
		moveLine = false;
		moveLineUp = false;
		move = false;
	}
	void FixedUpdate(){

		if (moveLine) {
			// move line horizontally
			if (!barrier && !wrongColor) {
			
				rb.MovePosition (new Vector3(transform.position.x, Mathf.Lerp (transform.position.y, center.transform.position.y, Time.fixedDeltaTime * 10 * speed), transform.position.z)  + Vector3.right * Time.deltaTime * 10 * speed * sign);
				rb.MoveRotation (0);
			}
		}

		if (moveLineUp) {
			//	move line vertically
			if (!barrier && !wrongColor) {
			    rb.MovePosition (new Vector3( Mathf.Lerp (transform.position.x, center.transform.position.x, Time.fixedDeltaTime * 10 * speed), transform.position.y, transform.position.z)  + Vector3.up * Time.deltaTime * 10 * speed);
				rb.MoveRotation (90);
				
			}
		}

		if (moveLineVDown) {
			//	move line vertically down
			if (!barrier && !wrongColor) {
				rb.MovePosition (new Vector3( Mathf.Lerp (transform.position.x, center.transform.position.x, Time.fixedDeltaTime * 10 * speed), transform.position.y, transform.position.z)  + Vector3.down * Time.deltaTime * 10 * speed);
				rb.MoveRotation (-90);
				
			}
		}


		if (move) {
			// move along the circle
			if (!barrier && !wrongColor) {
				
				timecounter += Time.fixedDeltaTime * speed * sign;

				float x = center.position.x +  Mathf.Cos (timecounter) * Vector3.Distance(radius.position, center.position);
				float y = center.position.y +  Mathf.Sin (timecounter) * Vector3.Distance(radius.position, center.position);
                rb.MovePosition (new Vector3 (x, y, 0) + Vector3.forward *sign * Time.fixedDeltaTime * 100 * speed);
			    
                if (plus90 == -1) {
					rb.MoveRotation (timecounter * Mathf.Rad2Deg - 90);
				} else if(plus90 == 1){
					rb.MoveRotation (timecounter * Mathf.Rad2Deg + 90);
				}
			}
		}
	}

	public void IncreaseSpeed(){
		speed *= 1.5f;
	}

	IEnumerator endGameIEnumerator(){
		yield return new WaitForSeconds (2);
		Score.instance.saveScore ();
	
		if (KeepDataOnPlayMode.instance.randomAds == KeepDataOnPlayMode.instance.reloadedTimes) {
			if (!KeepDataOnPlayMode.instance.noAds) {
				KeepDataOnPlayMode.instance.reloadedTimes = 0;
				KeepDataOnPlayMode.instance.randomAds =  KeepDataOnPlayMode.instance.generateIntAds ();
				ShowAd ();
			}
		}
		GetComponent<ButtonSelector> ().ReplayMenu ();

	}

	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}

	void Update () {
		
		if (barrier || wrongColor) {
			
			StartCoroutine (endGameIEnumerator ());
		
		}
		if (isGame) {
			#if UNITY_EDITOR
			if (Input.GetButtonDown ("Jump")) {
			
				if (TapToStart.instance.start) {
					if (!barrier) {
						if (!pausedGame) {
							jumped = true;
						}
					}

				}
			}
			#else
			
			#endif
		}

		


	}

		
	void OnTriggerEnter2D(Collider2D coll){
		
		if(coll.name == "barier"){
			KeepDataOnPlayMode.instance.reloadedTimes += 1;
			KeepDataOnPlayMode.instance.reloadedTimesRewardVideo += 1;
		
			transform.GetComponent<SpriteRenderer> ().sprite = null;
			transform.GetChild(0).transform.GetComponent<SpriteRenderer> ().sprite = null;
		    GameObject destroyEffect = ObjectPool.current.GetObject (destroyPrefab);
			destroyEffect.transform.position = transform.position;
			destroyEffect.SetActive (true);
			destroyEffect.GetComponent<PlaySound> ().playSound ();
		
			barrier = true;
		}
		if (coll.name == "up") {
			coll.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
		}

		if (coll.name == "upDown") {
			coll.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			coll.transform.parent.GetChild (2).gameObject.SetActive(false);
		}

		if (coll.name == "lineDownFirst") {
			TapToStart.instance.start = false;
			TapToStart.instance.startForHelp = true;
			TapToCompleteHelp.instance.FadeInHand ();
			move = false;
			moveLine = false;
			moveLineUp = false;
			moveLineVDown = false;
			firstTimeDetector = true;
			
		}



		if (coll.name == "down") {
			activateLine (coll.gameObject);
			dir = transform.position - coll.transform.parent.GetChild (5).transform.position;
			angle = Vector3.Angle( Vector3.right, dir);
			rad = angle * Mathf.Deg2Rad;

		

			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);
			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive(false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			}
			timecounter = -rad;
			plus90 = -1; 
			sign = -1;
			firstDetect = true;
			move = true;
			moveLine = false;
			moveLineUp = false;
			moveLineVDown = false;

			radius = coll.transform;
			center = coll.transform.parent.GetChild (5).transform;
		}
		if (coll.name == "down2") {
			activateLine (coll.gameObject);
			dir = transform.position - coll.transform.parent.GetChild (5).transform.position;
		    angle = Vector3.Angle( Vector3.right, dir);
			rad = angle * Mathf.Deg2Rad;
		
			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);

			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive(false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			}
			firstDetect = true;
			timecounter = rad;
			plus90 = -1; 
			sign = -1;
			move = true;
			moveLine = false;
			moveLineUp = false;
			moveLineVDown = false;
		
			radius = coll.transform;
			center = coll.transform.parent.GetChild (5).transform;
		}

		if (coll.name == "down3") {
			activateLine (coll.gameObject);
			dir = transform.position - coll.transform.parent.GetChild (5).transform.position;
			angle = Vector3.Angle( Vector3.right, dir );
			rad = angle * Mathf.Deg2Rad;
		

			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);
			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive(false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			}
			firstDetect = true;
			timecounter = rad;
			plus90 = -1; 
			sign = -1;
			move = true;
			moveLine = false;
			moveLineUp = false;
			moveLineVDown = false;
			radius = coll.transform;
			center = coll.transform.parent.GetChild (5).transform;
		}

		if (coll.name == "down4") {
			activateLine (coll.gameObject);
			dir = transform.position - coll.transform.parent.GetChild (5).transform.position;
			angle = Vector3.Angle( Vector3.right, dir);
			rad = angle * Mathf.Deg2Rad;
		

			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);
			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive(false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			}
			firstDetect = true;
			timecounter = -rad;
			plus90 = -1; 
			sign = -1;
			move = true;
			moveLine = false;
			moveLineUp = false;
			moveLineVDown = false;
			radius = coll.transform;
			center = coll.transform.parent.GetChild (5).transform;
		}

		if (coll.name == "down5") {
			activateLine (coll.gameObject);
			dir = transform.position - coll.transform.parent.GetChild (5).transform.position;
			angle = Vector3.Angle( Vector3.right, dir );
			rad = angle * Mathf.Deg2Rad;
	

			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);
			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive(false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			}
			firstDetect = true;
			timecounter = -rad;
			plus90 = 1; 
			sign = 1;
			move = true;
			moveLine = false;
			moveLineUp = false;
			moveLineVDown = false;
		
			radius = coll.transform;
			center = coll.transform.parent.GetChild (5).transform;
		}

		if (coll.name == "down6") {
			activateLine (coll.gameObject);
			dir = transform.position - coll.transform.parent.GetChild (5).transform.position;
			angle = Vector3.Angle( Vector3.right, dir);
			rad = angle * Mathf.Deg2Rad;
	


			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);
			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive(false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			}
			firstDetect = true;
			timecounter = rad;
			plus90 = 1; 
			sign = 1;
			move = true;
			moveLine = false;
			moveLineUp = false;
			moveLineVDown = false;
		
			radius = coll.transform;
			center = coll.transform.parent.GetChild (5).transform;
		}

		if (coll.name == "down7") {
			activateLine (coll.gameObject);
			dir = transform.position - coll.transform.parent.GetChild (5).transform.position;
			angle = Vector3.Angle( Vector3.right, dir );
			rad = angle * Mathf.Deg2Rad;



			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);
			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive(false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			}
			firstDetect = true;
			timecounter = rad;
			plus90 = 1; 
			sign = 1;
			move = true;
			moveLine = false;
			moveLineUp = false;
			moveLineVDown = false;
		
			radius = coll.transform;
			center = coll.transform.parent.GetChild (5).transform;
		}

		if (coll.name == "down8") {
			activateLine (coll.gameObject);
			dir = transform.position - coll.transform.parent.GetChild (5).transform.position;
			angle = Vector3.Angle( Vector3.right, dir);
			rad = angle * Mathf.Deg2Rad;
	

			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);
			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive(false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;			}
			firstDetect = true;
			timecounter = -rad;
			plus90 = 1; 
			sign = 1;
			move = true;
			moveLine = false;
			moveLineUp = false;
			moveLineVDown = false;
	
			radius = coll.transform;
			center = coll.transform.parent.GetChild (5).transform;
		}

		if (coll.name == "lineDown") {
	
			activateLine (coll.gameObject);
			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);
			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive (false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			}
			center = null;
			center = coll.transform.parent.GetChild (5).transform;
			move = false;
			moveLine = true;
			moveLineUp = false;
			moveLineVDown = false;
			sign = 1;

		}

		if (coll.name == "lineDownLeft") {
		
			activateLine (coll.gameObject);
			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);
			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive (false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			}
			center = null;
			center = coll.transform.parent.GetChild (5).transform;
			move = false;
			moveLine = true;
			moveLineUp = false;
			moveLineVDown = false;
			sign = -1;

		}

		if (coll.name == "lineUp") {
			activateLine (coll.gameObject);

			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);
			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive (false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			}

			move = false;
			moveLine = false;
			moveLineUp = true;
			moveLineVDown = false;
			center = coll.transform.parent.GetChild (5).transform;
	
		}

		if (coll.name == "lineVDown") {
			activateLine (coll.gameObject);
			if (coll.GetComponent<DownDetector> ().nextLine != null) {
				coll.GetComponent<DownDetector> ().nextLine.SetActive(true);
			}
			if (coll.GetComponent<DownDetector> ().previousLine != null) {
				coll.GetComponent<DownDetector> ().previousLine.SetActive (false);
				coll.GetComponent<DownDetector> ().previousLine.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = false;
			}

			move = false;
			moveLine = false;
			moveLineUp = false;
			moveLineVDown = true;
			center = coll.transform.parent.GetChild (5).transform;

		}
	}

	void OnTriggerStay2D(Collider2D coll){
	
		if (coll.GetComponent<SpriteRenderer> ()) {
			Color32 colliderColor = coll.GetComponent<SpriteRenderer> ().color;
			Color32 playerColor = transform.GetComponent<SpriteRenderer> ().color;
			if(playerColor.r == colliderColor.r &&
				playerColor.g == colliderColor.g &&
				playerColor.b == colliderColor.b &&
				playerColor.a == colliderColor.a){
				if (jumped) {
					jumped = false;
				    if (coll.transform.parent.gameObject.transform.GetChild (4).gameObject.GetComponent<CircleCollider2D> ()) {
						if (coll.transform.parent.gameObject.transform.GetChild (4).gameObject.GetComponent<CircleCollider2D> ().enabled) {
							Score.instance.changeScore (1);
							removeBarriers (coll.transform.parent.gameObject.transform.GetChild (4).gameObject);
						}

					}
				}

			} else if(playerColor.r != colliderColor.r ||
				playerColor.g != colliderColor.g ||
				playerColor.b != colliderColor.b ||
				playerColor.a != colliderColor.a){
				if (jumped) {
					if (!isSavedOnce) {
						if (KeepDataOnPlayMode.instance.isSoundOn) {
							GetComponent<AudioSource> ().Play ();
						}
						isSavedOnce = true;
		
						KeepDataOnPlayMode.instance.reloadedTimes += 1;
						KeepDataOnPlayMode.instance.reloadedTimesRewardVideo += 1;
                }

					savedCameraPos = cameraPos.localPosition;
					wrongColor = true;
			
				}
			}
		} 
	}

	IEnumerator saveScore(){
		yield return new WaitForSeconds (0.5f);
		Score.instance.saveScore ();

	}

	void activateLine(GameObject line){
		line.transform.parent.GetChild (0).transform.GetComponent<EdgeCollider2D> ().enabled = true;
		
	}

	void removeBarriers(GameObject bar){

		bar.GetComponent<CircleCollider2D> ().enabled = false;
		bar.SetActive(false);
		
			GameObject destroyEffect = ObjectPool.current.GetObject (barDestroyPrefab);
			
			destroyEffect.transform.position = bar.transform.position;
			destroyEffect.SetActive (true);
			destroyEffect.GetComponent<PlaySound> ().playSound ();
			destroyEffect.GetComponent<Deactivate> ().startDeactivation ();
		
	}
		
}
