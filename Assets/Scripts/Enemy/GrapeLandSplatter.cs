using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour
{
	SpriteFade spriteFade;

	void Awake()
	{
		spriteFade = GetComponent<SpriteFade>();
	}

	void Start()
	{
		StartCoroutine(spriteFade.SlowFadeRoutine());

		Invoke("DisableCollider", 0.2f);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
		playerHealth?.TakeDamage(1, transform);
	}

	void DisableCollider()
	{
		GetComponent<CapsuleCollider2D>().enabled = false;
	}
}
