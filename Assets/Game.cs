using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public static Game Instance {
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<Game>();
			}
			return _instance;
		}
	}

	private static Game _instance;

	public static float CheerUpAmount = 0.1f;
	public static float CheerDownAmount = -0.1f;

	public Team leftTeam;
	public Team rightTeam;

	public enum Team
	{
		Left = 0,
		Right = 1,
	}

	void Start()
	{

	}

	void Update()
	{

	}
}
