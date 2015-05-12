using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
	public Animator animator;

	GameObject effect;
	void Awake()
	{
		StartCoroutine(Test1());
	}

	void Update()
	{
		//AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
		//print(info.length);
	}

	IEnumerator Test1()
	{
		print(0);

		yield return null;

		yield return StartCoroutine(Test2());

		print(3);
	}

	IEnumerator Test2()
	{
		print(1);

		yield return null;

		print(2);
	}
}
