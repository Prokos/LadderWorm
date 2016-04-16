using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject ladder;
	public Vector2 exertingForce; 

	private float verticalSpeed               = 10f;
	private float horizontalMaxForce          = 10f;
	private string[] ladderPositionStates     = {"left", "center", "right"};
	private float[] ladderHorizontalPositions = {-100f, 0f, 100f};
	private int ladderPositionStateIndex      = 1; // default at center

	private Rigidbody2D rigidBody2D;

	// Use this for initialization
	void Awake () {
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	/**
	 * 	verticalMovement, -1 or 0 or 1
	 *  horizontalForce, -1 or 0 or 1
	 */
	public void Move (float verticalMovement, float horizontalMovement, bool falling) {
		float ladderRotation = ladder.transform.localRotation.z;

		//apply the verticalMovement
		rigidBody2D.velocity = new Vector2(0,verticalMovement * verticalSpeed);

		//do we need to change ladderPosition
		if(horizontalMovement == 0) {//no

		} else if (horizontalMovement > 0) {
			if(ladderPositionStateIndex < 2)  {
				ladderPositionStateIndex++;

				//optional animate to state
			}
		} else if (horizontalMovement < 0) {
			if(ladderPositionStateIndex > 0) {
				ladderPositionStateIndex--;

				//optional animate to state

			}
		}

		//set the horizontal position of the character TODO: should be animated
		transform.localPosition = new Vector2 (
			ladderHorizontalPositions [ladderPositionStateIndex],
			transform.localPosition.y
		);

		//update the exertingForce
		//are we on the same side as we are exerting force too
		if(
			horizontalMovement < 0 && ladderPositionStateIndex == 1 ||
			horizontalMovement > 0 && ladderPositionStateIndex == 3
		) {
			ladder.ExertForce (transform.localPosition.y, horizontalMovement * horizontalMaxForce);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
