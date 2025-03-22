using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    Rigidbody2D rb;

	[SerializeField] float knockBackTime = 0.2f;

	public bool GettingKnockedBack { get; private set; }

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void GetKnockedBack(Transform damageSource, float knockBackThrust)
	{
		GettingKnockedBack = true;
		Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
		rb.AddForce(difference, ForceMode2D.Impulse);
		StartCoroutine(KnockRoutine());
	}

	IEnumerator KnockRoutine()
	{
		yield return new WaitForSeconds(knockBackTime);
		rb.linearVelocity = Vector2.zero;
		GettingKnockedBack = false;
	}
}
