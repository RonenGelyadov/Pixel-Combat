using UnityEngine;

public class Destructible : MonoBehaviour
{
	[SerializeField] GameObject destroyVFX;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<DamageSource>() || collision.gameObject.GetComponent<Projectile>())
		{
			PickUpSpawner pickUpSpawner = GetComponent<PickUpSpawner>();
			pickUpSpawner?.DropItems();
			Instantiate(destroyVFX, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
