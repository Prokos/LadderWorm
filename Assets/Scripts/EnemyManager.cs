using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	public GameObject birdEnemyPrefab;     // The enemy prefab to be spawned.
	public float spawnTime = 1f;    // How long between each spawn.

	public GameObject playerObject;


	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", 0, 1);
	}


	void Spawn ()
	{
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		GameObject tmpBirdEnemy = Instantiate (birdEnemyPrefab) as GameObject;
		tmpBirdEnemy.GetComponent<BirdEnemy>().playerObject = playerObject;

	}
}