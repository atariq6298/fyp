using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
public class Player : NetworkBehaviour
{
	public Transform acceleration;
	public GameObject ball;
	public bool isBallClose;
	Rigidbody rb;
	void OnCollisionEnter(Collision col){
		//transform.localPosition = Vector3.zero;
		//GetComponent<Rigidbody> ().velocity = Vector3.zero;
	}
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		ball = GameObject.Find("ball");
	}
	void Update(){
		if (acceleration == null) {
			GameObject phone = GameObject.FindWithTag ("phone");
			if (phone != null) {
				acceleration = (phone.transform.FindChild ("accelerometer")).transform;
			}
		}
		if (ball.transform.position.x < 2f)
			isBallClose = true;
		else
			isBallClose = false;
		
	}
	void FixedUpdate (){
		if (isBallClose) {
			Vector3 a = acceleration.localPosition;
			if (a.z > 0.0f ) {
				rb.AddForce (new Vector3 (a.z * 0.125f, 0, 0), ForceMode.Impulse);
			}
			if (transform.localPosition.x > 2f) {
				transform.localPosition = new Vector3 (2f,0,0);
			}
		}
		if (!isBallClose) {
			transform.localPosition = Vector3.zero;
			rb.velocity = Vector3.zero;
			//transform.localPosition = Vector3.MoveTowards (transform.localPosition, Vector3.zero, 0.01f);
		}
	}
}