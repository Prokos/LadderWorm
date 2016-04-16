using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject ladderObject;
	public Vector2 exertingForce; 

	private Rigidbody2D m_RigidBody2D;
	private Ladder ladder;
	private Bounds ladderBounds;
	private Bounds movementBounds;

	private Vector2 maxSpeed;
	private Vector2 maxForce;

	private Vector2 maxSpeedOffLadder = new Vector2 (30f,  0f);
	private Vector2 maxForceOffLadder = new Vector2 (500f, 0f);
	private Vector2 maxSpeedOnLadder  = new Vector2 (10f, 30f);
	private Vector2 maxForceOnLadder  = new Vector2 (500f, 500f);

	//sorta enum and never used ;D
	private string[] ladderPositionStates     = {"left", "center", "right"};
	private float[] ladderHorizontalPositions = {-2f, 0f, 2f};
	private int ladderPositionStateIndex      = 1; // default at center
	private int lastLadderPositionStateIndex  = 1; //default to default as last

	// Use this for initialization
	void Awake () {
		m_RigidBody2D  = GetComponent<Rigidbody2D>();
		ladder         = ladderObject.GetComponent<Ladder>();
		ladderBounds   = ladder.GetBounds ();
		movementBounds = ladderBounds;

		//we start with being on the ladder
		maxSpeed = maxSpeedOnLadder;
		maxForce = maxForceOnLadder;
		movementBounds.size = Vector3.Scale(movementBounds.size, new Vector3 (1.5f, 0, 0));
	}

	/**
	 * 	verticalMovement, -1 or 0 or 1
	 *  horizontalForce, -1 or 0 or 1
	 */
	private void Move (Vector2 movement, bool falling) {
		//apply the verticalMovement
		m_RigidBody2D.AddRelativeForce(new Vector2(movement.x * maxForce.x, movement.y * maxForce.y));
		m_RigidBody2D.velocity = new Vector2(
			Mathf.Min(m_RigidBody2D.velocity.x, maxSpeed.x),
			Mathf.Min(m_RigidBody2D.velocity.y, maxSpeed.y)
		);
	}

	private void CalculateCurrentState() {
		//do we need to change ladderPosition
		if ( //center
			ladderPositionStateIndex != 1 &&
			transform.localPosition.x < ladderBounds.max.x &&
			transform.localPosition.x > ladderBounds.min.x
		) {
			ladderPositionStateIndex = 1;

		} else if ( //left
			ladderPositionStateIndex != 0 &&
			transform.localPosition.x < ladderBounds.min.x
		) {
			if (ladderPositionStateIndex < 2) {
				ladderPositionStateIndex = 0;
			}
		} else if (//right
			ladderPositionStateIndex != 2 &&
			transform.localPosition.x > ladderBounds.max.x
		) { 
			if (ladderPositionStateIndex > 0) {
				ladderPositionStateIndex = 2;
			}
		}

		//TODO: animations based on ladderPositionState
	}

	private void ApplyStateMaxValues() {
		if (ladderPositionStateIndex != lastLadderPositionStateIndex) {
			lastLadderPositionStateIndex = ladderPositionStateIndex;
			if (ladderPositionStateIndex == 1) { // center
				maxForce = maxForceOnLadder;
				maxSpeed = maxSpeedOnLadder;
			} else {
				maxForce = maxForceOffLadder;
				maxSpeed = maxSpeedOffLadder;
			}
		}
	}

	private void ApplyBoundLimits() {
		if (transform.localPosition.x > movementBounds.max.x) {
			transform.localPosition = new Vector2 (
				movementBounds.max.x,
				transform.localPosition.y
			);
		} else if (transform.localPosition.x < movementBounds.min.x) {
			transform.localPosition = new Vector2 (
				movementBounds.min.x,
				transform.localPosition.y
			);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 movement = Vector2.zero;
		bool falling = false;

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

		if (
			Input.GetKeyDown (KeyCode.LeftArrow) ||
			Input.GetKeyDown (KeyCode.RightArrow)
		) {
			Debug.Log (ladderPositionStateIndex);
		}
		CalculateCurrentState ();


		if (Input.GetKey (KeyCode.Space)) {
			falling = true;
		}

		Move (movement, falling);

		ApplyBoundLimits ();

		if (transform.localPosition.y > ladder.GetHeight ()) {
			ladder.AddLength (1);
		}
	}

}
