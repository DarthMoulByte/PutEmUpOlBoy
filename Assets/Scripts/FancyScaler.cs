using UnityEngine;
using System.Collections;

public class FancyScaler : MonoBehaviour
{
	public float scaleExtra = 0.2f;
	public float speed = 5f;

	private Vector3 startScale;

	private float offset;

	private void Start()
	{
		offset = Random.Range(0f, Mathf.PI);
		startScale = transform.localScale;
	}

	void Update()
	{
		transform.localScale = startScale + Vector3.one*(Mathf.Sin((Time.time + offset) * speed)*0.5f + 0.5f)* scaleExtra;
	}
}
