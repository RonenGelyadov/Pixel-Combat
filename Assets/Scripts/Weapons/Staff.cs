using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
	[SerializeField] WeaponInfo weaponInfo;
	[SerializeField] GameObject magicLaser;
	[SerializeField] Transform magicLaserSpawnPoint;

	Animator myAnimator;

	readonly int ATTACK_HASH = Animator.StringToHash("Attack");

	void Awake()
	{
		myAnimator = GetComponent<Animator>();
	}

	void Update()
	{
		MouseFollowWithOffset();
	}

	public void Attack()
	{
		myAnimator.SetTrigger(ATTACK_HASH);
	}

	public void SpawnStaffProjectileAnimEvent()
	{
		GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
		newLaser.gameObject.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
	}

	public WeaponInfo GetWeaponInfo()
	{
		return weaponInfo;
	}

	void MouseFollowWithOffset()
	{
		Vector3 mousePos = Input.mousePosition;
		Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

		float angle = Mathf.Atan2(mousePos.y - playerScreenPoint.y, Mathf.Abs(mousePos.x - playerScreenPoint.x)) * Mathf.Rad2Deg;

		if (mousePos.x < playerScreenPoint.x)
		{
			ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle); //angle in z
		}
		else if (mousePos.x > playerScreenPoint.x)
		{
			ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle); //angle in z
		}
	}
}
