using UnityEngine;
using System.Collections;

[RequireComponent (typeof(FixedJoint2D))]
public class PlayerMovement : MonoBehaviour {

	public Ladder ladder;


	public float sidewaysForce = 2;
	public float forceDeadZonePercentage = 0.5F;

	public ForceFloat anchorX;
	public ForceFloat anchorY;

	private FixedJoint2D attachedJoint;

	void Start () {
		attachedJoint = this.GetComponent <FixedJoint2D> ();
		anchorX.Start ();
		anchorY.Start ();
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

		if (movement.x != 0) {
			anchorX.Accerelate (Time.fixedDeltaTime, movement.x);
		}
		if (movement.y != 0) {
			anchorY.Accerelate (Time.fixedDeltaTime, movement.y);
		}
		anchorX.DecelerateAndUpdate (Time.fixedDeltaTime);
		anchorY.DecelerateAndUpdate (Time.fixedDeltaTime);

		float newX = anchorX.value;	
		float newY = anchorY.value;	

		attachedJoint.connectedAnchor = new Vector2 (newX, newY);

		if (attachedJoint.connectedAnchor.y > ladder.GetHeight() - 3) {
			ladder.AddSegment ();
		}

		float percentageOffLadder = attachedJoint.connectedAnchor.x / anchorX.max;
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
