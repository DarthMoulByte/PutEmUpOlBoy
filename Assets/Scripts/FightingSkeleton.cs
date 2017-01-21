using UnityEngine;
using System.Collections;

public class FightingSkeleton : MonoBehaviour
{
	public int hp = 10;

	private bool punch;
	private bool guardDown;

	private float timeOfGuardDown = 2f;
	private float lastPunchTime;
	private float lastGuardDownTime;
	private float guardDownEnd;

	private Animator animator;

	public FightingSkeleton otherSkeleton;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			Punch();
		}
		if (Input.GetKeyDown(KeyCode.F2))
		{
			Hit();
		}
		if (Input.GetKeyDown(KeyCode.F3))
		{
			GuardDown();
		}
		if (hp <= 0)
		{
			Debug.Log("DED");
			Debug.Break();
		}
		if (punch)
		{
			animator.SetTrigger("Punch");
			punch = false;
			Debug.Log("Punching");
		}

		animator.SetBool("GuardIsDown", guardDown && Time.time < guardDownEnd );

		if (guardDown && Time.time > guardDownEnd)
		{
			Debug.Log("Exiting Guard Down");
			guardDown = false;
		}
	}

	public void Punch()
	{
		if (guardDown) return;

		lastPunchTime = Time.time;
		punch = true;

		otherSkeleton.Hit();
	}
	public void GuardDown()
	{
		Debug.Log("Entering Guard Down");
		animator.SetTrigger( "Guard Down" );

		lastGuardDownTime = Time.time;
		guardDownEnd = timeOfGuardDown + lastGuardDownTime;
        guardDown = true;
	}

	public void Hit()
	{
		animator.SetTrigger( "Hit" );
		if (guardDown)
		{
			hp -= 3;
		}
		else
		{
			hp -= 1;
		}
	}
}
