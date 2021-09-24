//Written by Jayden Hunter
using UnityEngine;

/// <summary>
/// Manages the saving and loading for the game
/// </summary>
public class SaveManager : MonoBehaviour
{
	CatMoodHandler mood; //Reference to the cat's mood handler
	GameManager gameManager; //Reference to the game manager

	private void Awake()
	{
		//Get the reference to the game manager
		gameManager = GetComponent<GameManager>();
	}

	private void Start()
	{
		//If the reference to the cat manager is not null...
		if (gameManager.CatMoodManager != null)
		{
			//..We are in the main cat scene and should load the normal data
			mood = gameManager.CatMoodManager;
			Load();
		}
		else
		{
			//...Else we are in the black jack scene and should load it's data
			LoadBlackJack();
		}
	}

	/// <summary>
	/// Simple Save System to save the cat's mood and player's money
	/// </summary>
	public void Save()
	{
		PlayerPrefs.SetInt("Money", gameManager.money);
		PlayerPrefs.SetFloat("Hunger", mood.Hunger);
		PlayerPrefs.SetFloat("Boredom", mood.Boredom);
		PlayerPrefs.SetFloat("Tiredness", mood.Tiredness);
		PlayerPrefs.SetFloat("Loneliness", mood.Loneliness);
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
