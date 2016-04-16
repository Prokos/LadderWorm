using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ladder : MonoBehaviour {
	public int startLength;
	public GameObject segmentPrefab;
	public float angle;

	private int length;
	private Rigidbody2D body;

	void Awake () {
		length = startLength;
		body = GetComponent<Rigidbody2D> ();

		BuildLadder ();
	}

	public void ExertForce(float yPosition, float horizontalForce) {
		//nothing yet
	}

	void BuildLadder(){
		List<GameObject> segments = GetSegments();

		// Do we need a new segment?
		if (segments.Count >= length)
			return; // Nope

		for (int i = 0; i < length - segments.Count; i++) {
			SpriteRenderer segmentRenderer = segmentPrefab.transform.Find ("sprite").GetComponent<SpriteRenderer> ();
			Vector2 segmentPosition = new Vector2 (
            	0,
				segmentRenderer.bounds.size.y * (i + segments.Count)
			);

			AddSegment (segmentPosition);
		}
	}

	void AddSegment(Vector3 position){
		GameObject segment = Instantiate (segmentPrefab) as GameObject;

		segment.transform.parent = transform.FindChild ("Segments");
		segment.transform.localPosition = position;
		segment.transform.localRotation = Quaternion.identity;

		// Set camera follow target to latest segment, for now
		Camera.main.GetComponent<SmoothCamera2D>().target = segment.transform;
	}

	List<GameObject> GetSegments(){
		List<GameObject> children = new List<GameObject>();
		foreach (Transform child in transform.FindChild("Segments")){
			if (child.tag == "LadderSegment"){
				children.Add(child.gameObject);
			}
		}
		return children;
	}

	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			length++;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			body.AddTorque(10f * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			body.AddTorque(-10f * Time.deltaTime);
		}

		BuildLadder ();
	}
}