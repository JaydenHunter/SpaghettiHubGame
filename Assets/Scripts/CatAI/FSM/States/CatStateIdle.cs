using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateIdle : CatState
{
	public override void OnEnter()
	{
		stateMachine.AnimationHandler.PlayAnimation(CatAnimation.Idle);
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