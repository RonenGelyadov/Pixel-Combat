using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
	Animator myAnimator;

	void Awake()
	{
		myAnimator = GetComponent<Animator>();
	}

	void Start()
	{
		AnimatorStateInfo state = myAnimator.GetCurrentAnimatorStateInfo(0);
		myAnimator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
	}
}
