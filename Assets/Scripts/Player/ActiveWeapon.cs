using System.Collections;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
	public MonoBehaviour CurrentActiveWeapon { get; private set; }

	PlayerControls playerControls;
	float timeBetweenAttacks;

	bool attackButtonDown, isAttacking = false;

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

		AttackCooldown();
	}

	void Update()
	{
		Attack();
	}

	public void NewWeapon(MonoBehaviour newWeapon)
	{
		CurrentActiveWeapon = newWeapon;
		AttackCooldown();
		timeBetweenAttacks = (newWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
	}

	public void WeaponNull()
	{
		CurrentActiveWeapon = null;
	}

	void AttackCooldown()
	{
		isAttacking = true;
		StopAllCoroutines();
		StartCoroutine(TimeBetweenAttacksRoutine());
	}

	IEnumerator TimeBetweenAttacksRoutine()
	{
		yield return new WaitForSeconds(timeBetweenAttacks);
		isAttacking = false;
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
			AttackCooldown();
			(CurrentActiveWeapon as IWeapon).Attack();
		}		
	}
}
