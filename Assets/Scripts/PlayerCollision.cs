using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour {

	// Use this for initialization

	public float gameOverTime = 1.5f;
	private float gameOverTimeCurrent = 0;
	private bool timerRunning = false;

	public Text gameOverText;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timerRunning == true) {
			gameOverTimeCurrent += Time.deltaTime;
			if (gameOverTimeCurrent > gameOverTime) {
				gameOverText.gameObject.SetActive (false);
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	public void Collision(PlayerHit objectHit, Collider2D other) {
		if (other.tag == "Enemy") {
			objectHit.GetComponent<Collider2D> ().enabled = false;
			timerRunning = true;
			this.GetComponent<PlayerMovement> ().canMove = false;
			gameOverText.gameObject.SetActive (true);
		}
	}
}
