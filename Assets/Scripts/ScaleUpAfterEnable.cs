using UnityEngine;
using System.Collections;

public class ScaleUpAfterEnable : MonoBehaviour
{
	[Range(0f, 1f)]
	public float speed = 0.1f;

    private Vector3 startScale;

	void OnEnable()
	{
		transform.localScale = Vector3.one;
	}

	void Update()
	{
		transform.localScale += Vector3.one*speed;
	}
}
