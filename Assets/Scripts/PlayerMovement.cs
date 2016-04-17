using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MovementAlongLadder))]
public class PlayerMovement : MonoBehaviour {

	public Ladder ladder;

	public float sidewaysForce = 2;
	public float forceDeadZonePercentage = 0.5F;
	public float movementDeadZonePercentage = 0.5F;

	public float ladderPlacementCooldown = 0.2f;
	private float ladderPlacementCooldownCurrent = 0;
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

		float percentageOffLadder = getPercentageOffLadder ();
		/*
		if (movement.y != 0 && Mathf.Abs (percentageOffLadder) > movementDeadZonePercentage) {
			movement.y = 0;
			movementScript.anchorX.SetValuesExplicitly (movementScript.anchorX.value, 0);
		}
		*/

		movementScript.Move (movement, Time.fixedDeltaTime);

		Vector2 jointPosition = movementScript.GetPosition ();

		ladderPlacementCooldownCurrent -= Time.fixedDeltaTime;
		if (ladderPlacementCooldownCurrent < 0 && jointPosition.y > ladder.GetHeight() - 1) {
			ladder.AddSegment ();
			ladderPlacementCooldownCurrent = ladderPlacementCooldown;
		}

		percentageOffLadder = getPercentageOffLadder ();
		if (Mathf.Abs (percentageOffLadder) <= forceDeadZonePercentage) {
			percentageOffLadder = 0;
		}
		if (percentageOffLadder > 0 && Input.GetKey (KeyCode.RightArrow) 
			|| percentageOffLadder < 0 && Input.GetKey (KeyCode.LeftArrow)) 
		{
			ladder.ExertForce (new Vector2(percentageOffLadder * sidewaysForce, 0));
		}

	}

	float getPercentageOffLadder() {
		return movementScript.GetPosition ().x / movementScript.anchorX.max;
	}

}
