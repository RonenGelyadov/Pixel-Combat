using UnityEngine;

public class DamageSource : MonoBehaviour
{
	[SerializeField] int damageAmount = 1;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
		enemyHealth?.TakeDamage(damageAmount);	
	}
}
