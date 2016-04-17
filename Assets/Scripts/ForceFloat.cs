using UnityEngine;

[System.Serializable]
public class ForceFloat {

	[UnityEngine.HideInInspector]
	public float value { get; private set; }
	[UnityEngine.HideInInspector]
	public float speed { get; private set; }

	public float initialValue = 5;

	public float min = 0;
	public float max = 10;

	public float decelerationAdditive = 0.05f;
	public float decelerationMultiplicative = 0.999f;
	public float acceleration = 0.5f;

	public bool debug = false;

	public void Start() {
		value = initialValue;
	}

	public void Accerelate(float timeFrame, float normalisedAccelerationFactor) {
		float f = normalisedAccelerationFactor * acceleration * timeFrame;
		speed = speed + f;
	}

	public void DecelerateAndUpdate(float timeFrame) {
		float newSpeed = speed;
		float decelerationAdditiveAmount = decelerationAdditive * timeFrame;

		if (Mathf.Abs (newSpeed) < decelerationAdditiveAmount) {
			newSpeed = 0;
		}
		newSpeed *= Mathf.Pow (decelerationMultiplicative, timeFrame);
		if (newSpeed > 0) {
			newSpeed -= decelerationAdditiveAmount;
		} else if (newSpeed < 0) {
			newSpeed += decelerationAdditiveAmount;
		}
		speed = newSpeed;

		value += speed * timeFrame;

		if (value > max) {
			value = max;
			if (speed > 0) {
				speed = 0;
			}
		}
		if (value < min) {
			value = min;
			if (speed < 0) {
				speed = 0;
			}
		}
	}

	public void SetValuesExplicitly(float newValue, float newSpeed) {
		value = newValue;
		speed = newSpeed;
		this.initialValue = newValue;
	}
}
