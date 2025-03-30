using UnityEngine;

public class MouseFollow : MonoBehaviour
{
	Vector3 mousePosition;

	void Update()
	{
		FaceMouse();
	}

	void FaceMouse()
	{
		mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

		Vector2 direction = transform.position - mousePosition;

		transform.right = -direction;
	}
}
