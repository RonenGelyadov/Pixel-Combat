using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
	TMP_Text goldText;
	int currentGold = 0;

	const string COIN_AMOUNT_TEXT = "Gold Amount Text";

	public void UpdateCurrentGold()
	{
		currentGold += 1;

		if (goldText == null) 
		{
			goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
		}

		goldText.text = currentGold.ToString("D3");
	}
}
