using UnityEngine;
using System.Collections;

public class LadderSegment : MonoBehaviour {	
	void Update () {
		SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer> ();

		if (spriteRenderer.isVisible) {
			transform.localPosition = new Vector3 (
				transform.localPosition.x,
				transform.localPosition.y,
				Mathf.Lerp(10, -9, Camera.main.WorldToViewportPoint(transform.position).y)
			);
		}
	}
}
