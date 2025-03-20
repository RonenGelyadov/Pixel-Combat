using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] float dashSpeed = 4;
	[SerializeField] TrailRenderer myTrailRenderer;
	public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
	public static PlayerController Instance;


	PlayerControls playerControls;
	Vector2 movement;
	Rigidbody2D rb;
	Animator myAnimator;
	SpriteRenderer mySpriteRenderer;

	bool facingLeft = false;
	bool isDashing = false;

	void Awake()
	{
		Instance = this;
		playerControls = new PlayerControls();
		rb = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		playerControls.Combat.Dash.performed += _ => Dash();
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
			FacingLeft = true;
		}
		else if (mousePos.x > playerScreenPoint.x)
		{
			mySpriteRenderer.flipX = false;
			FacingLeft = false;
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
		moveSpeed /= dashSpeed;
		myTrailRenderer.emitting = false;
		yield return new WaitForSeconds(dashCD);
		isDashing = false;
	}
}
