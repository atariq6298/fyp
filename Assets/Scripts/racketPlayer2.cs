 using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class racketPlayer2 : MonoBehaviour {

	public float dx;
	public float dz;
	public GameObject ball;
	void OnCollisionEnter(Collision col){		
			dx = Random.Range (9f, 10.5f);
			//dz = 0;
			dz = Random.Range (-2.5f, 2.5f);
			float vy = 0;
			//col.attachedRigidbody.velocity = new Vector3 (-col.attachedRigidbody.velocity.x,col.attachedRigidbody.velocity.y+2,col.attachedRigidbody.velocity.z);
			float dy = col.transform.position.y - 2;
			float dzr = col.transform.position.z;
			dzr = dz - dzr;
			if (dy <= 0.5) {
				vy = 3.5f;
			} else if (dy <= 1) {
				vy = 2.5f;
			} else if (dy <= 5) {
				vy = 1.5f;
			} else {
				vy = 1f;
			}
			float vx = (float)((dx * (Mathf.Sqrt ((float)(2 * 9.81 * dy + vy * vy)) - vy)) / (2 * dy));
			float vz = (float)((dzr * (Mathf.Sqrt ((float)(2 * 9.81 * dy + vy * vy)) - vy)) / (2 * dy));
			col.rigidbody.velocity = new Vector3 (-vx, vy, vz);	
		 

	}
	// Use this for initialization
	void Start () {
		//ball=GameObject.Find ("ball");
	}	
	// Update is called once per frame
	void Update () {	
		
		this.transform.position=new Vector3(transform.position.x, ball.transform.position.y, ball.transform.position.z);
	}
}