using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] float moveSpeed = 22f;
	[SerializeField] GameObject particleOnHitPrefabVFX;

	WeaponInfo weaponInfo;
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

	public void UpdateWeaponInfo(WeaponInfo weaponInfo)
	{
		this.weaponInfo = weaponInfo;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
		Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();

		if (!collision.isTrigger && (enemyHealth || indestructible))
		{
			Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
			Destroy(gameObject);
		}	
	}

	void DetectFireDistance()
	{
		if (Vector3.Distance(startPosition, transform.position) > weaponInfo.weaponRange)
		{
			Destroy(gameObject);
		}
	}

	void MoveProjectile()
	{
		transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
	}
}

