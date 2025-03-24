using UnityEngine;

public class Parallax : MonoBehaviour
{
	[SerializeField] float parallaxOffset = -0.15f;

	Camera cam;
	Vector2 startPos;
	Vector2 travel => (Vector2)cam.transform.position - startPos;

	void Awake()
	{
		cam = Camera.main;
	}

	void Start()
	{
		startPos = transform.position;
	}

	void FixedUpdate()
	{
		transform.position = startPos + travel * parallaxOffset;
	}
}
