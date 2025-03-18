using UnityEngine;

public class Sword : MonoBehaviour
{
	[SerializeField] GameObject slashAnimPrefab;
	[SerializeField] Transform SlashAnimSpawnPoint;


	PlayerControls playerControls;
	Animator myAnimator;
	PlayerController playerController;
	ActiveWeapon activeWeapon;

	GameObject slashAnim;

	void Awake()
	{
		playerController = GetComponentInParent<PlayerController>();
		activeWeapon = GetComponentInParent<ActiveWeapon>();
		myAnimator = GetComponent<Animator>();
		playerControls = new PlayerControls();
	}

	void OnEnable()
	{
		playerControls.Enable();
	}

	void Start()
	{
		playerControls.Combat.Attack.started += _ => Attack();
	}

	void Update()
	{
		MouseFollowWithOffset();
	}

	void Attack()
	{
		myAnimator.SetTrigger("Attack");

		slashAnim = Instantiate(slashAnimPrefab, SlashAnimSpawnPoint.position, Quaternion.identity);
		slashAnim.transform.parent = this.transform.parent;
	}

	void MouseFollowWithOffset()
	{
		Vector3 mousePos = Input.mousePosition;
		Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

		float angle = Mathf.Atan2(mousePos.y - playerScreenPoint.y, Mathf.Abs(mousePos.x - playerScreenPoint.x)) * Mathf.Rad2Deg;

		if (mousePos.x < playerScreenPoint.x)
		{
			activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
		}
		else if (mousePos.x > playerScreenPoint.x)
		{
			activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
}
