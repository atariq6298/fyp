using UnityEngine;
using System.Collections;


public class manualplayer : MonoBehaviour {
	public float dx;
	public float dz;
	void OnCollisionEnter(Collision col){
		if (true) {
			//dx = Random.Range (8f, 10.5f);
			//dz = Random.Range (-2.5f, 2.5f);
			float vy = 0;
			//col.attachedRigidbody.velocity = new Vector3 (-col.attachedRigidbody.velocity.x,col.attachedRigidbody.velocity.y+2,col.attachedRigidbody.velocity.z);
			float dy=col.transform.position.y-2;
			float dzr = col.transform.position.z;
			dzr = dz - dzr;
			if (dy <= 0.5) {
				vy = 6f;
			} else if (dy <= 1) {
				vy = 5f;
			} else if (dy <= 5) {
				vy = 4f;
			} else {
				vy = 3f;
			}

			float vx = (float)((dx*(Mathf.Sqrt((float)(2*9.81*dy+vy*vy))-vy))/(2*dy));
			float vz = (float)((dzr*(Mathf.Sqrt((float)(2*9.81*dy+vy*vy))-vy))/(2*dy));
			col.rigidbody.velocity = new Vector3 (vx, vy, vz);
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}