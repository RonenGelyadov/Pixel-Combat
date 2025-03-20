using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;

    int currentHealth;
	Knockback knockback;
	Flash flash;

	[SerializeField] GameObject DeathVFXprefab;
	
	void Awake()
	{
		knockback = GetComponent<Knockback>();
		flash = GetComponent<Flash>();
	}

	void Start()
	{
		currentHealth = startingHealth;
	}
	
	public void TakeDamage(int damage)	
	{
		currentHealth -= damage;
		knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
		StartCoroutine(flash.FlashRoutine());
		StartCoroutine(CheckDetectDeathRoutine());
	}

	IEnumerator CheckDetectDeathRoutine()
	{
		yield return new WaitForSeconds(flash.GetRestoreMatTime());
		DetectDeath();
	}

	void DetectDeath()
	{
		if (currentHealth <= 0)
		{
			Instantiate(DeathVFXprefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
