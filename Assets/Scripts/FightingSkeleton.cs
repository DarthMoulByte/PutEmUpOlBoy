using UnityEngine;
using System.Collections;

public class FightingSkeleton : MonoBehaviour
{
	public float hp;

	private bool punch;
	public bool guardDown;

	private float timeOfGuardDown = 2f;
	private float lastGuardDownTime;
	private float guardDownEnd;
	private bool alive;
	private Animator animator;

	public Game.Team team;
	public FightingSkeleton otherSkeleton;

	private bool punchedOtherWhileGuardDown;

	void Start()
	{
		hp = Game.MaxHP;
        animator = GetComponent<Animator>();
		alive = true;
	}

	void Update()
	{
		if (!Game.Playing) return;

		DebugStuff();

		if (hp <= 0 && alive)
		{
			Death();
		}
		if (punch)
		{
			animator.SetTrigger("Punch");
			punch = false;
		}
		if (otherSkeleton.guardDown && !punchedOtherWhileGuardDown)
		{
			Punch();
			punchedOtherWhileGuardDown = true;
		}
		if (!otherSkeleton.guardDown && punchedOtherWhileGuardDown)
		{
			punchedOtherWhileGuardDown = false;
		}
		animator.SetBool("GuardIsDown", guardDown && Time.time < guardDownEnd );

		if (guardDown && Time.time > guardDownEnd)
		{
			guardDown = false;
		}
	}

	private void DebugStuff()
	{
		if (Input.GetKeyDown( team == Game.Team.Left ? KeyCode.F1 : KeyCode.F5 ))
		{
			Punch();
		}
		if (Input.GetKeyDown( team == Game.Team.Left ? KeyCode.F2 : KeyCode.F6 ))
		{
			Hit();
		}
		if (Input.GetKeyDown( team == Game.Team.Left ? KeyCode.F3 : KeyCode.F7 ))
		{
			GuardDown();
		}

	}

	private void Death()
	{
		alive = false;
        animator.SetTrigger("Death");
		Audio.PlayAudioSource(Audio.Instance.skeletonDeath);
		otherSkeleton.Victory();
	}

	private void Victory()
	{
		animator.SetTrigger("Victory");
		Audio.PlayAudioSource( Audio.Instance.winGame );

	}

	public void Punch()
	{
		if (guardDown) return;
		Audio.PlayAudioSource( Audio.Instance.skeletonSmack );

		punch = true;
			
		otherSkeleton.Hit();
	}

	public void GuardDown()
	{
		animator.SetTrigger( "Guard Down" );
		Audio.PlayAudioSource( Audio.Instance.skeletonSad );

		lastGuardDownTime = Time.time;
		guardDownEnd = timeOfGuardDown + lastGuardDownTime;
        guardDown = true;
	}

	public void Hit()
	{
		animator.SetTrigger( "Hit" );

		if (guardDown)
		{
			hp -= 1;
		}
		else
		{
			hp -= 1;
		}
		hp = Mathf.Clamp(hp, 0, Game.MaxHP);
	}
}
