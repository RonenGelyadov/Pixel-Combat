using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] float dashSpeed = 4f;
	[SerializeField] TrailRenderer myTrailRenderer;
	public bool FacingLeft { get { return facingLeft; } }

	PlayerControls playerControls;
	Vector2 movement;
	Rigidbody2D rb;
	Animator myAnimator;
	SpriteRenderer mySpriteRenderer;
	float startingMoveSpeed;

	bool facingLeft = false;
	bool isDashing = false;

	protected override void Awake()
	{
		base.Awake();
		playerControls = new PlayerControls();
		rb = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		playerControls.Combat.Dash.performed += _ => Dash();

		startingMoveSpeed = moveSpeed;
	}

	void OnEnable()
	{
		playerControls.Enable();
	}

	void Update()
	{
		PlayerInput();
	}

	void FixedUpdate()
	{
		AdjustPlayerFacingDirection();
		Move();
	}

	void PlayerInput()
	{
		movement = playerControls.Movement.Move.ReadValue<Vector2>();

		myAnimator.SetFloat("moveX", movement.x);
		myAnimator.SetFloat("moveY", movement.y);
	}

	void Move()
	{
		rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
	}

	void AdjustPlayerFacingDirection()
	{
		Vector3 mousePos = Input.mousePosition;
		Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

		if (mousePos.x < playerScreenPoint.x)
		{
			mySpriteRenderer.flipX = true;
			facingLeft = true;
		}
		else if (mousePos.x > playerScreenPoint.x)
		{
			mySpriteRenderer.flipX = false;
			facingLeft = false;
		}
	}

	void Dash()
	{
		if (!isDashing)
		{
			isDashing = true;
			moveSpeed *= dashSpeed;
			myTrailRenderer.emitting = true;
			StartCoroutine(EndDashRoutine());
		}
	}

	IEnumerator EndDashRoutine()
	{
		float dashTime = 0.2f;
		float dashCD = 0.25f;
		yield return new WaitForSeconds(dashTime);
		moveSpeed = startingMoveSpeed;
		myTrailRenderer.emitting = false;
		yield return new WaitForSeconds(dashCD);
		isDashing = false;
	}
}
