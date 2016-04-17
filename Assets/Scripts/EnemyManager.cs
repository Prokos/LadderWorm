using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	public GameObject playerObject;
	public GameObject ladderObject;

	public EnemySpawnTimer birds;

	void Update() {
		birds.UpdateTimer (Time.deltaTime, (spawner) => {
			Vector3 position = playerObject.transform.position + new Vector3(15,0,0);
			GameObject enemy = Instantiate(spawner.enemies[0], position, Quaternion.identity) as GameObject;

			enemy.GetComponent<BirdEnemy> ().playerObject = playerObject;
			// Determine position and rotation
		});
	}

	/*
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
	*/
}