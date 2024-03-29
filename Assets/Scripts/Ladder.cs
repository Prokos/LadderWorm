﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Ladder : MonoBehaviour {
	public int startLength;
	public GameObject segmentPrefab;
	public SpriteRenderer segmentRenderer;
	public float segmentMargin = 0f;
	public Text countText;

	private Rigidbody2D body;

	private List<GameObject> segments = new List<GameObject> ();

	void Awake () {
		body = GetComponent<Rigidbody2D> ();
		segmentRenderer = segmentPrefab.transform.Find ("sprite").GetComponent<SpriteRenderer> ();

		for (var i = 0; i < startLength; i++) {
			AddSegment ();
		}
	}

	public void ExertForce(Vector2 force) {
		body.AddForceAtPosition (Vector2.right * force.x * GetHeight(), segments.Last().transform.position);
	}
		
	public void AddSegment()
	{
		Vector3 segmentPosition = new Vector3 (
			0,
			(segmentRenderer.bounds.size.y + segmentMargin) * segments.Count,
			- segments.Count / 1000000
		);

		// Create segment
		GameObject segment = Instantiate (segmentPrefab) as GameObject;

		segment.transform.parent = transform.FindChild ("Segments");
		segment.transform.localPosition = segmentPosition;
		segment.transform.localRotation = Quaternion.identity;

		segments.Add (segment);

		// Update BoxCollider
		BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

		boxCollider.size = new Vector2 (
			segmentRenderer.bounds.size.x,
			GetHeight()
		);

		boxCollider.offset = new Vector2 (
			0f,
			boxCollider.size.y * .5f - segmentRenderer.bounds.size.y * .5f
		);

		// Score Counter
		if(countText != null){
			countText.text = segments.Count + "m";
		}
	}

	public Bounds GetBounds(){
		Bounds bounds = new Bounds(transform.position, Vector2.zero);

		foreach(Renderer r in GetComponentsInChildren<SpriteRenderer>()) {
			bounds.Encapsulate(r.bounds);
		}

		return bounds;
	}

	public float GetHeight(){
		return (segmentRenderer.bounds.size.y + segmentMargin) * segments.Count;
	}

	public float GetHeightInWorldCoordinates() {
		var topSegment = segments.Last ();
		return topSegment.transform.position.y;
	}
		
}