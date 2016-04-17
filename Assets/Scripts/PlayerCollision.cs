using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Collision(Collider2D other) {
		if (other.tag == "Enemy") {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
