using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] int maxHealth = 3;
	[SerializeField] float knockBackThrustAmount = 10f;
	[SerializeField] float damageRecoveryTime = 1f;

	int currentHealth;
	bool canTakeDamage = true;

	Knockback knockback;
	Flash flash;

	void Awake()
	{
		knockback = GetComponent<Knockback>();
		flash = GetComponent<Flash>();
	}

	void Start()
	{
		currentHealth = maxHealth;
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

		if (enemy)
		{
			TakeDamage(1, collision.transform);
		}
	}

	public void TakeDamage(int damage, Transform hitTransform)
	{
		if (!canTakeDamage) { return; }

		ScreenShakeManager.Instance.ShakeScreen();

		canTakeDamage = false;
		currentHealth -= damage;
		knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
		StartCoroutine(flash.FlashRoutine());
		StartCoroutine(DamageRecoveryRoutine());
	}

	IEnumerator DamageRecoveryRoutine()
	{
		yield return new WaitForSeconds(damageRecoveryTime);
		canTakeDamage = true;
	}
}
