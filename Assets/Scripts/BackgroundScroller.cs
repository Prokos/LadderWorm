using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {
	public Vector2 scrollSpeed;

	private Vector3 startPosition;

	void Start(){
		startPosition = transform.position;
	}

	void Update (){
		transform.position = new Vector3 (
			startPosition.x + Camera.main.transform.position.x * scrollSpeed.x,
			startPosition.y + Camera.main.transform.position.y * scrollSpeed.y,
			1
		);
	}
}