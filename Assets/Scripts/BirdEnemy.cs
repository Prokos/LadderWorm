using UnityEngine;
using System.Collections;


public class BirdEnemy : MonoBehaviour {

	//the point from where the enemies move
	//probably defaults to world but some enemies will have the ladder or player as a relative movement position
	public Vector2      relativeMovementPosition;
//	public GameObject   enemyPrefab;
//	public EnemyManager enemyManager;
//
	public Vector2 speed            = new Vector2(.25f, .25f);
	//the default enemy moves right and up
	public Vector2 relativeMovement = new Vector2 (0, 0);


	private int randomBehaviorIndex;
	private Vector2[] movements = {
		new Vector2 (1, 1),
		new Vector2 (1, -1),
		new Vector2 (-1, 1),
		new Vector2 (-1, -1)
	};
	private Vector2[] spawnPointsRelativeToPlayer = {
		new Vector2 (-10f, -10f),
		new Vector2 (-10f, 10f),
		new Vector2 (10f, -10f),
		new Vector2 (10f, 10f)
	};

	//give the enemy some time to be removed it might pop back in frame
	private Coroutine removeWithWait;
	private int framesTillRemoved = 300;
	private bool isRemoving = false;

	private Player player;
	public GameObject playerObject;

	// Use this for initialization
	void Start() {
		player = playerObject.GetComponent<Player>();
		DetermineRelativeMovementPosition();
		DetermineSpawnPoint ();
	}

	public virtual void DetermineRelativeMovementPosition() {
		relativeMovementPosition = playerObject.transform.position;
	}

	public virtual void DetermineSpawnPoint() {
		randomBehaviorIndex = Random.Range (0, spawnPointsRelativeToPlayer.Length);
		transform.position = relativeMovement = new Vector2 (
			playerObject.transform.position.x + spawnPointsRelativeToPlayer[randomBehaviorIndex].x,
			playerObject.transform.position.y + spawnPointsRelativeToPlayer[randomBehaviorIndex].y
		);
	}

	public virtual void UpdateRelativeMovementPosition() {
		//bird enemies are relative to world don't need to update anything
	}

	public virtual void Move() {
		//basic forward movement
//		transform.position = GetNextPosition();
	}

//	public virtual Vector2 GetNextPosition() {
//		
//		return relativeMovementPosition = new Vector2 (
//			relativeMovementPosition.x + movements[randomBehaviorIndex].x * speed.x,
//			relativeMovementPosition.y + movements[randomBehaviorIndex].y * speed.y
//		);
//	}

	IEnumerator RemoveWithWait() {
		yield return new WaitUntil(() => framesTillRemoved == 0);
		Remove ();
	}

	void Remove() {
		Destroy (gameObject);
		Destroy (this);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if (removeWithWait != null)
				StopCoroutine (removeWithWait);
			
			Destroy(gameObject);
			Destroy (this);
			player.HitByEnemy ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (isRemoving) {
			framesTillRemoved--;
		}
		UpdateRelativeMovementPosition ();
		Move ();
	}


}
