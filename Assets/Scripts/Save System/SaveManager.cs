using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	CatMoodHandler mood;
	GameManager gameManager;

	private void Awake()
	{
		gameManager = GetComponent<GameManager>();
	}

	private void Start()
	{
		if (gameManager.CatMoodManager != null)
		{
			mood = gameManager.CatMoodManager;
			Load();
		}
			else
		{
			LoadBlackJack();
		}
	}
	private void Update()
	{

	}
	public void Save()
	{
		PlayerPrefs.SetInt("Money", gameManager.money);
		PlayerPrefs.SetFloat("Hunger", (int)mood.Hunger);
		PlayerPrefs.SetFloat("Boredom", (int)mood.Boredom);
		PlayerPrefs.SetFloat("Tiredness", (int)mood.Tiredness);
		PlayerPrefs.SetFloat("Loneliness", (int)mood.Loneliness);
	}

	public void SaveBlackJack()
	{
		PlayerPrefs.SetInt("Money", gameManager.money);
	}

	public void LoadBlackJack()
	{
		gameManager.money = PlayerPrefs.GetInt("Money", 100);
	}

	public void Load()
	{
		gameManager.money = PlayerPrefs.GetInt("Money", 100);
		mood.Hunger = PlayerPrefs.GetFloat("Hunger", 75);
		mood.Boredom = PlayerPrefs.GetFloat("Boredom", 65);
		mood.Tiredness = PlayerPrefs.GetFloat("Tiredness", 0);
		mood.Loneliness = PlayerPrefs.GetFloat("Loneliness", 0);
	}

}
