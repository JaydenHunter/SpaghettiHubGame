//Written by Jayden Hunter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool SceneHasCat = false;
	public int money = 0;
	public int moneyPerSecondHappy = 1;
	public int moneyPerSecondModerate = 1;
	public int moneyPerSecondOther = 0;
	public CatMoodHandler mood;
	private float timer = 5;

	private void Awake()
	{
		if (SceneHasCat)
			mood = GameObject.Find("Cat").GetComponent<CatMoodHandler>();
	}

	private void Update()
	{
		if (SceneHasCat)
		{
			timer -= Time.deltaTime;

			if (timer <= 0)
			{
				switch (mood.CurrentMood)
				{
					case MoodStatus.Happy:
						money += moneyPerSecondHappy;
						break;
					case MoodStatus.Moderate:
						money += moneyPerSecondModerate;
						break;
					default:
						money += moneyPerSecondOther;
						break;
				}

				timer = 5;
			}
		}
	}

	public CatMoodHandler CatMoodManager { get => mood; }
}
