using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ball : MonoBehaviour
{
	Renderer renderer;
	public Text scoreText;
	public int computerPoints;
	public int playerPoints;
	int state;
	Rigidbody rb;
	CanvasGroup pauseMenu;
	void OnCollisionEnter (Collision col)
	{
		string name = col.gameObject.name;
		Debug.Log (name);
		if (state == 2 & string.Equals (name, "computerHalf")) {
			state = 4;
		} else if (state == 4 & string.Equals (name, "playerHalf")) {
			state = 5;
		} else if (state == 5 & string.Equals (name, "RACKET")) {
			state = 6;
		} else if (state == 6 & string.Equals (name, "RACKET")) {
			state = 6;
		} else if (state == 6 & string.Equals (name, "computerHalf")) {
			state = 7;
		} else if (state == 7 & string.Equals (name, "racket2")) {
			state = 8;
		} else if (state == 8 & string.Equals (name, "playerHalf")) {
			state = 5;
		} else if (state == 2 & !string.Equals (name, "computerHalf")) {
			resetBall ();
			playerPoint();
			updateScore ();
		} else if (state == 4 & !string.Equals (name, "playerHalf")) {
			resetBall ();
			playerPoint();
			updateScore ();
		} else if (state == 5 & !string.Equals (name, "RACKET")) {
			resetBall ();
			computerPoint();
			updateScore ();
		} else if (state == 6 & !string.Equals (name, "computerHalf")) {
			resetBall ();
			computerPoint();
			updateScore ();
		} else if (state == 7 & !string.Equals (name, "racket2")) {
			resetBall ();
			playerPoint();
			updateScore ();
		} else if (state == 8 & !string.Equals (name, "playerHalf")) {
			resetBall ();
			computerPoint();
			updateScore ();
		}
	}

	void Start ()
	{
		renderer = GameObject.Find ("camera parent").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>();
		pauseMenu = GameObject.Find ("CanvasGroup").GetComponent<CanvasGroup> ();
		updateScore ();
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		computerPoints = 0;
		playerPoints = 0;
		state = 2;
	}
	void computerPoint(){
		computerPoints++;
		if (computerPoints == 10) {
			if (playerPoints == 10) {
				computerPoints = 9;
				playerPoints = 9;
			}
		}
		if (computerPoints == 11) {
			computerPoints = 0;
			playerPoints = 0;
		}
	}
	void playerPoint(){
		playerPoints++;
		if (playerPoints == 10) {
			if (computerPoints == 10) {
				computerPoints = 9;
				playerPoints = 9;
			}
		}
		if (playerPoints == 11) {
			computerPoints = 0;
			playerPoints = 0;
		}
	}


	void Update ()
	{
		if (Input.GetMouseButtonUp (0)) {
		}
		if (Input.GetMouseButtonDown (1)) {
			resetBall ();
			rb.useGravity = true;
			rb.velocity = (new Vector3 (-8, -5, 0));

		}
	}
	void updateScore(){
		scoreText.text = "Player:- " + playerPoints + "\nOpponent:- " + computerPoints;
	}
	void resetBall(){
		rb.useGravity = false;
		transform.position = new Vector3 (10f, 3f, 0f);
		rb.velocity = new Vector3 (0f, 0f, 0f);
		state = 2;
	}
	public void Resume(){
		renderer.enabled = false;
		Time.timeScale = 1f;
		pauseMenu.alpha = 0f;
		pauseMenu.interactable = false;
		Debug.Log ("resume");
	}
	public void Restart(){
		renderer.enabled = false;
		computerPoints = 0;
		playerPoints = 0;
		updateScore ();
		resetBall ();
		Resume ();
	}
	public void close(){
		Application.Quit ();
	}
}