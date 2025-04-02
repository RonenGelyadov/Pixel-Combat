using UnityEngine;

public class MouseFollow : MonoBehaviour
{
	void Update()
	{
		FaceMouse();
	}

	void FaceMouse()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		Vector2 direction = transform.position - mousePosition;
		transform.right = -direction;
	}
}
