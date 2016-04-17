using UnityEngine;
using System.Collections;

public class SnakeEnemy : MonoBehaviour {

	public float speedFactorMin = 0.5f;

	private MovementAlongLadder movementScript;
	private GameObject sprite;

	public enum LaneSpawnBehaviour { OnlyMiddle, OnlySides }
	public LaneSpawnBehaviour spawnBehaviour;

	public void Connect(GameObject playerObject, GameObject ladderObject, Vector3 position) {
		sprite = transform.GetChild (0).gameObject;

		this.movementScript = this.GetComponent<MovementAlongLadder> ();
		this.movementScript.ladder = ladderObject.GetComponent<Ladder>();

		float startX;
		float random = Random.value;
		if (spawnBehaviour == LaneSpawnBehaviour.OnlySides) {
			if (random < 0.5f) {
				startX = movementScript.anchorX.min;
			} else {
				startX = movementScript.anchorX.max;
			}
		} else if (spawnBehaviour == LaneSpawnBehaviour.OnlyMiddle) {
			startX = 0;
		} else {
			startX = 0;
		}

		Vector3 eulerAngles = sprite.transform.localEulerAngles;
		eulerAngles.z = 0;
		sprite.transform.localEulerAngles = eulerAngles;
		if (startX < 0) {
			Vector3 newScale = sprite.transform.localScale;
			newScale.x *= -1;
			sprite.transform.localScale = newScale;
		}
			
		this.movementScript.anchorX.SetValuesExplicitly (startX, 0);
		this.movementScript.anchorY.SetValuesExplicitly (transform.localPosition.y - 20, 0);
	}

	void FixedUpdate() {
		movementScript.Move (new Vector2 (0, 1), Time.fixedDeltaTime);

		if (movementScript.GetPosition ().y >= movementScript.ladder.GetHeight ()) {
			Destroy (gameObject);
		}
	}
}
