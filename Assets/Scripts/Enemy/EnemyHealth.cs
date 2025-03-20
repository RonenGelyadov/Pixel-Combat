using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;

    int currentHealth;

	void Start()
	{
		currentHealth = startingHealth;
	}
	
	public void TakeDamage(int damage)	
	{
		currentHealth -= damage;
		Debug.Log(currentHealth);
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
