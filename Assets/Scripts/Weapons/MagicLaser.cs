using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
	[SerializeField] float laserGrowTime = 2f;

	bool isGrowing = true;
	float laserRange;
	SpriteRenderer spriteRenderer;
	CapsuleCollider2D capsuleCollider;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		capsuleCollider = GetComponent<CapsuleCollider2D>();
	}

	void Start()
	{
		LaserFaceMouse();		
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<Indestructible>() && !collision.isTrigger)
		{
			isGrowing = false;
		}
	}

	public void UpdateLaserRange(float laserRange)
	{
		this.laserRange = laserRange;
		StartCoroutine(IncreaseLaserLengthRoutine());
	}

	IEnumerator IncreaseLaserLengthRoutine()
	{
		float timePassed = 0f;

		while (spriteRenderer.size.x < laserRange && isGrowing)
		{
			timePassed += Time.deltaTime;
			float linearTime = timePassed / laserGrowTime;

			spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearTime), 1f);

			capsuleCollider.size = new Vector2(Mathf.Lerp(0.9f, laserRange, linearTime), capsuleCollider.size.y);
			capsuleCollider.offset = new Vector2(Mathf.Lerp(0.9f, laserRange, linearTime) / 2 , capsuleCollider.offset.y);

			yield return null;
		}

		StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
	}

	void LaserFaceMouse()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		Vector2 direction = transform.position - mousePosition;
		transform.right = -direction;
	}
}
