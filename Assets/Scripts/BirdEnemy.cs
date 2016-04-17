using UnityEngine;
using System.Collections;


public class BirdEnemy : Enemy {
	
	public float speed = 0;

	public float lifeDuration = 20;
	private float lifeDurationCurrent = 0;

	public GameObject playerObject;
	private GameObject sprite;

	public void AdjustSpriteToRotation() {
		sprite = transform.GetChild (0).gameObject;

		float angle = transform.eulerAngles.z;

		Vector3 angles = sprite.transform.localEulerAngles;
		angles.z = 90;
		sprite.transform.localEulerAngles = angles;

		if (angle < 180) {
			Vector3 newScale = sprite.transform.localScale;
			newScale.y *= -1;
			sprite.transform.localScale = newScale;
		}
	}


	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0), Space.Self);

		lifeDurationCurrent += Time.deltaTime;
		if (lifeDurationCurrent > lifeDuration) {
			Destroy (gameObject);
		}
	}


}
