//Written by Jayden Hunter

using UnityEngine;

/// <summary>
/// Use to disable an interactable object after a set amount of time
/// </summary>
public class InteractableLifeTime : MonoBehaviour
{
	public float lifeTimeOnActive; //Length of time the object is active for

	private float timer; //Timer to count down the lifetime

	private void Start()
	{
		timer = lifeTimeOnActive;
	}

	private void Update()
	{
		timer -= Time.deltaTime;

		//If the timer is at or below 0....
		if(timer <= 0)
		{
			//...Reset the timer and set the object to inactive
			timer = lifeTimeOnActive;
			gameObject.SetActive(false);
		}
	}
}
