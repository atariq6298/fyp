using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class ChildRacketController : NetworkBehaviour {
	Transform child;
	public Transform acc;
	public Transform gyro;
	GameObject ball;
	// Use this for initialization
	void Start () {
		//transform.Rotate(-90f,0,0);
		ball = GameObject.Find ("ball");
		child = transform.FindChild ("RACKET");
		GameObject phone = GameObject.FindWithTag ("phone");
		if (phone != null) {
			gyro = (phone.transform.FindChild ("gyroscope")).transform;
		}

	}
	// Update is called once per frame
	void FixedUpdate(){ 
		if (gyro == null) {
			GameObject phone = GameObject.FindWithTag ("phone");
			if (phone != null) {
				gyro = (phone.transform.FindChild ("gyroscope")).transform;
			}
		}

		this.transform.position = new Vector3 (transform.position.x, ball.transform.position.y, ball.transform.position.z);
		
		if (Input.touchCount > 0) {
			child.transform.localPosition = Vector3.zero;
		}
		if (gyro != null )
		//child.localEulerAngles = new Vector3 (gyro.localPosition.x, -gyro.localPosition.y, -gyro.localPosition.z);
		    child.localEulerAngles = new Vector3 (gyro.localPosition.x, -gyro.localPosition.y, -gyro.localPosition.z);
    }
	void Update(){
		
	}
}