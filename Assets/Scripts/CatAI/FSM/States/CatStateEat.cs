//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateEat : CatState
{
	public override void OnEnter()
	{
		stateMachine.AnimationHandler.PlayAnimation(CatAnimation.Eat);
	}

	public override void OnUpdate()
	{

	}

	public override void OnFixedUpdate()
	{

	}

	public override void OnExit()
	{

	}
}
