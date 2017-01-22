using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SkeletonRow : MonoBehaviour
{
	public Game.Team team;

	public float cheerFactor;

	[SerializeField] private List<CrowdSkeleton> row;

	private CrowdSkeleton lastCheeringSkeleton;

	public FightingSkeleton teamSkeleton;

	public void Cheer(CrowdSkeleton skeleton)
	{
		int thisSkeleton = row.IndexOf(skeleton);
		int lastSkeleton = row.IndexOf(lastCheeringSkeleton);

		if (thisSkeleton == lastSkeleton + 1)
		{
			cheerFactor += Game.Instance.CheerUpAmount;
		}
		else if (thisSkeleton == 0 && lastSkeleton == 3)
		{
			cheerFactor += Game.Instance.CheerUpAmount;
		}
		else if(lastCheeringSkeleton == null)
		{
			cheerFactor += Game.Instance.CheerUpAmount;
		}
		else
		{
			cheerFactor += Game.Instance.CheerDownAmount;
		}

		lastCheeringSkeleton = skeleton;
	}

	void Start()
	{
		row = GetComponentsInChildren<CrowdSkeleton>().ToList();

		if (team == Game.Team.Left)
		{
			Game.Instance.leftTeam = team;
		}
		if (team == Game.Team.Right)
		{
			Game.Instance.rightTeam = team;
		}
	}

	void Update()
	{
		if (cheerFactor > 1f)
		{
			teamSkeleton.Punch();
			cheerFactor = 0;
		}
		if (cheerFactor < -1f)
		{
			teamSkeleton.GuardDown();
			cheerFactor = 0;
		}

		if (Input.GetKeyDown (KeyCode.N)) 
		{
			RandomizeCheerKeys ();
		}


		//foreach (var crowdSkeleton in row)
		//{
		//	if (crowdSkeleton.Cheer == null)
		//	{
		//		
		//	}
		//}
	}

	public void RandomizeCheerKeys()
	{
		var keysLeft = row.Select (s => new {
			s.key,
			s.keyIcon,
		}).ToList ();

		List<CrowdSkeleton> skeletonsLeft = new List<CrowdSkeleton> ();
		skeletonsLeft.AddRange (row);

		for (var i = 0; i < row.Count; i++) 
		{
			int randomSkeletonIndex = UnityEngine.Random.Range (0, skeletonsLeft.Count);
			int randomKeysIndex = UnityEngine.Random.Range (0, keysLeft.Count);

			skeletonsLeft [randomSkeletonIndex].SetCheerKey (keysLeft [randomKeysIndex].key, keysLeft [randomKeysIndex].keyIcon);

			skeletonsLeft.RemoveAt (randomSkeletonIndex);
			keysLeft.RemoveAt (randomKeysIndex);
		}
	}
}
