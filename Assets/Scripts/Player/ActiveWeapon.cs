using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
	PlayerControls playerControls;
	bool attackButtonDown, isAttacking = false;

	public MonoBehaviour CurrentActiveWeapon { get; private set; }

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

	public void NewWeapon(MonoBehaviour newWeapon)
	{
		CurrentActiveWeapon = newWeapon;
	}

	public void WeaponNull()
	{
		CurrentActiveWeapon = null;
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
		if (attackButtonDown && !isAttacking && CurrentActiveWeapon != null)
		{
			isAttacking = true;
			(CurrentActiveWeapon as IWeapon).Attack();
		}		
	}
}
