using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;

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
		knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
		StartCoroutine(flash.FlashRoutine());
		DetectDeath();
	}

	void DetectDeath()
	{
		if (currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}
}
