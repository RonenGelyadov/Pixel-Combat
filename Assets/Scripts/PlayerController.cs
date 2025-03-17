using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	float moveSpeed = 1f;

	PlayerControls playerControls;
	Vector2 movement;
	Rigidbody2D rb;

	void Awake()
	{
		playerControls = new PlayerControls();
		rb = GetComponent<Rigidbody2D>();
	}

	void OnEnable()
	{
		playerControls.Enable();
	}

	void Update()
	{
		
	}

	void PlayerInput()
	{

	}
}
