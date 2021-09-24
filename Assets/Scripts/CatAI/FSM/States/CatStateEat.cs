//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateEat : CatState
{
	public Transform foodBowl;
	public float minDistToEat = 1.0f;
	public float hungerReduction = 0.0005f; //Put this on the food
	private float maxSpeed = 1;
	public override void OnEnter()
	{
		if (!CheckTransformExists())
			return;
		manager.Animation.PlayAnimation(CatAnimation.Eat);
		manager.Movement.FollowTarget = foodBowl;
		manager.Movement.MaxSpeed = maxSpeed;
	}

	public override void OnUpdate()
	{

		if (manager.Mood.CheckPriorityChange(ECatState.Eat))
			stateMachine.ChangeState(ECatState.Idle);
	}

	private bool CheckTransformExists()
	{
		if (!foodBowl.gameObject.activeInHierarchy)
		{
			stateMachine.ChangeState(ECatState.Idle);
			return false;
		}
		return true;
	}

	public override void OnFixedUpdate()
	{
		if (!CheckTransformExists())
			return;

		manager.Movement.MaxSpeed = maxSpeed;
		if (Vector3.Distance(transform.position, foodBowl.position) > minDistToEat)
		{
			manager.Movement.MoveTowardsTargetWithRotation();
		}
		else
		{
			manager.Mood.hunger -= Utils.ModifyFloatWithLimit(hungerReduction * Time.fixedDeltaTime, 100);
			manager.Animation.PlayAnimation(CatAnimation.Eat);
		}
	}

	public override void OnExit()
	{

	}
}
