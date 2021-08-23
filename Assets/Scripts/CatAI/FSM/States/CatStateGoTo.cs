using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateGoTo : CatState
{
	private Vector3 targetPosition = Vector3.zero;

	public override void OnEnter()
	{
		stateMachine.AnimationHandler.PlayAnimation(CatAnimation.Walk);
	}

	public override void OnUpdate()
	{
		
	}

	public override void OnFixedUpdate()
	{
		stateMachine.MovementHandler.MoveTowardsTargetWithRotation();
	}

	public override void OnExit()
	{

	}
}