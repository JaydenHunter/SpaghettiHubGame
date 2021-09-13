//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLifeTime : MonoBehaviour
{
	public float lifeTimeOnActive;

	private float timer;

	private void Start()
	{
		timer = lifeTimeOnActive;
	}

	private void Update()
	{
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			timer = lifeTimeOnActive;
			gameObject.SetActive(false);
		}
	}
}
