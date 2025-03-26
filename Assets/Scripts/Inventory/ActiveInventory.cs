using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
	int activeSlotIndexNum = 0;

	PlayerControls playerControls;

	void Awake()
	{
		playerControls = new PlayerControls();
	}

	void Start()
	{
		playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
	}

	void OnEnable()
	{
		playerControls.Enable();
	}

	void ToggleActiveSlot(int numValue)
	{
		ToggleActiveHighlight(numValue - 1);
	}

	void ToggleActiveHighlight(int indexNum)
	{
		activeSlotIndexNum = indexNum;

		foreach (Transform inventorySlot in this.transform)
		{
			inventorySlot.GetChild(0).gameObject.SetActive(false);
		}

		this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

		ChangeActiveWeapon();
	}

	void ChangeActiveWeapon()
	{
		Debug.Log(transform.GetChild(activeSlotIndexNum).GetComponent<InventorySlot>().GetWeaponInfo().name);
	}
}

