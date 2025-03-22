using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] float roamChangeDirFloat = 2f;

	enum State
	{
		Roaming
	}

	State state;
	EnemyPathfinding enemyPathfinding;

	void Awake()
	{
		enemyPathfinding = GetComponent<EnemyPathfinding>();
		state = State.Roaming;
	}

	void Start()
	{
		StartCoroutine(RoamingRoutine());
	}

	IEnumerator RoamingRoutine()
	{
		while (state == State.Roaming)
		{
			Vector2 roamPosition = GetRoamingPosition();
			enemyPathfinding.MoveTo(roamPosition);
			yield return new WaitForSeconds(roamChangeDirFloat);
		}
	}

	Vector2 GetRoamingPosition()
	{
		return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
	}
}
