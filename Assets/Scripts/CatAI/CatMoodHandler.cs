//Written by Jayden Hunter
using TMPro;
using UnityEngine;

//Tracks what current mood the cat is in
public enum MoodStatus
{
	Hungry,
	Bored,
	Lonely,
	Happy,
	Moderate,
	Tired
}

/// <summary>
/// Handles the mood of the cat
/// </summary>
public class CatMoodHandler : MonoBehaviour
{
	public bool mainScene = false;  //Check if we are in the main scene o  not
	public GameObject ball; //Reference to the play ball
	public GameObject foodBowl; //Reference to the food bowl the cat will eat from
	public TextMeshProUGUI moodTMPro; //Text display of the cat's mood

	private CatManager manager;

	[SerializeField] private MoodStatus currentMood = MoodStatus.Moderate; //Current mood of the cat

	[SerializeField] private float boredomRatePerSecond = 0.01f; //How much the boredom increases per second
	[SerializeField] private float hungerRatePerSecond = 0.01f; //How much the hunger increases per second
	[SerializeField] private float lonelinessRatePerSecond = 0.01f; //How much the loneliness increases per second
	[SerializeField] private float tirednessRatePerSecond = 0.01f; //How much the tiredness increases per second

	[SerializeField] private float boredomThreshold = 80.0f; //Threshold until the cat becomes bored
	[SerializeField] private float hungerThreshold = 80.0f;	//Threshold until the cat becomes hungry
	[SerializeField] private float lonelinessThreshold = 80.0f; //Threshold untl the cat becomes lonely
	[SerializeField] private float tirednessThreshold = 80.0f; //Threshold until the cat becomes tired

	[SerializeField] private float maxBoredom = 100; //Max boredome value the cat can have
	[SerializeField] public float boredom = 50; //Starting boredom rate

	[SerializeField] private float maxHunger = 100; //Max hunger value the cat can have
	[SerializeField] public float hunger = 70; //Starting hunger rate

	[SerializeField] private float maxLoneliness = 100; //Max loneliness value the cat can have
	[SerializeField] public float loneliness = 30; //Starting loneliness rate

	[SerializeField] private float maxTiredness = 100; //Max tiredness value the cat can have
	[SerializeField] public float tiredness = 25; //Starting tiredness value

	[SerializeField] private float happyThreshold = 60; //Happiness threshold of the cat

	private void Awake()
	{
		manager = GetComponent<CatManager>();
	}

	/// <summary>
	/// Updates the text display of the cat's current mood
	/// </summary>
	private void UpdateMoodText()
	{
		if (mainScene)
			moodTMPro.text = $"Mood: {currentMood}";
	}

	/// <summary>
	/// Handles the mood of the cat by checking each status
	/// </summary>
	public void HandleMood()
	{
		//Increment all statuses 
		boredom += Utils.ModifyFloatWithLimit(boredomRatePerSecond * Time.deltaTime, maxBoredom);
		hunger += Utils.ModifyFloatWithLimit(hungerRatePerSecond * Time.deltaTime, maxHunger);
		loneliness += Utils.ModifyFloatWithLimit(lonelinessRatePerSecond * Time.deltaTime, maxLoneliness);
		tiredness += Utils.ModifyFloatWithLimit(tirednessRatePerSecond * Time.deltaTime, maxTiredness);

		//Check which mood we should be in with a priority going top to bottom
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
		else if (hunger < happyThreshold && tiredness < happyThreshold && loneliness < happyThreshold && boredom < happyThreshold)
		{
			currentMood = MoodStatus.Happy;
		}
		else
		{
			currentMood = MoodStatus.Moderate;
		}

		UpdateMoodText();
	}

	/// <summary>
	/// Checks if the priority has changed to something more important while in an active state
	/// </summary>
	/// <param name="currentState"></param>
	/// <returns></returns>
	public bool CheckPriorityChange(ECatState currentState)
	{
		if (currentState == ECatState.PlayBall && ball.activeInHierarchy)
		{
			if (currentMood == MoodStatus.Hungry || currentMood == MoodStatus.Tired)
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

	/// <summary>
	/// Gets the desired state that the cat should be in
	/// </summary>
	/// <returns></returns>
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
		return desiredState;
	}

	//Properties
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
