using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundScroller : MonoBehaviour {
	public Vector2 scrollSpeed;
	public Sprite sprite;

	private int poolSize = 3; //We don't do this dynamically because we don't want to allocate memory during runtime

	private Vector3 startPosition;
	private List<GameObject> spritePool;

	void Start(){
		startPosition = new Vector2 (
			0,
			Camera.main.transform.localPosition.y - Camera.main.orthographicSize
		);

		FillPool ();
	}

	void FillPool(){
		for (var i = 0; i < poolSize; i++) {
			GameObject spriteObject = new GameObject ();
			SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer> ();

			spriteRenderer.sprite = sprite;
		
			spriteObject.transform.parent = transform;

			// Position the sprite next to the last one in the pool
			spriteObject.transform.localPosition = new Vector2 (
				-(spriteRenderer.bounds.size.x * .5f * (poolSize - 1)) + spriteRenderer.bounds.size.x * i,
				0
			);
		}
	}

	void Update (){
		transform.position = new Vector3 (
			startPosition.x + Camera.main.transform.position.x * scrollSpeed.x,
			startPosition.y + Camera.main.transform.position.y * scrollSpeed.y,
			startPosition.z
		);

		// Check bounds and see if we need more sprites

	}
}