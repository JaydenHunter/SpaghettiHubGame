//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateWander : CatState
{
	public Transform wanderTarget = null;
	[SerializeField] private float wanderRadius = 5;
	private float distanceThreshold = 1f;
	private float stationaryTime = 3f;
	private float stationaryTimer;
	private float maxSpeed = 1;
	public override void OnEnter()
	{
		stationaryTime = stationaryTimer;
		RandomizeWanderPos();
		manager.Animation.PlayAnimation(CatAnimation.Idle);
		manager.Movement.MaxSpeed = maxSpeed;
	}

	public override void OnUpdate()
	{
		if(manager.Mood.CheckPriorityChange(ECatState.Wander))
		{
			manager.StateMachine.ChangeState(ECatState.Idle);
		}
	}

	public override void OnFixedUpdate()
	{
		float distanceToTarget = Vector3.Distance(transform.position, wanderTarget.position);
		
		if(distanceToTarget < distanceThreshold)
		{
			manager.Animation.PlayAnimation(CatAnimation.Idle);
			
			stationaryTimer -= Time.fixedDeltaTime;
			if (stationaryTimer <= 0)
			{
				RandomizeWanderPos();
				stationaryTimer = stationaryTime;
			}
		}
		else
		{
			manager.Movement.MoveTowardsTargetWithRotation();
			Debug.Log($"Move To Target");
		}
	}

	public override void OnExit()
	{

	}

	private void RandomizeWanderPos()
	{
		wanderTarget.position = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius));
		manager.Movement.FollowTarget = wanderTarget;
	}
}
