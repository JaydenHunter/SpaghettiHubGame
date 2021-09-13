using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleItem : MonoBehaviour
{

	public GameObject item;
	public bool resetPosition = true;
	private Vector3 spawnLocation;
	// Start is called before the first frame update
	void Start()
	{
		spawnLocation = item.transform.position;
		item.SetActive(false);
	}

	public void ToggleActive()
	{
		bool active = item.activeInHierarchy;
		if(!active)
		{
			item.transform.position = spawnLocation;
		}

		item.SetActive(!active);
	}

}
