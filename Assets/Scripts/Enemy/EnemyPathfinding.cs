using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
	[SerializeField] float moveSpeed = 2f;

    Rigidbody2D rb;
	Vector2 moveDir;
	Knockback knockback;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		knockback = GetComponent<Knockback>();
	}

	void FixedUpdate()
	{
		if (knockback.gettingKnockedBack) { return; }

		rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
	}

	public void MoveTo(Vector2 targetPosition)
	{
		moveDir = targetPosition;
	}
}
