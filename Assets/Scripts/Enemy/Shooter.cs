using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] float bulletMoveSpeed;
	[SerializeField] int burstCount;
	[SerializeField] float timeBetweenBursts;
	[SerializeField] float restTime = 1f;
	// [SerializeField] float range = 5f;

	bool isShooting = false;

	public void Attack()
	{
		if (!isShooting)
		{
			StartCoroutine(ShootRoutine());
		}
	}

	IEnumerator ShootRoutine()
	{
		isShooting = true;

		for (int i = 0; i < burstCount; i++)
		{
			Vector2 targerDirection = PlayerController.Instance.transform.position - transform.position;

			GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
			newBullet.transform.right = targerDirection;

			if (newBullet.TryGetComponent(out Projectile projectile))
			{
				projectile.UpdateMoveSpeed(bulletMoveSpeed);
			}

			yield return new WaitForSeconds(timeBetweenBursts);
		}

		yield return new WaitForSeconds(restTime);
		isShooting = false;
	}

	//public float GetEnemyRange()
	//{
	//	return range;
	//}
}
