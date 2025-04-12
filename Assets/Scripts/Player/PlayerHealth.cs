using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
	public bool IsDead { get; private set; }

	[SerializeField] int maxHealth = 3;
	[SerializeField] float knockBackThrustAmount = 10f;
	[SerializeField] float damageRecoveryTime = 1f;

	Slider healthSlider;
	int currentHealth;
	bool canTakeDamage = true;
	Knockback knockback;
	Flash flash;

	const string HEALTH_SLIDER_TEXT = "Health Slider";
	const string TOWN_TEXT = "Town";
	readonly int DEATH_HASH = Animator.StringToHash("Death");

	protected override void Awake()
	{
		base.Awake();

		knockback = GetComponent<Knockback>();
		flash = GetComponent<Flash>();
	}

	void Start()
	{
		IsDead = false;
		currentHealth = maxHealth;

		UpdateHealthSlider();
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

		if (enemy)
		{
			TakeDamage(1, collision.transform);
		}
	}

	public void HealPlayer()
	{
		if (currentHealth < maxHealth)
		{
			currentHealth += 1;
			UpdateHealthSlider();
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
		UpdateHealthSlider();
		CheckIfPlayerDeath();
	}

	void CheckIfPlayerDeath()
	{
		if (currentHealth <= 0 && !IsDead)
		{
			IsDead = true;
			Destroy(ActiveWeapon.Instance.gameObject);
			currentHealth = 0;
			GetComponent<Animator>().SetTrigger(DEATH_HASH);
			StartCoroutine(DeathLoadSceneRoutine());
		}
	}

	IEnumerator DeathLoadSceneRoutine()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
		Stamina.Instance.ReplenishStaminaOnDeath();
		SceneManager.LoadScene(TOWN_TEXT);
	}

	IEnumerator DamageRecoveryRoutine()
	{
		yield return new WaitForSeconds(damageRecoveryTime);
		canTakeDamage = true;
	}

	void UpdateHealthSlider()
	{
		if (healthSlider == null)
		{
			healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
		}

		healthSlider.maxValue = maxHealth;
		healthSlider.value = currentHealth;
	}
}
