using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateWander : CatState
{
	public override void OnEnter()
	{
		stateMachine.AnimationHandler.PlayAnimation(CatAnimation.Walk);
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
