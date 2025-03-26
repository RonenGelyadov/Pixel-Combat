using UnityEngine;
using Unity.Cinemachine;

public class CameraController : Singleton<CameraController>
{
	CinemachineCamera cinemachineCamera;

	public void SetPlayerCameraFollow()
	{
		cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
		cinemachineCamera.Follow = PlayerController.Instance.transform;
	}
}
