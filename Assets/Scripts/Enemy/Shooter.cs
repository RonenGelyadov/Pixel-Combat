using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] float bulletMoveSpeed;
	[SerializeField] int burstCount;
	[SerializeField] int projectilesPerBurst;
	[SerializeField] [Range(0, 359)] float angleSpread;
	[SerializeField] float startingDistance = 0.1f;
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
		
		Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
		float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x);

		for (int i = 0; i < burstCount; i++)
		{

			GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
			newBullet.transform.right = targetDirection;

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
