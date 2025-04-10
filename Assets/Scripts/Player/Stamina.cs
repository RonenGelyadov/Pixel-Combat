using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton <Stamina>
{
	public int CurrentStamina { get; private set; }

	[SerializeField] Sprite fullStaminaImage, emptyStaminaImage;
	[SerializeField] int timeBetweenStaminaRefresh = 3;

	Transform staminaContainer;
	int startingStamina = 3;
	int maxStamina;

	const string STAMINA_CONTAINER_TEXT = "Stamina Container";

	protected override void Awake()
	{
		base.Awake();

		maxStamina = startingStamina;
		CurrentStamina = startingStamina;
	}

	void Start()
	{
		staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
	}

	public void UseStamina()
	{
		CurrentStamina--;
		UpdateStaminaImages();
	}

	public void RefreshStamina()
	{
		if (CurrentStamina < maxStamina)
		{
			CurrentStamina++;
		}

		UpdateStaminaImages();
	}

	IEnumerator RefreshStaminaRoutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeBetweenStaminaRefresh);
			RefreshStamina();
		}
	}

	void UpdateStaminaImages()
	{
		for (int i = 0; i < maxStamina; i++)
		{
			if (i <= CurrentStamina - 1)
			{
				staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaImage;
			}
			else
			{
				staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaImage;
			}
		}

		if (CurrentStamina < maxStamina)
		{
			StopAllCoroutines();
			StartCoroutine(RefreshStaminaRoutine());
		}
	}
}
