using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
	[SerializeField] WeaponInfo weaponInfo;
	[SerializeField] GameObject arrowPrefab;
	[SerializeField] Transform arrowSpawnPoint;

	Animator myAnimator;

	readonly int FIRE_HASH = Animator.StringToHash("Fire");

	void Awake()
	{
		myAnimator = GetComponent<Animator>();
	}

	public void Attack()
	{
		myAnimator.SetTrigger(FIRE_HASH);
		GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
		newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
	}

	public WeaponInfo GetWeaponInfo()
	{
		return weaponInfo;
	}
}
