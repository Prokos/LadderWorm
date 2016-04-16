using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ladder : MonoBehaviour {
	public int startLength;
	public GameObject segmentPrefab;
	public float angle;
	public float currentTorque = 0;
	public float maxTorque     = 10f;
	public SpriteRenderer segmentRenderer;

	private int length;
	private Rigidbody2D body;

	void Awake () {
		length = startLength;
		body = GetComponent<Rigidbody2D> ();
		segmentRenderer = segmentPrefab.transform.Find ("sprite").GetComponent<SpriteRenderer> ();

		BuildLadder ();
	}

	public void ExertForce(float yPosition, float horizontalForce) {
		//nothing yet
		currentTorque = yPosition * horizontalForce;

		//
	}

	void BuildLadder(){
		List<GameObject> segments = GetSegments();

		// Do we need a new segment?
		if (segments.Count >= length)
			return; // Nope

		for (int i = 0; i < length - segments.Count; i++) {
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

		// Set camera follow target to latest segment, for now
		// Camera.main.GetComponent<SmoothCamera2D>().target = segment.transform;
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

	Bounds GetBounds(){
		Bounds bounds = new Bounds(transform.position, Vector2.zero);

		foreach(Renderer r in GetComponentsInChildren<SpriteRenderer>()) {
			bounds.Encapsulate(r.bounds);
		}

		return bounds;
	}

	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			length++;
		}
		body.AddTorque (currentTorque * Time.deltaTime);

		BuildLadder ();
	}
}