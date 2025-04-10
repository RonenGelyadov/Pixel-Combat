using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;
	[SerializeField] GameObject DeathVFXprefab;
	[SerializeField] float knockBackThrust = 15f;

	int currentHealth;
	Knockback knockback;
	Flash flash;
	
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
		knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
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
			GetComponent<PickUpSpawner>().DropItems();
			Destroy(gameObject);
		}
	}
}
