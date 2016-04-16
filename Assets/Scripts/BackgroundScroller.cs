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
		spritePool = new List<GameObject> ();

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

			spritePool.Add (spriteObject);
		}
	}

	void Update (){
		transform.position = new Vector3 (
			startPosition.x - Camera.main.transform.position.x * scrollSpeed.x,
			startPosition.y + Camera.main.transform.position.y * scrollSpeed.y,
			startPosition.z
		);

		// Check bounds and see if we need more sprites
		CheckSpritePositions();
	}

	void CheckSpritePositions(){
		if (spritePool.Count == 0)
			return;
		
		Bounds cameraBounds = Camera.main.GetOrthographicBounds();

		GameObject mostRightSprite = null;
		GameObject mostLeftSprite = null;
		foreach(GameObject sprite in spritePool){
			if(mostRightSprite == null || sprite.transform.localPosition.x > mostRightSprite.transform.localPosition.x){
				mostRightSprite = sprite;
			}

			if(mostLeftSprite == null || sprite.transform.localPosition.x < mostLeftSprite.transform.localPosition.x){
				mostLeftSprite = sprite;
			}
		}

		SpriteRenderer spriteRenderer = mostRightSprite.GetComponent<SpriteRenderer>();

		if (cameraBounds.max.x > mostRightSprite.GetComponent<SpriteRenderer>().bounds.max.x) {
			mostLeftSprite.transform.localPosition = new Vector2 (
				mostRightSprite.transform.localPosition.x + spriteRenderer.bounds.size.x,
				0
			);
		} else if (cameraBounds.min.x < mostLeftSprite.GetComponent<SpriteRenderer>().bounds.min.x) {
			mostRightSprite.transform.localPosition = new Vector2 (
				mostLeftSprite.transform.localPosition.x - spriteRenderer.bounds.size.x,
				0
			);
		}
	}

	Bounds GetBounds(){
		Bounds bounds = new Bounds();

		foreach(Renderer r in GetComponentsInChildren<SpriteRenderer>()) {
			bounds.Encapsulate(r.bounds);
		}

		return bounds;
	}
}