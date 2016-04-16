using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject ladderObject;
	public Vector2 exertingForce; 

	private Rigidbody2D m_RigidBody2D;
	private Ladder ladder;
	private Bounds ladderBounds;

	private Vector2 maxSpeed = new Vector2 (15f, 30f);
	private Vector2 maxForce = new Vector2(750f, 500f);
	//sorta enum and never used ;D
	private string[] ladderPositionStates     = {"left", "center", "right"};
	private float[] ladderHorizontalPositions = {-2f, 0f, 2f};
	private int ladderPositionStateIndex      = 1; // default at center


	// Use this for initialization
	void Awake () {
		m_RigidBody2D = GetComponent<Rigidbody2D>();
		ladder        = ladderObject.GetComponent<Ladder>();
		ladderBounds  = ladder.GetBounds ();
	}

	/**
	 * 	verticalMovement, -1 or 0 or 1
	 *  horizontalForce, -1 or 0 or 1
	 */
	private void Move (Vector2 movement, bool falling) {
		//apply the verticalMovement
		m_RigidBody2D.AddRelativeForce(new Vector2(0,movement.y * maxForce.y));
		m_RigidBody2D.velocity = new Vector2(
			Mathf.Min(m_RigidBody2D.velocity.x, maxSpeed.x),
			Mathf.Min(m_RigidBody2D.velocity.y, maxSpeed.y)
		);

		//update the exertingForce
		//are we on the same side as we are exerting force too
		ladder.ExertForce (new Vector2(maxForce.x * movement.x, 0f));
	}

	private void PotentialReposition() {
		//do we need to change ladderPosition
//		if () {//no
//
//		} else if () {
//			if (ladderPositionStateIndex < 2) {
//				ladderPositionStateIndex++;
//
//				//optional animate to state
//			}
//		} else if () {
//			if (ladderPositionStateIndex > 0) {
//				ladderPositionStateIndex--;
//
//				//optional animate to state
//
//			}
//		}

		//set the horizontal position of the character TODO: should be animated
		transform.localPosition = new Vector2 (
			ladderHorizontalPositions [ladderPositionStateIndex],
			transform.localPosition.y
		);
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
			
		PotentialReposition ();

		if (Input.GetKey (KeyCode.Space)) {
			falling = true;
		}

		Move (movement, falling);

		if (transform.localPosition.y > ladder.GetHeight ()) {
			ladder.AddLength (1);
		}
	}

}
