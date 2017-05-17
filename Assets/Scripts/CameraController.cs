using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform player;
	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.position.x, 4.5f, player.position.z);
	}
}