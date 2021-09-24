//Written by Jayden Hunter
using UnityEngine;

/// <summary>
/// Used for events to Save the current Game State
/// </summary>
public class SaveOnEvent : MonoBehaviour
{
	private SaveManager saveManager; //Reference to the save manager

	private void Awake()
	{
		//Get the saveManager component from the game manager
		saveManager = GameObject.Find("GameManager").GetComponent<SaveManager>();
	}

	/// <summary>
	/// Calls the Save manager to save the game
	/// </summary>
	public void SaveGame()
	{
		saveManager.Save();
	}
}
