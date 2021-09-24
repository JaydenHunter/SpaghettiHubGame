//Written by Jayden Hunter

using UnityEngine;

/// <summary>
/// Manages most of the basic game logic such as money
/// </summary>
public class GameManager : MonoBehaviour
{
	public bool SceneHasCat = false;			//Determines if the current scene has the cat
	public int money = 0;						//The amount of money the player currently has
	public int moneyPerIntervalHappy = 3;		//Money gained per interval when the cat is happy
	public int moneyPerIntervalModerate = 2;	//Money gained per interval when the cat is moderate
	public int moneyPerIntervalOther = 1;		//Money gained per interval when the cat is in any other mood
	public CatMoodHandler mood;					//The cat's current mood

	private float timer = 5;

	private void Awake()
	{
		//Find the cat object if the current scene has the cat
		if (SceneHasCat)
			mood = GameObject.Find("Cat").GetComponent<CatMoodHandler>();
	}

	private void Update()
	{
		//If the current scene has a cat...
		if (SceneHasCat)
		{
			timer -= Time.deltaTime;
			//...And Timer is at or below 0...
			if (timer <= 0)
			{
				//...Increase money each interval depening on it's current mood
				switch (mood.CurrentMood)
				{
					case MoodStatus.Happy:
						money += moneyPerIntervalHappy;
						break;
					case MoodStatus.Moderate:
						money += moneyPerIntervalModerate;
						break;
					default:
						money += moneyPerIntervalOther;
						break;
				}

				//Reset the timer
				timer = 5;
			}
		}

		//Used for testing to gain money once space is pressed
		if(Input.GetKeyDown(KeyCode.Space))
		{
			money += 50;
		}
	}

	public CatMoodHandler CatMoodManager { get => mood; }
}
