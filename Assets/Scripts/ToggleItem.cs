//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleItem : MonoBehaviour
{
	private GameManager gameManager;
	public GameObject item;
	public int cost;
	private Button button;

	public bool resetPosition = true;
	private Vector3 spawnLocation;
	// Start is called before the first frame update
	void Start()
	{
		button = GetComponent<Button>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		spawnLocation = item.transform.position;
		item.SetActive(false);
	}

	public void ToggleActive()
	{
		bool active = item.activeInHierarchy;
		if (!active)
		{
			if (gameManager.money < cost)
				return;

			item.transform.position = spawnLocation;
			gameManager.money -= cost;
		}

		item.SetActive(!active);
	}

	private void Update()
	{
		if (item.activeInHierarchy || cost > gameManager.money)
			button.interactable = false;
		else
			button.interactable = true;
	}

}
