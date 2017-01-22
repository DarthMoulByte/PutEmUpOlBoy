using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorRandomizer : MonoBehaviour
{
	private SpriteRenderer renderer;

	void Start()
	{
		renderer = GetComponent<SpriteRenderer>();

		var random = Random.Range(0.5f, 1f);

		renderer.color = Random.ColorHSV(0.25f, 0.5f, 0.2f, 0.4f, 1f, 1f);
	}
}
