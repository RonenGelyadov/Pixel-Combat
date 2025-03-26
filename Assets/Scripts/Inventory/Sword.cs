using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
	[SerializeField] GameObject slashAnimPrefab;
	[SerializeField] Transform SlashAnimSpawnPoint;
	[SerializeField] Transform weaponCollider;
	[SerializeField] float swordAttackCD = 0.5f;

	Animator myAnimator;
	PlayerController playerController;
	ActiveWeapon activeWeapon;

	GameObject slashAnim;

	void Awake()
	{
		playerController = GetComponentInParent<PlayerController>();
		activeWeapon = GetComponentInParent<ActiveWeapon>();
		myAnimator = GetComponent<Animator>();
	}


	void Update()
	{
		MouseFollowWithOffset();
	}


	public void Attack()
	{
		myAnimator.SetTrigger("Attack");
		weaponCollider.gameObject.SetActive(true);

		slashAnim = Instantiate(slashAnimPrefab, SlashAnimSpawnPoint.position, Quaternion.identity);
		slashAnim.transform.parent = this.transform.parent;

		StartCoroutine(AttackCDRoutine());
	}

	IEnumerator AttackCDRoutine()
	{
		yield return new WaitForSeconds(swordAttackCD);
		ActiveWeapon.Instance.ToggleIsAttacking(false);
	}

	public void DoneAttackingAnimEvent()
	{
		weaponCollider.gameObject.SetActive(false);
	}

	public void SwingUpFlipAnimEvent()
	{
		slashAnim.transform.rotation = Quaternion.Euler(-180, 0, 0);

		if (playerController.FacingLeft)
		{
			slashAnim.GetComponent<SpriteRenderer>().flipX = true;
		}
	}

	public void SwingDownFlipAnimEvent()
	{
		slashAnim.transform.rotation = Quaternion.Euler(0, 0, 0);

		if (playerController.FacingLeft)
		{
			slashAnim.GetComponent<SpriteRenderer>().flipX = true;
		}
	}

	void MouseFollowWithOffset()
	{
		Vector3 mousePos = Input.mousePosition;
		Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

		//float angle = Mathf.Atan2(mousePos.y - playerScreenPoint.y, Mathf.Abs(mousePos.x - playerScreenPoint.x)) * Mathf.Rad2Deg;

		if (mousePos.x < playerScreenPoint.x)
		{
			activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0); //angle in z
			weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
		}
		else if (mousePos.x > playerScreenPoint.x)
		{
			activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0); //angle in z
			weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}
}
