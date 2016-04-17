using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other) {
		transform.parent.GetComponent <PlayerCollision> ().Collision(other);
	}
}
