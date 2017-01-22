using UnityEngine;
using System.Collections;

public class SkeletonAI : MonoBehaviour
{
	private FightingSkeleton skeleton;

	private bool punchedAtEndOfGame;

	void Start()
	{
		Game.Instance.NewRound += NewRound;

		skeleton = GetComponent<FightingSkeleton>();
	}

	void Update()
	{
		if (Game.Instance.timer.timer <= 0.1f && !punchedAtEndOfGame)
		{
			skeleton.Punch();
			punchedAtEndOfGame = true;
		}
	}

	private void NewRound()
	{
		punchedAtEndOfGame = false;
	}
}
