using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
	[SerializeField] GameObject goldCoinPrefab;

	public void DropItems()
	{
		Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
	}
}
