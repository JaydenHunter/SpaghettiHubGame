//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CatStateMachine))]
[RequireComponent(typeof(CatSoundManager))]
[RequireComponent(typeof(CatMovementHandler))]
[RequireComponent(typeof(CatAnimationHandler))]
public class CatManager : MonoBehaviour
{
	private CatStateMachine stateMachine;
	private CatSoundManager soundManager;
	private CatMovementHandler movementHandler;
	private CatAnimationHandler animationHandler;

	private void Awake()
	{
		stateMachine = GetComponent<CatStateMachine>();
		soundManager = GetComponent<CatSoundManager>();
		movementHandler = GetComponent<CatMovementHandler>();
		animationHandler = GetComponent<CatAnimationHandler>();
	}

	//Properties
	public CatStateMachine StateMachine { get => stateMachine;}
	public CatSoundManager SoundManager { get => soundManager;}
	public CatMovementHandler MovementHandler { get => movementHandler; }
	public CatAnimationHandler AnimationHandler { get => animationHandler; }
}
