using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemySpawnTimer {

	public float minimumTime = 0.5f;
	public float maximumTime = 1;

	public float value { get; private set; }

	public GameObject[] enemies;

	public delegate void OnTimerReadyCallback(EnemySpawnTimer self);
	public void UpdateTimer(float timeframe, OnTimerReadyCallback onTimerReady) {
		value += timeframe;
		if (isTriggered () == true) {
			onTimerReady (this);
			Reset ();
		} 
	}

	public bool isTriggered() {
		return value > maximumTime;
	}

	public void Reset() {
		value = 0;
	}
	public void ManuallyMoveTimer(float newValue) {
		value = newValue;
	}
}
