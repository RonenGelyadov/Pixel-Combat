using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
	[SerializeField] Material whiteFlashMat;
	[SerializeField] float restoreDefaultMatTime = 0.2f;

	Material defaultMat;
	SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultMat = spriteRenderer.material;
	}

	public IEnumerator FlashRoutine()
	{
		spriteRenderer.material = whiteFlashMat;
		yield return new WaitForSeconds(restoreDefaultMatTime);
		spriteRenderer.material = defaultMat;
	}
}
