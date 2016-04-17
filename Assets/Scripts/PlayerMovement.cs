using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MovementAlongLadder))]
public class PlayerMovement : MonoBehaviour {

	public Ladder ladder;

	public float sidewaysForce = 2;
	public float forceDeadZonePercentage = 0.5F;

	private MovementAlongLadder movementScript;

	void Start() {
		movementScript = this.GetComponent<MovementAlongLadder> ();
	}

	void FixedUpdate() {
		Vector2 movement = Vector2.zero;

		if (Input.GetKey (KeyCode.UpArrow)) {
			movement += Vector2.up;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			movement += Vector2.down;
		}


		if (Input.GetKey (KeyCode.LeftArrow)) {
			movement += Vector2.left;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			movement += Vector2.right;
		}

		movementScript.Move (movement, Time.fixedDeltaTime);

		Vector2 jointPosition = movementScript.GetPosition ();

		if (jointPosition.y > ladder.GetHeight() - 3) {
			ladder.AddSegment ();
		}

		float percentageOffLadder = jointPosition.x / movementScript.anchorX.max;
		if (Mathf.Abs (percentageOffLadder) <= forceDeadZonePercentage) {
			percentageOffLadder = 0;
		}
		if (percentageOffLadder > 0 && Input.GetKey (KeyCode.RightArrow) 
			|| percentageOffLadder < 0 && Input.GetKey (KeyCode.LeftArrow)) 
		{
			ladder.ExertForce (new Vector2(percentageOffLadder * sidewaysForce, 0));
		}

	}
}
