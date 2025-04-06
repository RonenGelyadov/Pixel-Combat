using System;
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

		float startAngle, currentAngle, angleStep;
		TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);

		for (int i = 0; i < burstCount; i++)
		{
			for (int j = 0; j < projectilesPerBurst; j++)
			{
				Vector2 pos = FindBulletSpawnPos(currentAngle);

				GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
				newBullet.transform.right = newBullet.transform.position - transform.position;

				if (newBullet.TryGetComponent(out Projectile projectile))
				{
					projectile.UpdateMoveSpeed(bulletMoveSpeed);
				}

				currentAngle += angleStep;
			}

			currentAngle = startAngle;

			yield return new WaitForSeconds(timeBetweenBursts);

			TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);
		}

		yield return new WaitForSeconds(restTime);
		isShooting = false;
	}

	private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep)
	{
		Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
		float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
		startAngle = targetAngle;
		float endAngle = targetAngle;
		currentAngle = targetAngle;
		float halfAngleSpread = 0f;
		angleStep = 0f;
		if (angleSpread != 0)
		{
			angleStep = angleSpread / (projectilesPerBurst - 1);
			halfAngleSpread = angleSpread / 2;
			startAngle = targetAngle - halfAngleSpread;
			endAngle = targetAngle + halfAngleSpread;
			currentAngle = startAngle;
		}
	}

	Vector2 FindBulletSpawnPos(float currentAngle)
	{
		float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
		float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

		Vector2 pos = new Vector2(x, y);

		return pos;
	}
}
