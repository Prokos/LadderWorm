using UnityEngine;
using System.Collections;

public class PlayerAnimationControls : MonoBehaviour {

	private MovementAlongLadder movementScript;
	private Animator animator;

	// Use this for initialization
	void Start() {
		movementScript = this.GetComponent<MovementAlongLadder> ();
		animator = transform.GetComponentInChildren<Animator> ();
	}

	
	// Update is called once per frame
	void Update () {
		if (movementScript.anchorY.speed >= 0) {
			animator.speed = movementScript.anchorY.speed / 10;
		}
	}
}
