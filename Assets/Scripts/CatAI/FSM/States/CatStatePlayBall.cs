//Written by Jayden Hunter
using UnityEngine;

public class CatStatePlayBall : CatState
{
	public Transform ball;
	private float maxSpeed = 4;
	public float boredomReduction = 0.05f;

	public override void OnEnter()
	{
		manager.Animation.PlayAnimation(CatAnimation.Walk);
		manager.Movement.FollowTarget = ball;
		manager.Movement.MaxSpeed = maxSpeed;
	}

	public override void OnUpdate()
	{
		
	}

	public override void OnFixedUpdate()
	{
		manager.Movement.MoveTowardsTargetWithRotation(true);

		if (manager.Mood.CheckPriorityChange(ECatState.PlayBall))
			stateMachine.ChangeState(ECatState.Idle);

		manager.Mood.boredom -= Utils.ModifyFloatWithLimit(boredomReduction * Time.deltaTime, 100);
	}

	public override void OnExit()
	{

	}
}