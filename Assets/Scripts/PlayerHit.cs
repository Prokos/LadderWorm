using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour {

	public GameObject player;

	void OnTriggerEnter2D(Collider2D other) {
		transform.parent.GetComponent <PlayerCollision> ().Collision(other);
	}
}
