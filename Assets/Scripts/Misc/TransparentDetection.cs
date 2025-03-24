using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
	[Range(0, 1)]
	[SerializeField] float transparencyAmount = 0.8f;
	[SerializeField] float fadeTime = 0.4f;

	SpriteRenderer spriteRenderer;
	Tilemap tilemap;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		tilemap = GetComponent<Tilemap>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerController>())
		{
			if (spriteRenderer)
			{
				StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
			}
			else if (tilemap)
			{
				StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
			}
			
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerController>())
		{
			if (spriteRenderer)
			{
				StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 1f));
			}
			else if (tilemap)
			{
				StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, 1f));
			}
		}
	}

	IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
	{
		float elapsedTime = 0;
		while (elapsedTime < fadeTime)
		{
			elapsedTime += Time.deltaTime;
			float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
			spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
			yield return null;
		}
	}

	IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
	{
		float elapsedTime = 0;
		while (elapsedTime < fadeTime)
		{
			elapsedTime += Time.deltaTime;
			float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
			tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
			yield return null;
		}
	}
}
