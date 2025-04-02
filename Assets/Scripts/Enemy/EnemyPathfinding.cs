using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
	[SerializeField] float moveSpeed = 2f;

    Rigidbody2D rb;
	Vector2 moveDir;
	Knockback knockback;
	SpriteRenderer spriteRenderer;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		knockback = GetComponent<Knockback>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate()
	{
		if (knockback.GettingKnockedBack) { return; }

		rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

		if (moveDir.x < 0)
		{
			spriteRenderer.flipX = true;
		}
		else if (moveDir.x > 0)
		{
			spriteRenderer.flipX = false;
		}
	}

	public void MoveTo(Vector2 targetPosition)
	{
		moveDir = targetPosition;
	}
}
