using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Timer : MonoBehaviour
{
	public Transform timeScalerObject;

	public float timer;

	void Start()
	{
		timer = Game.TimerMax;
	}

	public void RestartTimer()
	{
		timer = Game.TimerMax;
	}

	void Update()
	{
		DebugStuff();
		if (!Game.Playing) return;

		timer -= Time.deltaTime;
		timeScalerObject.localScale = new Vector3((1/Game.TimerMax) * timer, 1f, 1f);

		if (timer <= 0)
		{
			TimeIsUp();
		}
	}

	private void DebugStuff()
	{
		if (Input.GetKeyDown( KeyCode.KeypadMinus ))
		{
			timer -= 1f;	
		}
		if (Input.GetKeyDown( KeyCode.KeypadPlus ))
		{
			timer += 1f;
		}

	}

	private void TimeIsUp()
	{
		Game.TimeIsUp();
	}
}
