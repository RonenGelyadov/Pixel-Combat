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

		ToggleActiveHighlight(0);
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
		if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
		{
			Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
		}

		Transform childTransform = transform.GetChild(activeSlotIndexNum);
		InventorySlot inventorySlot = childTransform.gameObject.GetComponent<InventorySlot>();
		WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
		GameObject weaponToSpawn = weaponInfo.weaponPrefab;

		if (weaponInfo == null)
		{
			ActiveWeapon.Instance.WeaponNull();
			return;
		}

		GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
		ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
		newWeapon.transform.parent = ActiveWeapon.Instance.transform;

		ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
	}
}

