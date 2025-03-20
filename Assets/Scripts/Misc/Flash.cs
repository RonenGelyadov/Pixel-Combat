using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
	[SerializeField] Material whiteFlashMat;
	[SerializeField] float restoreDefaultMatTime = 0.1f;

	Material defaultMat;
	SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultMat = spriteRenderer.material;
	}

	public float GetRestoreMatTime()
	{
		return restoreDefaultMatTime;
	}

	public IEnumerator FlashRoutine()
	{
		spriteRenderer.material = whiteFlashMat;
		yield return new WaitForSeconds(restoreDefaultMatTime);
		spriteRenderer.material = defaultMat;
	}
}
