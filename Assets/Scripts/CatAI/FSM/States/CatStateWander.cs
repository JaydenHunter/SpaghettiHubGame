//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CatStateWander : CatState
{
	public Transform wanderTarget = null;
	[SerializeField] private float wanderRadius = 5;
	private float distanceThreshold = 1f;
	private float stationaryTime = 3f;
	public float stationaryTimer;
	private float maxSpeed = 1;

	[SerializeField] private Vector3 wanderAreaOrigin = Vector3.zero;
	[SerializeField] private Vector3 wanderAreaSize = Vector3.zero;




	public override void OnEnter()
	{
		stationaryTimer = stationaryTime;
		RandomizeWanderPos();
		manager.Animation.PlayAnimation(CatAnimation.Idle);
		manager.Movement.MaxSpeed = maxSpeed;
	}

	public override void OnUpdate()
	{
		if (manager.Mood.CheckPriorityChange(ECatState.Wander))
		{
			manager.StateMachine.ChangeState(ECatState.Idle);
		}
	}

	public override void OnFixedUpdate()
	{
		float distanceToTarget = Vector3.Distance(transform.position, wanderTarget.position);

		if (distanceToTarget < distanceThreshold)
		{
			manager.Animation.PlayAnimation(CatAnimation.Idle);

			stationaryTimer -= Time.fixedDeltaTime;
			if (stationaryTimer <= 0)
			{
				stationaryTimer = stationaryTime;
				RandomizeWanderPos();
				stateMachine.ChangeState(ECatState.Idle);
			}
		}
		else
		{
			manager.Movement.MoveTowardsTargetWithRotation();
		}
	}

	public override void OnExit()
	{

	}

	private void RandomizeWanderPos()
	{
		float halfX = wanderAreaSize.x / 2;
		float halfZ = wanderAreaSize.z / 2;
		 Vector3 newPos = new Vector3(Random.Range(-halfX,halfX), 0, Random.Range(-halfZ,halfZ));
		newPos.x += wanderAreaOrigin.x;
		newPos.z += wanderAreaOrigin.z;
		wanderTarget.position = newPos;
		manager.Movement.FollowTarget = wanderTarget;
	}

#if UNITY_EDITOR

	[Header("Editor Tools")]
	public bool drawWanderArea = false;
	public bool drawWanderTarget = false;
	public Color wanderAreaColor;
	private void OnDrawGizmosSelected()
	{
		if (drawWanderArea)
			DrawWanderArea();
		if (drawWanderTarget)
			DrawWanderTarget();
	}

	private void DrawWanderArea()
	{
		Gizmos.color = wanderAreaColor;
		Gizmos.DrawCube(wanderAreaOrigin, wanderAreaSize);
	}

	private void DrawWanderTarget()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(wanderTarget.position,0.25f);
	}
#endif

	public Vector3 WanderAreaOrigin { get => wanderAreaOrigin; set => wanderAreaOrigin = value; }
	public Vector3 WanderAreaSize { get => wanderAreaSize; set => wanderAreaSize = value; }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CatStateWander))]
public class CatStateWanderEditor : Editor
{
	CatStateWander wanderState;

	private void OnEnable()
	{
		wanderState = (CatStateWander)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

	}

	private void OnSceneGUI()
	{
		if (wanderState.drawWanderArea)
		{
			Handles.color = Color.red;
			Handles.DrawWireCube(wanderState.WanderAreaOrigin, wanderState.WanderAreaSize);
			Vector3 offSetPos = wanderState.WanderAreaOrigin;
			offSetPos.y += 5;
			wanderState.WanderAreaSize = Handles.ScaleHandle(wanderState.WanderAreaSize, offSetPos, wanderState.transform.rotation, 1);
			wanderState.WanderAreaOrigin = Handles.PositionHandle(wanderState.WanderAreaOrigin, wanderState.transform.rotation);
		}
	}


}
#endif