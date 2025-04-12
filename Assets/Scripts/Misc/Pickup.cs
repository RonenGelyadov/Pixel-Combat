using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	enum PickUpType
	{
		GoldCoin,
		StaminaGlobe,
		HealthGlobe
	}

	[SerializeField] PickUpType pickUpType;
	[SerializeField] float pickUpDistance = 5f;
	[SerializeField] float acceleration = 0.2f;
	[SerializeField] float moveSpeed = 2f;
	[SerializeField] AnimationCurve animCurve;
	[SerializeField] float heightY = 1.5f;
	[SerializeField] float popDuration = 1f;

	Vector3 moveDir;
	Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		StartCoroutine(AnimCurveSpawnRoutine());
	}

	void Update()
	{
		Vector3 playerPos = PlayerController.Instance.transform.position;

		if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
		{
			moveDir = (playerPos - transform.position).normalized;
			moveSpeed += acceleration;
		}
		else
		{
			moveDir = Vector3.zero;
			moveSpeed = 2f;
		}
	}

	void FixedUpdate()
	{
		rb.linearVelocity = moveDir * moveSpeed * Time.fixedDeltaTime;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerController>())
		{
			DetectPickUpType();
			Destroy(gameObject);
		}
	}

	IEnumerator AnimCurveSpawnRoutine()
	{
		Vector2 startPoint = transform.position;
		Vector2 randomPos = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
		Vector2 endPoint = startPoint + randomPos;

		float timePassed = 0;

		while (timePassed < popDuration)
		{
			timePassed += Time.deltaTime;
			float linearT = timePassed / popDuration;
			float heightT = animCurve.Evaluate(linearT);
			float height = Mathf.Lerp(0f, heightY, heightT);

			transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);

			yield return null;
		}
	}

	void DetectPickUpType()
	{
		switch (pickUpType)
		{
			case PickUpType.GoldCoin:
				EconomyManager.Instance.UpdateCurrentGold();
				break;
			
			case PickUpType.HealthGlobe:
				PlayerHealth.Instance.HealPlayer();
				break;

			case PickUpType.StaminaGlobe:
				Stamina.Instance.RefreshStamina();
				break;

			default:
				break;
		}
	}
}
