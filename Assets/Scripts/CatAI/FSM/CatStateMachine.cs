//Written by Jayden Hunter

using System.Collections.Generic;
using UnityEngine;

public enum ECatState
{
	Idle,
	Wander,
	Follow,
	PlayBall,
	Eat,
	Sleep
}

[RequireComponent(typeof(CatStateIdle))]
[RequireComponent(typeof(CatStateWander))]
[RequireComponent(typeof(CatStateFollow))]
[RequireComponent(typeof(CatStatePlayBall))]
[RequireComponent(typeof(CatStateEat))]
public class CatStateMachine : MonoBehaviour
{
	private Dictionary<ECatState, CatState> states = null;
	private CatState currentState = null;
	private CatState previousState = null;
	private CatManager manager;

	private void Awake()
	{
		states = new Dictionary<ECatState, CatState>();

		states.Add(ECatState.Idle, GetComponent<CatStateIdle>());
		states.Add(ECatState.Wander, GetComponent<CatStateWander>());
		states.Add(ECatState.Follow, GetComponent<CatStateFollow>());
		states.Add(ECatState.PlayBall, GetComponent<CatStatePlayBall>());
		states.Add(ECatState.Eat, GetComponent<CatStateEat>());

		previousState = states[ECatState.Idle];

		manager = GetComponent<CatManager>();
	}

	private void Start()
	{
		ChangeState(ECatState.Idle);		
	}

	private void Update()
	{
		if (currentState)
			currentState.OnUpdate();
	}

	private void FixedUpdate()
	{
		if (currentState)
			currentState.OnFixedUpdate();
	}

	public void ChangeState(ECatState desiredState)
	{
		if(currentState)
		{
			previousState = currentState;
			currentState.OnExit();
		}
		currentState = states[desiredState];
		currentState.OnEnter();
	}

	public CatState CurrentState { get => currentState; }
	public CatState PreviousState { get => previousState;}
	public CatManager Manager { get => manager; }
}
