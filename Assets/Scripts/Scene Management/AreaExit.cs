using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
	[SerializeField] string sceneToLoad;
	[SerializeField] string sceneTransitionName;

	float waitToLoadTime = 1f;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerController>())
		{
			SceneManagement.Instance.SetTransitionName(sceneTransitionName);			
			UIFade.Instance.FadeToBlack();
			StartCoroutine(LoadSceneRoutine());
		}
	}

	IEnumerator LoadSceneRoutine()
	{
		while (waitToLoadTime >= 0)
		{
			waitToLoadTime -= Time.deltaTime;
			yield return null;
		}

		SceneManager.LoadScene(sceneToLoad);
	}
}
