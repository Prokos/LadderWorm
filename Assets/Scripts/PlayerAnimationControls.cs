using UnityEngine;
using System.Collections;

public class PlayerAnimationControls : MonoBehaviour {

	private MovementAlongLadder movementScript;
	private SpriteRenderer spriteCenter;
	private SpriteRenderer spriteLeft;
	private SpriteRenderer spriteRight;
	private Animator animator;

	// Use this for initialization
	void Start() {
		movementScript = this.GetComponent<MovementAlongLadder> ();
		animator     = transform.GetComponentInChildren<Animator> ();
		spriteCenter = transform.GetChild (0).GetComponent<SpriteRenderer> ();
		spriteLeft   = transform.GetChild (2).GetComponent<SpriteRenderer> ();
		spriteRight  = transform.GetChild (1).GetComponent<SpriteRenderer> ();
	}

	
	// Update is called once per frame
	void Update () {
		if (movementScript.anchorY.speed >= 0) {
			animator.speed = movementScript.anchorY.speed / 10;
		}

		float percentageOffladder = GetPercentageOffLadder ();
		spriteRight.enabled = false;
		spriteLeft.enabled = false;
		spriteCenter.enabled = false;
		if (percentageOffladder < -.7) {
			spriteLeft.enabled = true;
		} else if (percentageOffladder > .7) {
			spriteRight.enabled = true;
		} else {
			spriteCenter.enabled = true;
		}

	}

	float GetPercentageOffLadder() {
		return movementScript.GetPosition ().x / movementScript.anchorX.max;
	}
}
