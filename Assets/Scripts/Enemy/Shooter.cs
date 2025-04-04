using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
	[SerializeField] GameObject bulletPrefab;
	// [SerializeField] float range = 5f;

	public void Attack()
	{
		Vector2 targerDirection = PlayerController.Instance.transform.position - transform.position;

		GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
		newBullet.transform.right = targerDirection;
	}

	//public float GetEnemyRange()
	//{
	//	return range;
	//}
}
