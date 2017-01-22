using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CrowdSkeleton : MonoBehaviour
{
	public KeyCode key;

	public Sprite keyIcon;

	private Vector3 startPosition;

	public Action Cheer;

	private float lastCheerTime;

	private SkeletonRow row;

	private Animator animator;

	private SpriteRenderer iconRenderer;

	private AudioSource cheerAudio;

	private ParticleSystem cheerSuccessfulParticleSystem;

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
		cheerSuccessfulParticleSystem = GetComponentInChildren<ParticleSystem>();
		row = GetComponentInParent<SkeletonRow>();
		iconRenderer = transform.FindChild("PlayerActor").FindChild("ButtonIcon").GetComponent<SpriteRenderer> ();
		 
		startPosition = transform.position;

		animator.SetTrigger( "Idle" );
		state = SkeletonState.Idle;
		iconRenderer.sprite = keyIcon;

		cheerAudio = Instantiate(Audio.Instance.skeletonCheer);
	}

	void Update()
	{
		if (!Game.Playing) return;

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
				Audio.PlayAudioSource(cheerAudio);
				state = SkeletonState.Idle;

				row.Cheer( this );
			}

			Debug.Log(name + " " + state);
		}
	}

	public KeyCode GetCheerKey()
	{
		return key;
	}

	public void SetCheerKey(KeyCode newKey, Sprite newKeyIcon)
	{
		key = newKey;
		keyIcon = newKeyIcon;
		iconRenderer.sprite = newKeyIcon;
	}
}
