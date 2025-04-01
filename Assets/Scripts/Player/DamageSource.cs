using UnityEngine;

public class DamageSource : MonoBehaviour
{
	int damageAmount;

	void Start()
	{
		MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
		damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
		enemyHealth?.TakeDamage(damageAmount);	
	}
}
