//Written by Jayden Hunter

/// <summary>
/// Handles the idle state of the Cat
/// </summary>
public class CatStateIdle : CatState
{
	public override void OnEnter()
	{
		manager.Animation.PlayAnimation(CatAnimation.Idle);
	}

	public override void OnUpdate()
	{
		switch (manager.Mood.CurrentMood)
		{
			case MoodStatus.Hungry:
				stateMachine.ChangeState(ECatState.Eat);
				break;
			case MoodStatus.Bored:
				stateMachine.ChangeState(ECatState.PlayBall);
				break;
			case MoodStatus.Lonely:
				break;
			case MoodStatus.Happy:
				stateMachine.ChangeState(ECatState.Wander);
				break;
			case MoodStatus.Moderate:
				stateMachine.ChangeState(manager.Mood.GetDesiredState());
				break;
			case MoodStatus.Tired:
				break;
		}
	}

	public override void OnFixedUpdate()
	{

	}

	public override void OnExit()
	{

	}
}