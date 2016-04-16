using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ladder : MonoBehaviour {
	public int startLength;
	public GameObject segmentPrefab;
	public float angle;
	public float Torque = 0;
	public float maxTorque     = 10f;
	public SpriteRenderer segmentRenderer;

	private Rigidbody2D body;

	void Awake () {
		body = GetComponent<Rigidbody2D> ();
		segmentRenderer = segmentPrefab.transform.Find ("sprite").GetComponent<SpriteRenderer> ();

		for (var i = 0; i < startLength; i++) {
			AddSegment ();
		}
	}

	public void ExertForce(Vector2 force) {
		body.AddForceAtPosition (Vector2.one * Time.deltaTime * force.x * GetBounds().size.y, new Vector2(0, GetBounds ().size.y));
	}

	public void AddSegment(){
		// Determine next position
		List<GameObject> segments = GetSegments();

		Vector2 segmentPosition = new Vector2 (
			0,
			segmentRenderer.bounds.size.y * (segments.Count)
		);

		// Create segment
		GameObject segment = Instantiate (segmentPrefab) as GameObject;

		segment.transform.parent = transform.FindChild ("Segments");
		segment.transform.localPosition = segmentPosition;
		segment.transform.localRotation = Quaternion.identity;

		// Update BoxCollider
		BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
		Bounds ladderBounds = GetBounds ();

		boxCollider.size = new Vector2 (
			ladderBounds.size.x,
			ladderBounds.size.y
		);

		boxCollider.offset = new Vector2 (
			0f,
			boxCollider.size.y * .5f - segmentRenderer.bounds.size.y * .5f
		);
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

	public Bounds GetBounds(){
		Bounds bounds = new Bounds(transform.position, Vector2.zero);

		foreach(Renderer r in GetComponentsInChildren<SpriteRenderer>()) {
			bounds.Encapsulate(r.bounds);
		}

		return bounds;
	}

	public float GetHeight(){
		return segmentRenderer.bounds.size.y * GetSegments ().Count;
	}
		
}