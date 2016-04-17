using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	public GameObject birdEnemyPrefab;     // The enemy prefab to be spawned.
	public float spawnTime;    // How long between each spawn.
	public float randomAddonTime;

	private bool IsSpawning = true;

	public GameObject playerObject;


	void Start ()
	{
		// First spawn is double the spawn time delayed + random
		Invoke ("Spawn", spawnTime * 2 + Random.value * randomAddonTime);
	}


	void Spawn ()
	{
		if (IsSpawning) {
			// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
			GameObject tmpBirdEnemy = Instantiate (birdEnemyPrefab) as GameObject;
			tmpBirdEnemy.GetComponent<BirdEnemy> ().playerObject = playerObject;

			//regular respawn
			Invoke ("Spawn", spawnTime + Random.value * randomAddonTime);
		}

	}
}