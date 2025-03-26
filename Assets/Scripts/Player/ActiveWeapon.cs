using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
	PlayerControls playerControls;
	bool attackButtonDown, isAttacking = false;

	[SerializeField] MonoBehaviour currentActiveWeapon;

	protected override void Awake()
	{
		base.Awake();

		playerControls = new PlayerControls();
	}

	void OnEnable()
	{
		playerControls.Enable();
	}

	void Start()
	{
		playerControls.Combat.Attack.started += _ => StartAttacking();
		playerControls.Combat.Attack.canceled += _ => StopAttacking();
	}

	void Update()
	{
		Attack();
	}

	public void ToggleIsAttacking(bool value)
	{
		isAttacking = value;
	}

	void StartAttacking()
	{
		attackButtonDown = true;
	}

	void StopAttacking()
	{
		attackButtonDown = false;
	}

	void Attack()
	{
		if (attackButtonDown && !isAttacking)
		{
			isAttacking = true;
			(currentActiveWeapon as IWeapon).Attack();
		}		
	}
}
