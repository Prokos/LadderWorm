using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject ladderObject;
	public Vector2 exertingForce; 

	private Rigidbody2D m_RigidBody2D;
	private Ladder ladder;
	private Bounds ladderBounds;

	private Vector2 maxSpeed = new Vector2 (15f, 30f);
	private Vector2 maxForce = new Vector2(500f, 500f);
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
	private void Move (float verticalMovement, float horizontalMovement, bool falling) {
		//apply the verticalMovement
		m_RigidBody2D.AddRelativeForce(new Vector2(0,verticalMovement * maxForce.y));
		m_RigidBody2D.velocity = new Vector2(
			Mathf.Min(m_RigidBody2D.velocity.x, maxSpeed.x),
			Mathf.Min(m_RigidBody2D.velocity.y, maxSpeed.y)
		);
		Debug.Log (m_RigidBody2D.velocity);
		//update the exertingForce
		//are we on the same side as we are exerting force too
		if(
			horizontalMovement < 0 && ladderPositionStateIndex == 1 ||
			horizontalMovement > 0 && ladderPositionStateIndex == 3
		) {
			ladder.ExertForce (transform.localPosition, new Vector2(maxForce.x * horizontalMovement, 0f));
		}
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
		float verticalMovement = 0;
		float horizontalMovement = 0;
		bool falling = false;

		if (Input.GetKey (KeyCode.UpArrow)) {
			verticalMovement++;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			verticalMovement--;
		}


		if (Input.GetKey (KeyCode.LeftArrow)) {
			horizontalMovement--;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			horizontalMovement++;
		}
			
		PotentialReposition ();

		if (Input.GetKey (KeyCode.Space)) {
			falling = true;
		}

		Move (verticalMovement, horizontalMovement, falling);

		if (transform.localPosition.y > ladder.GetHeight ()) {
			ladder.AddLength (1);
		}
	}

}
