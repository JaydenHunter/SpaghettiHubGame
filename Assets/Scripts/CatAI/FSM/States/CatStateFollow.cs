//Written by Jayden Hunter
using UnityEngine;

/// <summary>
/// Handles the Follow State of the Cat
/// </summary>
public class CatStateFollow : CatState
{
	private Transform followTransform = null;

	public override void OnEnter()
	{
		manager.Animation.PlayAnimation(CatAnimation.Walk);
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
