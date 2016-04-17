using UnityEngine;
using System.Collections;


public class BirdEnemy : Enemy {

	//the point from where the enemies move
	//probably defaults to world but some enemies will have the ladder or player as a relative movement position
	public Vector3      relativeMovementPosition;
//	public GameObject   enemyPrefab;
//	public EnemyManager enemyManager;
//
	public Vector2 speed            = new Vector2(.25f, .25f);
	//the default enemy moves right and up
	public Vector2 relativeMovement = new Vector2 (0, 0);


	private int randomBehaviorIndex;
	private Vector2[] movements = {
		new Vector2 (1f, -0.325f),
		new Vector2 (1f, -.75f),
		new Vector2 (-1f, -0.325f),
		new Vector2 (-1f, -.75f),
		new Vector2 (1.2f, -.001f),
		new Vector2 (-1.2f, .001f),
		new Vector2 (0.05f, 0.5f),
		new Vector2 (0.08f, .5f),
		new Vector2 (-0.09f, .5f)
	};
	private Vector2[] spawnPointsRelativeToPlayer = {
		new Vector2 (-10f, 15f),
		new Vector2 (-5f, 15f),
		new Vector2 (5f, 15f),
		new Vector2 (10f, 15f),
		new Vector2 (-15f, 5f),
		new Vector2 (15f, 5f),
		new Vector2 (4f, 15f),
		new Vector2 (-5f, 15f),
		new Vector2 (7f, 15f)
	};

	//give the enemy some time to be removed it might pop back in frame
	private SpriteRenderer spriteRenderer;

	public GameObject playerObject;

	// Use this for initialization
	void Start() {
		return;
		DetermineRelativeMovementPosition();
		DetermineSpawnPoint ();
		if (movements [randomBehaviorIndex].x < 0) {
			spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
			spriteRenderer.transform.localScale = new Vector2 (
				spriteRenderer.transform.localScale.x * -1,
				spriteRenderer.transform.localScale.y
			);
		}
	}

	public virtual void DetermineRelativeMovementPosition() {
		//relativeMovementPosition = transform.TransformPoint (0,0,0);
	}

	public virtual void DetermineSpawnPoint() {
		randomBehaviorIndex = Random.Range (0, spawnPointsRelativeToPlayer.Length);
		transform.position = transform.TransformPoint (
			playerObject.transform.position.x + spawnPointsRelativeToPlayer[randomBehaviorIndex].x,
			playerObject.transform.position.y + spawnPointsRelativeToPlayer[randomBehaviorIndex].y,
			0
		);
	}

	public virtual void UpdateRelativeMovementPosition() {
		//bird enemies are relative to world don't need to update anything
	}

	public virtual void Move() {
		//basic movement
		transform.position = GetNextPosition();
	}

	public virtual Vector2 GetNextPosition() {
		
		return relativeMovementPosition = new Vector2 (
			transform.position.x + movements[randomBehaviorIndex].x * speed.x,
			transform.position.y + movements[randomBehaviorIndex].y * speed.y
		);
	}


	// Update is called once per frame
	void Update () {
		return;
		UpdateRelativeMovementPosition ();
		Move ();
	}


}
