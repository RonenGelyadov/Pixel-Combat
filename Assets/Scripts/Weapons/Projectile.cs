using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] float moveSpeed = 22f;
	[SerializeField] GameObject particleOnHitPrefabVFX;
	[SerializeField] bool isEnemyProjectile = false;
	[SerializeField] float projectileRange = 10f;

	Vector3 startPosition;

	void Start()
	{
		startPosition = transform.position;
	}

	void Update()
	{
		MoveProjectile();
		DetectFireDistance();
	}

	public void UpdateProjectileRange(float projectileRange)
	{
		this.projectileRange = projectileRange;
	}

	public void UpdateMoveSpeed(float moveSpeed)
	{
		this.moveSpeed = moveSpeed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
		Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();
		PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

		if (!collision.isTrigger && (enemyHealth || indestructible || player))
		{
			if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
			{
				player?.TakeDamage(1, transform);

				Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
				Destroy(gameObject);
			}
			else if (!collision.isTrigger && indestructible)
			{
				Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
				Destroy(gameObject);
			}
		}	
	}

	void DetectFireDistance()
	{
		if (Vector3.Distance(startPosition, transform.position) > projectileRange)
		{
			Destroy(gameObject);
		}
	}

	void MoveProjectile()
	{
		transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
	}
}

