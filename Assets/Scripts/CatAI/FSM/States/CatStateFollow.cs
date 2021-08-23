using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateFollow : CatState
{
	private Transform followTransform = null;

	public override void OnEnter()
	{
		stateMachine.AnimationHandler.PlayAnimation(CatAnimation.Walk);
	}

	public override void OnUpdate()
	{
		if(followTransform)
		{

		}
	}

	public override void OnFixedUpdate()
	{
		
	}

	public override void OnExit()
	{

	}
}
