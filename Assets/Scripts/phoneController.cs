using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class phoneController : NetworkBehaviour {
	private Transform Reticle;

	private float holdTime = 1f; 
	private float acumTime = 0;
	private Vector2 touchPosStart;
	Renderer renderer;
	Transform acc;
	Transform gyro;
	Gyroscope g;
	CanvasGroup pauseMenu;
	void Start () {
		
		renderer = GameObject.Find ("camera parent").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>();
		renderer.enabled = false;
		pauseMenu = GameObject.Find ("CanvasGroup").GetComponent<CanvasGroup> ();
		if (isClient) {
			Screen.orientation = ScreenOrientation.Portrait;
			Destroy (GameObject.Find ("GvrViewerMain"));
			//Destroy(GameObject.Find ("camera parent"));
			Destroy (GameObject.Find ("Room"));
			Destroy (GameObject.Find ("ball"));
			Destroy (GameObject.Find ("table"));
			Destroy (GameObject.Find ("table"));
			Destroy (GameObject.Find ("racket2"));
			Destroy (GameObject.Find ("Player"));
		} else {
			Screen.orientation = ScreenOrientation.Landscape;
			GameObject.Find ("GvrViewerMain").GetComponent<GvrViewer> ().VRModeEnabled=true;
		}
		acc = transform.FindChild ("accelerometer");
		gyro = transform.FindChild ("gyroscope");
		g=Input.gyro;
		g.enabled = true;	
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (isLocalPlayer) {
			gyro.localPosition = new Vector3(g.attitude.eulerAngles.x,g.attitude.eulerAngles.y,g.attitude.eulerAngles.z);
			acc.localPosition = g.userAcceleration;
		}
	}
	void Update(){
		
		if(Input.touchCount > 0)
		{
			bool longTouch = false;
			bool swipe = false;

			acumTime += Input.GetTouch(0).deltaTime;
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				touchPosStart = Input.GetTouch (0).position;
			}

			if(acumTime >= holdTime)
			{
				longTouch = true;
			}
			if(Input.GetTouch(0).phase == TouchPhase.Ended ) 
			{
				if (longTouch) {
					acumTime = 0;
					CmdChangeFront ();
				}else if (Mathf.Sqrt (Mathf.Pow (touchPosStart.x - Input.GetTouch (0).position.x, 2f) + Mathf.Pow (touchPosStart.y - Input.GetTouch (0).position.y, 2f)) > 200f) {
					CmdPause ();
				}else {
					CmdServe ();
				}
			} 
		}
	}

	[Command]
	void CmdPause(){
		renderer.enabled = true;
		Time.timeScale = 0f;
		pauseMenu.alpha = 1f;
		pauseMenu.interactable = true;
	}
	[Command]
	void CmdDebug(){
		Debug.Log ("pause");
	}
	[Command]
	void CmdChangeFront(){
		GameObject cameraParent = GameObject.FindGameObjectWithTag ("camera parent");
		Transform camera = cameraParent.transform.FindChild ("Main Camera");
		cameraParent.transform.eulerAngles=new Vector3(cameraParent.transform.eulerAngles.x,90f-camera.transform.localEulerAngles.y,cameraParent.transform.eulerAngles.z);
		GameObject playerParent = GameObject.FindGameObjectWithTag ("Player");
		Transform player = playerParent.transform.FindChild ("RACKET");
		playerParent.transform.eulerAngles = new Vector3 (-90f, -player.localEulerAngles.x, 0f);
	}
	[Command]
	void CmdServe(){
		Rigidbody ball = GameObject.FindGameObjectWithTag ("ball").GetComponent<Rigidbody> ();
		ball.useGravity = true;
		ball.velocity = (new Vector3 (-8, -5, 0));
	}
}