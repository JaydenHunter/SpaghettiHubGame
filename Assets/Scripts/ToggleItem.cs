//Written by Jayden Hunter
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Used for Toggling Items On and Off
/// </summary>
public class ToggleItem : MonoBehaviour
{
	JoyController controller;
	private GameManager gameManager;
	public GameObject item;
	public int cost;
	public bool resetPosition = true;		//Used to determine if it's position should be reset on toggle
	private Button button;
	private GameManager gameManager;		//Reference to Gamemanger script
	private Vector3 spawnLocation;			//Location where the item will spawn

	// Start is called before the first frame update
	void Start()
	{
		controller = GetComponent<JoyController>();
		button = GetComponent<Button>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		spawnLocation = item.transform.position;
		item.SetActive(false);

	}

	/// <summary>
	/// Toggle's whether the item is active or not
	/// </summary>
	public void ToggleActive(float joyDistance)
	{
		bool active = item.activeInHierarchy;

		//If the item is not currently active...
		if (!active)
		{
			//...And we don't have enough money, return out
			if (gameManager.money < cost)
				return;

			//...Else we subtract the money and reset the objects location
			item.transform.position = spawnLocation;
			gameManager.money -= cost;
		}

		//Toggles whether the object is active or not
		item.SetActive(!active);
	}


	private void Update()
	{
		//Check if we have enough money and whether it's active already or not
		//Used for enabling/disabling the button associated with toggling the item
		if (item.activeInHierarchy || cost > gameManager.money)
			button.interactable = false;
		else
			button.interactable = true;
	}

}
