using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    ParticleSystem ps;

	void Awake()
	{
		ps = GetComponent<ParticleSystem>();
	}

	void Update()
	{
		if (ps && !ps.IsAlive())
		{
			DestroySelfAnimEvent();
		}
	}

	public void DestroySelfAnimEvent()
    {
        Destroy(gameObject);
    }
}
