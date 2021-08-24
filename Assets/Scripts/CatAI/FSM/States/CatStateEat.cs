//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateEat : CatState
{
	public Transform foodBowl;
	public float minDistToEat = 1.0f;
	public float hungerReduction = -0.05f; //Put this on the food
	private float maxSpeed = 1;
	public override void OnEnter()
	{
		manager.Animation.PlayAnimation(CatAnimation.Eat);
		manager.Movement.FollowTarget = foodBowl;
		manager.Movement.MaxSpeed = maxSpeed;
	}

	public override void OnUpdate()
	{
		manager.Movement.MaxSpeed = maxSpeed;
		if (Vector3.Distance(transform.position, foodBowl.position) > minDistToEat)
		{
			manager.Movement.MoveTowardsTargetWithRotation();
		}
		else
		{
			manager.Mood.hunger += Utils.ModifyFloatWithLimit(hungerReduction * Time.deltaTime, 100);
			manager.Animation.PlayAnimation(CatAnimation.Eat);
		}

		if (manager.Mood.CheckPriorityChange(ECatState.Eat))
			stateMachine.ChangeState(ECatState.Idle);
	}

	public override void OnFixedUpdate()
	{

	}

	public override void OnExit()
	{

	}
}
