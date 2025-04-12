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
		StopAllCoroutines();
		StartCoroutine(RefreshStaminaRoutine());
	}

	public void RefreshStamina()
	{
		if (CurrentStamina < maxStamina && !PlayerHealth.Instance.IsDead)
		{
			CurrentStamina++;
		}

		UpdateStaminaImages();
	}

	public void ReplenishStaminaOnDeath()
	{
		CurrentStamina = startingStamina;
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
			Transform child = staminaContainer.GetChild(i);
			Image image = child?.GetComponent<Image>();

			if (i <= CurrentStamina - 1)
			{
				image.sprite = fullStaminaImage;
			}
			else
			{
				image.sprite = emptyStaminaImage;
			}
		}
	}
}
