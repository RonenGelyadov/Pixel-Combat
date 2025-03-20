using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float moveSpeed = 1f;

	public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
	public static PlayerController Instance;

	PlayerControls playerControls;
	Vector2 movement;
	Rigidbody2D rb;
	Animator myAnimator;
	SpriteRenderer mySpriteRenderer;

	bool facingLeft = false;

	void Awake()
	{
		Instance = this;
		playerControls = new PlayerControls();
		rb = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
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
}
