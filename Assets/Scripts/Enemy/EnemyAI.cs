using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] float roamChangeDirFloat = 2f;
	[SerializeField] float attackRange = 5f;
	[SerializeField] MonoBehaviour enemyType;
	[SerializeField] float attackCooldown = 2f;
	[SerializeField] bool stopMovingWhileAttacking = false;

	bool canAttack = true;

	enum State
	{
		Roaming,
		Attacking
	}

	Vector2 roamPosition;
	float timeRoaming = 0f;

	State state;
	EnemyPathfinding enemyPathfinding;

	void Awake()
	{
		enemyPathfinding = GetComponent<EnemyPathfinding>();
		state = State.Roaming;
	}

	void Start()
	{
		//if (enemyType)
		//{
		//	attackRange = (enemyType as IEnemy).GetEnemyRange();
		//}
		//else
		//{
		//	attackRange = 0f;
		//}

		roamPosition = GetRoamingPosition();
	}

	void Update()
	{
		MovementStateControl();
	}

	void MovementStateControl() 
	{
		switch (state)
		{
			default:

			case State.Roaming:
				Roaming();
				break;

			case State.Attacking:
				Attacking();
				break;
		}
	}

	void Roaming()
	{
		timeRoaming += Time.deltaTime;

		enemyPathfinding.MoveTo(roamPosition);

		if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
		{
			state = State.Attacking;
		}

		if (timeRoaming > roamChangeDirFloat)
		{
			roamPosition = GetRoamingPosition();
		}
	}

	void Attacking()
	{
		if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
		{
			state = State.Roaming;
		}

		if (canAttack && attackRange != 0)
		{
			canAttack = false;
			(enemyType as IEnemy).Attack();

			if (stopMovingWhileAttacking)
			{	
				enemyPathfinding.StopMoving();
			}
			else
			{
				enemyPathfinding.MoveTo(roamPosition);
			}

			StartCoroutine(AttackCooldownRoutine());
		}
	}

	IEnumerator AttackCooldownRoutine()
	{
		yield return new WaitForSeconds(attackCooldown);

		canAttack = true;
	}

	public void ToggleCanAttack(bool canAttack)
	{
		this.canAttack = canAttack;
	}

	Vector2 GetRoamingPosition()
	{
		timeRoaming = 0f;

		return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
	}
}
