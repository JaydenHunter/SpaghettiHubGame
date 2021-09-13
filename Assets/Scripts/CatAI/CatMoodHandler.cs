//Written by Jayden Hunter
using TMPro;
using UnityEngine;

public enum MoodStatus
{
	Hungry,
	Bored,
	Lonely,
	Happy,
	Moderate,
	Tired
}

public class CatMoodHandler : MonoBehaviour
{
	public GameObject ball;
	public GameObject foodBowl;
	public TextMeshProUGUI moodTMPro;

	private CatManager manager;

	[SerializeField] private MoodStatus currentMood = MoodStatus.Moderate;

	[SerializeField] private float boredomRatePerSecond = 0.01f;
	[SerializeField] private float hungerRatePerSecond = 0.01f;
	[SerializeField] private float lonelinessRatePerSecond = 0.01f;
	[SerializeField] private float tirednessRatePerSecond = 0.01f;

	[SerializeField] private float boredomThreshold = 80.0f;
	[SerializeField] private float hungerThreshold = 80.0f;
	[SerializeField] private float lonelinessThreshold = 80.0f;
	[SerializeField] private float tirednessThreshold = 80.0f;

	[SerializeField] private float maxBoredom = 100;
	[SerializeField] public float boredom = 50;
	[SerializeField] private float maxHunger = 100;
	[SerializeField] public float hunger = 70;
	[SerializeField] private float maxLoneliness = 100;
	[SerializeField] public float loneliness = 30;
	[SerializeField] private float maxTiredness = 100;
	[SerializeField] public float tiredness = 25;

	[SerializeField] private float happyThreshold = 60;

	private void Awake()
	{
		manager = GetComponent<CatManager>();
	}

	private void UpdateMoodText()
	{
		moodTMPro.text = $"Mood: {currentMood}";
	}

	public void HandleMood()
	{
		
		boredom += Utils.ModifyFloatWithLimit(boredomRatePerSecond * Time.deltaTime, maxBoredom);
		hunger += Utils.ModifyFloatWithLimit(hungerRatePerSecond * Time.deltaTime, maxHunger);
		loneliness += Utils.ModifyFloatWithLimit(lonelinessRatePerSecond * Time.deltaTime, maxLoneliness);
		tiredness += Utils.ModifyFloatWithLimit(tirednessRatePerSecond * Time.deltaTime, maxTiredness);

		if (hunger >= hungerThreshold)
		{
			currentMood = MoodStatus.Hungry;
		}
		else if (tiredness >= tirednessThreshold)
		{
			currentMood = MoodStatus.Tired;
		}
		else if (loneliness >= lonelinessThreshold)
		{
			currentMood = MoodStatus.Lonely;
		}
		else if (boredom >= boredomThreshold)
		{
			currentMood = MoodStatus.Bored;
		}
		else if (hunger < happyThreshold && tiredness < happyThreshold  && loneliness < happyThreshold && boredom < happyThreshold)
		{
			currentMood = MoodStatus.Happy;
		}
		else
		{
			currentMood = MoodStatus.Moderate;
		}

		UpdateMoodText();
	}

	public bool CheckPriorityChange(ECatState currentState)
	{
		if (currentState == ECatState.PlayBall && ball.activeInHierarchy)
		{
			if (currentMood == MoodStatus.Hungry || currentMood == MoodStatus.Tired )
				return true;
			else if (boredom <= 10)
				return true;
		}

		else if (currentState == ECatState.Eat && foodBowl.activeInHierarchy)
		{
			if (hunger <= 10)
				return true;
		}
		else if (currentState == ECatState.Wander)
		{
			if (currentMood != MoodStatus.Moderate && currentMood != MoodStatus.Happy)
				return true;
		}
		return false;
	}

	public ECatState GetDesiredState()
	{
		float value = happyThreshold;
		ECatState desiredState = ECatState.Wander;

		if (hunger > value)
		{
			value = hunger;
			desiredState = ECatState.Eat;
		}
		if (boredom > value)
		{
			value = boredom;
			desiredState = ECatState.PlayBall;
		}
		if (loneliness > value)
		{
			value = loneliness;
			desiredState = ECatState.Wander; //Switch to want attention state
		}
		if (tiredness > value)
		{
			value = tiredness;
			desiredState = ECatState.Sleep;
		}
		Debug.Log($"Desired State: {desiredState.ToString()} | Value: {value}");
		return desiredState;
	}

	public MoodStatus CurrentMood { get => currentMood; }
	public float Boredom { get => boredom; set => boredom += Utils.ModifyFloatWithLimit(value * Time.deltaTime, maxBoredom); }
	public float Hunger { get => hunger; set => hunger += Utils.ModifyFloatWithLimit(value * Time.deltaTime, maxHunger); }
	public float Loneliness { get => loneliness; set => loneliness += Utils.ModifyFloatWithLimit(value * Time.deltaTime, maxLoneliness); }
	public float Tiredness { get => tiredness; set => tiredness += Utils.ModifyFloatWithLimit(value * Time.deltaTime, maxTiredness); }
	public float BoredomThreshold { get => boredomThreshold; }
	public float HungerThreshold { get => hungerThreshold; }
	public float LonelinessThreshold { get => lonelinessThreshold; }
	public float TirednessThreshold { get => tirednessThreshold; }
}
