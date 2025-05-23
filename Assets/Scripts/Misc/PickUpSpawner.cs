using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
	[SerializeField] GameObject goldCoin, healthGlobe, staminaGlobe;

	public void DropItems()
	{
		int randomNum = Random.Range(1, 4);

		if (randomNum == 1)
		{
			Instantiate(healthGlobe, transform.position, Quaternion.identity);
		}
		else if (randomNum == 2)
		{
			Instantiate(staminaGlobe, transform.position, Quaternion.identity);
		}
		else if (randomNum == 3)
		{
			int randomAmountOfGold = Random.Range(1, 4);

			for (int i = 0; i < randomAmountOfGold; i++)
			{
				Instantiate(goldCoin, transform.position, Quaternion.identity);
			}
		}
		
	}
}
