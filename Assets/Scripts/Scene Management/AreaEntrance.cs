using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
	[SerializeField] string transitionName;

	void Start()
	{
		if (transitionName == SceneManagement.Instance.SceneTransitionName)
		{
			PlayerController.Instance.transform.position = this.transform.position;
			CameraController.Instance.SetPlayerCameraFollow();
			UIFade.Instance.FadeToClear();
		}
	}
}
