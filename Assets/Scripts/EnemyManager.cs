using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
	public GameObject playerObject;
	public GameObject ladderObject;

	public EnemySpawnTimer birds;
	public EnemySpawnTimer snakes;

	void Update() {
		birds.UpdateTimer (Time.deltaTime, (spawner) => {
			Vector3 position = playerObject.transform.position + new Vector3(0,0,0);
			var enemy = Instantiate(spawner.enemies[0], position, Quaternion.identity) as GameObject;

			var enemyScript = enemy.GetComponent<BirdEnemy>();
			enemyScript.playerObject = playerObject;

			float randomAngle = Random.Range(-30, 20) + 90;
			if (Random.value < 0.5) randomAngle += 180;

			Vector3 angles = enemyScript.transform.eulerAngles;
			angles.z = randomAngle;
			enemyScript.transform.eulerAngles = angles;

			enemyScript.transform.Translate(new Vector3(0, -20, 0), Space.Self);

			float randomPositionalDeviation = Random.Range(-30, 30);
			enemyScript.transform.position += new Vector3(0, randomPositionalDeviation, 0);

			enemyScript.AdjustSpriteToRotation();

			float speed = Random.Range(8, 15);
			enemyScript.speed = speed;
		});

		snakes.UpdateTimer (Time.deltaTime, (spawner) => {
			Vector3 position = playerObject.transform.position;
			var enemy = Instantiate(spawner.enemies[0], new Vector3(), Quaternion.identity) as GameObject;

			var enemyScript = enemy.GetComponent<SnakeEnemy>();
			enemyScript.Connect(playerObject, ladderObject, position);
		});
	}

}