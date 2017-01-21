using System;
using UnityEngine;
using System.Collections;

public class CrowdSkeleton : MonoBehaviour
{
	public KeyCode key;

	private Vector3 startPosition;

	public Action Cheer;

	private float lastCheerTime;

	private SkeletonRow row;

	private Animator animator;

	public enum SkeletonState
	{
		Down = 0,
		Up = 1,
		Idle = 2,
	}

	public SkeletonState state = SkeletonState.Down;

	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		row = GetComponentInParent<SkeletonRow>();

		startPosition = transform.position;

		animator.SetTrigger( "Idle" );
		state = SkeletonState.Idle;
	}

	void Update()
	{
		if (Input.GetKeyDown(key))
		{
			if (state == SkeletonState.Idle)
			{
				animator.SetTrigger( "Down" );
				state = SkeletonState.Down;
			}
			else if (state == SkeletonState.Down)
			{
				animator.SetTrigger( "Jump" );
				state = SkeletonState.Idle;

				row.Cheer( this );
			}

			Debug.Log(name + " " + state);
		}
	}
}
