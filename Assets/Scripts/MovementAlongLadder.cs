using UnityEngine;
using System.Collections;

[RequireComponent (typeof(FixedJoint2D))]
public class MovementAlongLadder : MonoBehaviour {

	public Ladder ladder;

	public ForceFloat anchorX;
	public ForceFloat anchorY;

	private FixedJoint2D attachedJoint;

	void Start () {
		attachedJoint = this.GetComponent <FixedJoint2D> ();
		attachedJoint.connectedBody = ladder.GetComponent<Rigidbody2D>();
		anchorX.Start ();
		anchorY.Start ();
	}

	public void Move(Vector2 normalisedAcceleration, float timeframe) {
		anchorY.max = ladder.GetHeight ();
		if (normalisedAcceleration.x != 0) {
			anchorX.Accerelate (timeframe, normalisedAcceleration.x);
		}
		if (normalisedAcceleration.y != 0) {
			anchorY.Accerelate (timeframe, normalisedAcceleration.y);
		}
		anchorX.DecelerateAndUpdate (timeframe);
		anchorY.DecelerateAndUpdate (timeframe);

		float newX = anchorX.value;	
		float newY = anchorY.value;	

		attachedJoint.connectedAnchor = new Vector2 (newX, newY);
	}

	public Vector2 GetPosition() {
		return attachedJoint.connectedAnchor;
	}
}
