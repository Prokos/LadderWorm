using UnityEngine;
using System.Collections;

public class LadderSegmentThrow : MonoBehaviour {

	public MovementAlongLadder movementScript;
	public Ladder ladder;
	public GameObject segmentPrefab;
	public SpriteRenderer segmentRenderer;

	// Use this for initialization
	void Start () {
		segmentRenderer = segmentPrefab.transform.Find ("sprite").GetComponent<SpriteRenderer> ();
	}

	// Throw a segment on top of the ladder
	void ThrowSegment () {
		Debug.Log ("transform: " + transform.position.y);
		Vector2 spawnLocation = new Vector2 (
			transform.position.x,
			transform.position.y
		);

		// Create segment
		GameObject segment = Instantiate (segmentPrefab) as GameObject;

		segment.transform.parent = transform.FindChild ("Segments");
		segment.transform.localPosition = spawnLocation;
		segment.transform.localRotation = Quaternion.Euler(0, 75, 0);

		ladder.AddSegment();

		Debug.Log ("SpawnLocation: " + spawnLocation);
	}
}