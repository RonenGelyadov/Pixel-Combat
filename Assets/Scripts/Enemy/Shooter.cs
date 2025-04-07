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
	[SerializeField] bool stagger;
	[Tooltip("Stagger has to be enabled for oscillate to work properly")]
	[SerializeField] bool oscillate;

	bool isShooting = false;

	void OnValidate()
	{
		if (oscillate) { stagger = true; }
		if (projectilesPerBurst < 1) { projectilesPerBurst = 1; }
		if (burstCount < 1) { burstCount = 1; }
		if (timeBetweenBursts < 0.1f) { timeBetweenBursts = 0.1f; }
		if (restTime < 0.1f) { restTime = 0.1f; }
		if (startingDistance < 0.1f) { startingDistance = 0.1f; }
		if (angleSpread == 0) { projectilesPerBurst = 1; }
		if (bulletMoveSpeed <= 0) { bulletMoveSpeed = 0.1f; }
	}

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

		float startAngle, currentAngle, angleStep, endAngle;
		float timeBetweenProjectiles = 0;

		if (stagger) { timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst; }

		TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

		for (int i = 0; i < burstCount; i++)
		{
			if (!oscillate) 
			{ 
				TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle); 
			}

			if (oscillate && i % 2 == 1)
			{
				TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
			}
			else if (oscillate)
			{
				currentAngle = endAngle;
				endAngle = startAngle;
				startAngle = currentAngle;
				angleStep *= -1;
			}

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

					if (stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }
				}

			currentAngle = startAngle;

			if (!stagger) { yield return new WaitForSeconds(timeBetweenBursts); }
		}

		yield return new WaitForSeconds(restTime);
		isShooting = false;
	}

	private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
	{
		Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
		float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
		startAngle = targetAngle;
		endAngle = targetAngle;
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
