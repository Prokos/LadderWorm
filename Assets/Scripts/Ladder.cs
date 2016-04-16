using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ladder : MonoBehaviour {
	public int startLength;
	public GameObject segmentPrefab;
	public float angle;

	private int length;

	void Awake () {
		length = startLength;

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
			Vector3 segmentPosition = new Vector3 (
            	0,
				segmentRenderer.bounds.size.y * (i + segments.Count),
            	1
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
			transform.Rotate(Vector3.forward * 10 * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate(Vector3.back * 10 * Time.deltaTime);
		}

		BuildLadder ();
	}
}