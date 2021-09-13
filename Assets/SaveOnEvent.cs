//Written by Jayden Hunter
using UnityEngine;

public class SaveOnEvent : MonoBehaviour
{
	private SaveManager saveManager;

	private void Awake()
	{
		saveManager = GameObject.Find("GameManager").GetComponent<SaveManager>();
	}
	public void SaveGame()
	{
		saveManager.Save();
	}
}
