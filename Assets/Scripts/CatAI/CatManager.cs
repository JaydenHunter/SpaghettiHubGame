//Written by Jayden Hunter
using UnityEngine;

[RequireComponent(typeof(CatStateMachine))]
[RequireComponent(typeof(CatSoundManager))]
[RequireComponent(typeof(CatMovementHandler))]
[RequireComponent(typeof(CatAnimationHandler))]
[RequireComponent(typeof(CatMoodHandler))]
public class CatManager : MonoBehaviour
{
	private CatStateMachine stateMachine;
	private CatSoundManager soundManager;
	private CatMovementHandler movementHandler;
	private CatAnimationHandler animationHandler;
	private CatMoodHandler moodHandler;

	private void Awake()
	{
		stateMachine = GetComponent<CatStateMachine>();
		soundManager = GetComponent<CatSoundManager>();
		movementHandler = GetComponent<CatMovementHandler>();
		animationHandler = GetComponent<CatAnimationHandler>();
		moodHandler = GetComponent<CatMoodHandler>();
	}


	private void Update()
	{
		moodHandler.HandleMood();
	}

	//Properties
	public CatStateMachine StateMachine { get => stateMachine;}
	public CatSoundManager Sound { get => soundManager;}
	public CatMovementHandler Movement { get => movementHandler; }
	public CatAnimationHandler Animation { get => animationHandler; }
	public CatMoodHandler Mood { get => moodHandler; }
}
