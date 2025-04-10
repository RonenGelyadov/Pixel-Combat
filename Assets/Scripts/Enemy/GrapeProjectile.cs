using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class GrapeProjectile : MonoBehaviour
{
	[SerializeField] float duration = 1f;
	[SerializeField] AnimationCurve animCurve;
	[SerializeField] float heightY = 3;
	[SerializeField] GameObject grapeProjectileShadow;
	[SerializeField] GameObject splatterPrefab;

	void Start()
	{
		GameObject grapeShadow = 
			Instantiate(grapeProjectileShadow, transform.position + new Vector3(0, -0.3f, 0), Quaternion.identity);

		Vector3 playerPos = PlayerController.Instance.transform.position;
		Vector3 grapeShadowStartPosition = grapeShadow.transform.position;

		StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
		StartCoroutine(MoveGrapeShadowRoutine(grapeShadow, grapeShadowStartPosition, playerPos));
	}

	IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
	{
		float timePassed = 0f;

		while (timePassed < duration)
		{
			timePassed += Time.deltaTime;
			float linearT = timePassed / duration;
			float heightT = animCurve.Evaluate(linearT);
			float height = Mathf.Lerp(0f, heightY, heightT);

			transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

			yield return null;
		}

		Instantiate(splatterPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	IEnumerator MoveGrapeShadowRoutine(GameObject grapeShadow, Vector3 startPosition, Vector3 endPosition)
	{
		float timePassed = 0f;

		while (timePassed < duration)
		{
			timePassed += Time.deltaTime;
			float linearT = timePassed / duration;

			grapeShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);

			yield return null;
		}

		Destroy(grapeShadow);
	}
}
