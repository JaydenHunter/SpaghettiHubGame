//Written by Jayden Hunter
using UnityEngine;

/// <summary>
/// Manages all functions of the cat
/// </summary>
[RequireComponent(typeof(CatStateMachine))]
[RequireComponent(typeof(CatSoundManager))]
[RequireComponent(typeof(CatMovementHandler))]
[RequireComponent(typeof(CatAnimationHandler))]
[RequireComponent(typeof(CatMoodHandler))]
public class CatManager : MonoBehaviour
{
	private CatStateMachine stateMachine; //Reference to the state machine
	private CatSoundManager soundManager; //Reference to the sound manager
	private CatMovementHandler movementHandler; //Reference to the movement manager
	private CatAnimationHandler animationHandler; //Reference to the animation manager
	private CatMoodHandler moodHandler; //Reference to the mood manager

	private void Awake()
	{
		//Get Required Components
		stateMachine = GetComponent<CatStateMachine>();
		soundManager = GetComponent<CatSoundManager>();
		movementHandler = GetComponent<CatMovementHandler>();
		animationHandler = GetComponent<CatAnimationHandler>();
		moodHandler = GetComponent<CatMoodHandler>();
	}


	private void Update()
	{
		//Update the mood handler
		moodHandler.HandleMood();
	}

	//Properties
	public CatStateMachine StateMachine { get => stateMachine;}
	public CatSoundManager Sound { get => soundManager;}
	public CatMovementHandler Movement { get => movementHandler; }
	public CatAnimationHandler Animation { get => animationHandler; }
	public CatMoodHandler Mood { get => moodHandler; }
}
