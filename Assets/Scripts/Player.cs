using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject ladderObject;
	public Vector2 exertingForce; 
	private Rigidbody2D m_RigidBody2D;
	private Ladder ladder;

	private float verticalSpeed               = 30f;
	private float horizontalMaxForce          = 10f;
	//sorta enum and never used ;D
	private string[] ladderPositionStates     = {"left", "center", "right"};
	private float[] ladderHorizontalPositions = {-2f, 0f, 2f};
	private int ladderPositionStateIndex      = 1; // default at center


	// Use this for initialization
	void Awake () {
		m_RigidBody2D = GetComponent<Rigidbody2D>();
		ladder        = ladderObject.GetComponent<Ladder> ();
	}

	/**
	 * 	verticalMovement, -1 or 0 or 1
	 *  horizontalForce, -1 or 0 or 1
	 */
	public void Move (float verticalMovement, float horizontalMovement, bool falling) {
		//apply the verticalMovement
		m_RigidBody2D.velocity = new Vector2(0,verticalMovement * verticalSpeed);

		//update the exertingForce
		//are we on the same side as we are exerting force too
		if(
			horizontalMovement < 0 && ladderPositionStateIndex == 1 ||
			horizontalMovement > 0 && ladderPositionStateIndex == 3
		) {
			ladder.ExertForce (transform.localPosition, new Vector2(horizontalMaxForce * horizontalMovement, 0f));
		}
	}

	public void Reposition(float horizontalMovement) {
		//do we need to change ladderPosition
		if (horizontalMovement == 0) {//no

		} else if (horizontalMovement > 0) {
			if (ladderPositionStateIndex < 2) {
				ladderPositionStateIndex++;

				//optional animate to state
			}
		} else if (horizontalMovement < 0) {
			if (ladderPositionStateIndex > 0) {
				ladderPositionStateIndex--;

				//optional animate to state

			}
		}

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

		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.LeftArrow)) {
			Reposition (horizontalMovement);
		}

		if (Input.GetKey (KeyCode.Space)) {
			falling = true;
		}

		Move (verticalMovement, horizontalMovement, falling);
	}

}
