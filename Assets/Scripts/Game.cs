using System;
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

	public float CheerUpAmount = 0.1f;
	public float CheerDownAmount = -0.1f;

	public static float MaxHP = 5;

	public Team leftTeam;
	public Team rightTeam;

	public GameObject startScreenObject;

	public Timer timer;

	public static float TimerMax = 15;

	public Action NewRound;

	[System.Serializable]
	public class CountdownObjects
	{
		public GameObject three;
		public GameObject two;
		public GameObject one;
		public GameObject go;
		public GameObject timer;
	}

	public CountdownObjects countdownObjects;

	public enum GameState
	{
		StartScreen,
		Countdown,
		Playing,
		EndScreen,
	}

	public static GameState state;

	public enum Team
	{
		Left = 0,
		Right = 1,
	}

	public static bool Playing;

	private void Start()
	{
		if (timer == null) timer = FindObjectOfType<Timer>();

		state = GameState.StartScreen;
		ShowStartScreen();
	}

	private void Update()
	{
		Playing = state == GameState.Playing;

		if (state == GameState.StartScreen)
		{
			if (Input.anyKeyDown)
			{
				if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
				{
					HideStartScreen();
				}
			}

		}
	}

	private void ShowStartScreen()
	{
		Audio.PlayAudioSource(Audio.Instance.mainMenu);
		startScreenObject.SetActive(true);
	}

	private void HideStartScreen()
	{
		startScreenObject.SetActive(false);
		StartCountDown(0.5f);
		Audio.StopAudioSource( Audio.Instance.mainMenu, 3f);
	}

	private static void StartCountDown(float delayBeforeCountdown = 0f)
	{
		state = GameState.Countdown;
		_instance.StartCoroutine(_instance.CountDown(delayBeforeCountdown));
		_instance.NewRound();
	}

	public float counterDelay = 0.3f;

	public IEnumerator CountDown(float delayBeforeCountdown)
	{
		yield return new WaitForSeconds( delayBeforeCountdown );

		countdownObjects.three.SetActive(false);
		countdownObjects.two.SetActive(false);
		countdownObjects.one.SetActive(false);
		countdownObjects.go.SetActive(false);

		countdownObjects.three.SetActive( true );

		Audio.PlayAudioSource(Audio.Instance.three);

		yield return new WaitForSeconds( counterDelay );

		countdownObjects.three.SetActive( false );
		countdownObjects.two.SetActive( true );
		Audio.PlayAudioSource( Audio.Instance.two );

		yield return new WaitForSeconds( counterDelay );

		countdownObjects.two.SetActive( false );
		countdownObjects.one.SetActive( true );
		Audio.PlayAudioSource( Audio.Instance.one );

		yield return new WaitForSeconds( counterDelay );

		countdownObjects.one.SetActive( false );
		countdownObjects.go.SetActive( true );
		Audio.PlayAudioSource( Audio.Instance.go );

		yield return new WaitForSeconds( counterDelay * 1.2f);

		countdownObjects.go.SetActive( false );

		RestartTimer();

		state = GameState.Playing;
	}

	public static void RestartTimer()
	{
		_instance.timer.RestartTimer();
	}

	public static void TimeIsUp()
	{
		state = GameState.Countdown;
		StartCountDown(2f);
	}
}
