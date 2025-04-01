using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
	[SerializeField] GameObject slashAnimPrefab;
	// [SerializeField] float swordAttackCD = 0.5f;
	[SerializeField] WeaponInfo weaponInfo;
	
	Transform slashAnimSpawnPoint;
	Transform weaponCollider;
	Animator myAnimator;
	GameObject slashAnim;

	void Awake()
	{
		myAnimator = GetComponent<Animator>();
	}

	void Start()
	{
		weaponCollider = PlayerController.Instance.GetWeaponCollider();
		slashAnimSpawnPoint = PlayerController.Instance.GetSlashAnimSpawnPoint();
	}

	void Update()
	{
		MouseFollowWithOffset();
	}

	public WeaponInfo GetWeaponInfo()
	{
		return weaponInfo;
	}

	public void Attack()
	{
		myAnimator.SetTrigger("Attack");
		weaponCollider.gameObject.SetActive(true);
		slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
		slashAnim.transform.parent = this.transform.parent;
	}

	public void DoneAttackingAnimEvent()
	{
		weaponCollider.gameObject.SetActive(false);
	}

	public void SwingUpFlipAnimEvent()
	{
		slashAnim.transform.rotation = Quaternion.Euler(-180, 0, 0);

		if (PlayerController.Instance.FacingLeft)
		{
			slashAnim.GetComponent<SpriteRenderer>().flipX = true;
		}
	}

	public void SwingDownFlipAnimEvent()
	{
		slashAnim.transform.rotation = Quaternion.Euler(0, 0, 0);

		if (PlayerController.Instance.FacingLeft)
		{
			slashAnim.GetComponent<SpriteRenderer>().flipX = true;
		}
	}

	void MouseFollowWithOffset()
	{
		Vector3 mousePos = Input.mousePosition;
		Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

		// float angle = Mathf.Atan2(mousePos.y - playerScreenPoint.y, Mathf.Abs(mousePos.x - playerScreenPoint.x)) * Mathf.Rad2Deg;

		if (mousePos.x < playerScreenPoint.x)
		{
			ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 0); //angle in z
			weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
		}
		else if (mousePos.x > playerScreenPoint.x)
		{
			ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0); //angle in z
			weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}
}
