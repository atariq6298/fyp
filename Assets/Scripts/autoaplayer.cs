using UnityEngine;
using System.Collections;

public class autoaplayer : MonoBehaviour
{
	
	public float dx;
	public float dz;

	void OnCollisionEnter (Collision col)
	{
		if (true) {
			dx = Random.Range (8f, 10.5f);
			dz = Random.Range (-2.5f, 2.5f);
			float vy = 0;
	
			float dy = col.transform.position.y - 2;
			float dzr = col.transform.position.z;
			dzr = dz - dzr;
			if (dy <= 0.5) {
				vy = 3f;
			} else if (dy <= 1) {
				vy = 2f;
			} else if (dy <= 5) {
				vy = 1f;
			} else {
				vy = 0f;
			}

			float vx = (float)((dx * (Mathf.Sqrt ((float)(2 * 9.81 * dy + vy * vy)) - vy)) / (2 * dy));
			float vz = (float)((dzr * (Mathf.Sqrt ((float)(2 * 9.81 * dy + vy * vy)) - vy)) / (2 * dy));
			col.rigidbody.velocity = new Vector3 (vx, vy, vz);	
		}
	}

	void Start ()
	{
	}

	void FixedUpdate ()
	{	
	}
}