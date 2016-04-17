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

	private bool OnLadder = true;

	[UnityEngine.HideInInspector]
	public bool canMove = true;

	void Start() {
		movementScript = this.GetComponent<MovementAlongLadder> ();
	}

	void FixedUpdate() {
		Vector2 movement = Vector2.zero;

		float percentageOffLadder = GetPercentageOffLadder ();

		if (Mathf.Abs (percentageOffLadder) > movementDeadZonePercentage) {
			OnLadder = false;
		} else if(Mathf.Abs (percentageOffLadder) < movementDeadZonePercentage) {
			OnLadder = true;
		}

		if (OnLadder) {
			
			if (Input.GetKey (KeyCode.DownArrow)) {
				movement += Vector2.down;
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				movement += Vector2.up;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				movement += Vector2.left;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				movement += Vector2.right;
			}

		} else {
			bool verticalInput = false;
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.UpArrow)) {
				verticalInput = true;
			}
			if(verticalInput) {
				if (percentageOffLadder < movementDeadZonePercentage && !Input.GetKey (KeyCode.LeftArrow)) {
					movement += Vector2.right;
				} else if (percentageOffLadder > movementDeadZonePercentage && !Input.GetKey (KeyCode.RightArrow)) {
					movement += Vector2.left;
				}
			}

			if (Input.GetKey (KeyCode.LeftArrow) && movement.x >= 0) {
				movement += Vector2.left;
			}
			if (Input.GetKey (KeyCode.RightArrow) && movement.x <= 0) {
				movement += Vector2.right;
			}
			
		}
		if (canMove == false) {
			movement = new Vector2 ();
		}

		movementScript.Move (movement, Time.fixedDeltaTime);

		Vector2 jointPosition = movementScript.GetPosition ();

		ladderPlacementCooldownCurrent -= Time.fixedDeltaTime;
		if (ladderPlacementCooldownCurrent < 0 && jointPosition.y > ladder.GetHeight() - 4) {
			ladder.AddSegment ();
			ladderPlacementCooldownCurrent = ladderPlacementCooldown;
		}

		percentageOffLadder = GetPercentageOffLadder ();
		if (Mathf.Abs (percentageOffLadder) <= forceDeadZonePercentage) {
			percentageOffLadder = 0;
		}
		if (percentageOffLadder > 0 && Input.GetKey (KeyCode.RightArrow) 
			|| percentageOffLadder < 0 && Input.GetKey (KeyCode.LeftArrow)) 
		{
			ladder.ExertForce (new Vector2(percentageOffLadder * sidewaysForce, 0));
		}

	}

	float GetPercentageOffLadder() {
		return movementScript.GetPosition ().x / movementScript.anchorX.max;
	}

}
