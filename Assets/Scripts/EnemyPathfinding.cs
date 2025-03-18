using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
	[SerializeField] float moveSpeed = 2f;

    Rigidbody2D rb;
	Vector2 moveDir;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
	}

	public void MoveTo(Vector2 targetPosition)
	{
		moveDir = targetPosition;
	}
}
