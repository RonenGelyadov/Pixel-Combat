using UnityEngine;

public class DamageSource : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<EnemyAI>())
		{
			print("Yaaa !!!");
		}
	}
}
