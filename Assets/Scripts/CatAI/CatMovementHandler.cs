//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovementHandler : MonoBehaviour
{
	private Vector3 goToPosition = Vector2.zero;
	private Transform followTarget = null;
	private float wanderRadius = 10f;
	private float followSpeed = 10f;
	private float wanderSpeed = 10f;
	private float goToSpeed = 10f;
	[SerializeField] private float movementForce = 10f;
	[SerializeField] private float maxSpeed = 2f;
	[SerializeField] private float stoppingDistance = 0.25f;
	[SerializeField] private float hitDistance = 1.5f;
	[SerializeField] private float hitForce = 1000f;
	[SerializeField] private float hitCooldown = 2;
	private float hitTimer = 0;
	private float rotationSpeed = 35f;
	private CatManager manager;
	[SerializeField] private float animationSpeedMultiplier = 0.25f;
	[SerializeField] private float minVelocityToWalk = 0.25f;

	private Rigidbody rigidBody = null;

	private void Awake()
	{
		hitTimer = hitCooldown;
		manager = GetComponent<CatManager>();
		rigidBody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		followTarget = GameObject.Find("PlayBallTest").transform;
	}

	public void MoveTowardsTargetWithRotation()
	{

		hitTimer -= Time.deltaTime;

		if (followTarget)
		{
			float distToTarget = Vector3.Distance(transform.position, followTarget.position);
			if (distToTarget > stoppingDistance)
			{
				if (rigidBody.velocity.magnitude > minVelocityToWalk)
					manager.AnimationHandler.PlayAnimation(CatAnimation.Walk, rigidBody.velocity.magnitude * animationSpeedMultiplier);
				else
					manager.AnimationHandler.PlayAnimation(CatAnimation.Idle);
				//Lerp this to rotate slowly
				Vector3 lookAtVector = new Vector3(followTarget.position.x, transform.position.y, followTarget.position.z);
				transform.LookAt(lookAtVector);


				rigidBody.AddRelativeForce(Vector3.forward * movementForce * Time.deltaTime, ForceMode.Force);
				if (rigidBody.velocity.magnitude > maxSpeed)
					rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
			}

			//Make this bool
			if (distToTarget < hitDistance && hitTimer <= 0)
			{
				Rigidbody tarRB = followTarget.GetComponent<Rigidbody>();
				if (tarRB)
				{
					Vector3 forward = transform.forward;
					Vector3 up = transform.up;
					Vector3 randDir = new Vector3(Random.Range(0, 0.25f), Random.Range(0, 0.25f), Random.Range(0, 0.25f));
					Vector3 forceDir = forward + up + randDir;

					tarRB.AddRelativeForce(forceDir * hitForce, ForceMode.Impulse);
					manager.AnimationHandler.PlayAnimation(CatAnimation.Jump);
					manager.SoundManager.PlaySingle("BallHit", 0.85f, 1.15f);
					rigidBody.velocity = rigidBody.velocity.normalized * 0;
					hitTimer = hitCooldown;
				}
			}
		}


	}

	public Vector3 GoToPosition { get => goToPosition; set => goToPosition = value; }
	public Transform FollowTarget { get => followTarget; set => followTarget = value; }
	public float WanderRadius { get => wanderRadius; }
	public float FollowSpeed { get => followSpeed; }
	public float WanderSpeed { get => wanderSpeed; }
	public float GoToSpeed { get => goToSpeed; }
	public float MovementForce { get => movementForce; set => movementForce = value; }
}
