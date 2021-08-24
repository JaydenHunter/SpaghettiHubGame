//Written by Jayden Hunter
using UnityEngine;

public class CatMovementHandler : MonoBehaviour
{
	private Vector3 goToPosition = Vector2.zero;
	[SerializeField] private Transform followTarget = null;
	private float wanderRadius = 10f;
	private float followSpeed = 10f;
	private float wanderSpeed = 10f;
	private float goToSpeed = 10f;

	[SerializeField] private float hitDistance = 1.5f;
	[SerializeField] private float hitForce = 1000f;
	[SerializeField] private float hitCooldown = 2;
	private float hitTimer = 0;
	private CatManager manager;

	//Movement Fields
	[SerializeField] private float movementForce = 10f;
	[SerializeField] private float maxSpeed = 2f;
	[SerializeField] private float stoppingDistance = 0.25f;
	[SerializeField] private float animationSpeedMultiplier = 0.25f;
	[SerializeField] private float minVelocityToWalk = 0.25f;

	//Rotational Fields
	[SerializeField] private float rotationVelocity = 35f;

	private Rigidbody rigidBody = null;

	private void Awake()
	{
		hitTimer = hitCooldown;
		manager = GetComponent<CatManager>();
		rigidBody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		followTarget = GameObject.Find("PlayBall").transform;
	}

	public void MoveTowardsTargetWithRotation(bool hit = false)
	{

		hitTimer -= Time.deltaTime;

		if (followTarget)
		{
			float distToTarget = Vector3.Distance(transform.position, followTarget.position);
			if (distToTarget > stoppingDistance)
			{
				float currentVelocity = rigidBody.velocity.magnitude;
				if (currentVelocity > minVelocityToWalk)
					manager.Animation.PlayAnimation(CatAnimation.Walk, currentVelocity * animationSpeedMultiplier);
				else
					manager.Animation.PlayAnimation(CatAnimation.Idle);


				if (currentVelocity > maxSpeed)
					rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;

				//Rotate the Cat to face towards the Target overtime
				Vector3 lookAtVector = new Vector3(followTarget.position.x, transform.position.y, followTarget.position.z);
				rigidBody.transform.LookAt(lookAtVector);

				rigidBody.AddRelativeForce(Vector3.forward * movementForce * Time.fixedDeltaTime, ForceMode.Force);

			}

			//Make this bool
			if (hit)
			{
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
						manager.Animation.PlayAnimation(CatAnimation.Jump);
						manager.Sound.PlaySingle("BallHit", 0.85f, 1.15f);
						rigidBody.velocity = rigidBody.velocity.normalized * 0;
						hitTimer = hitCooldown;
					}
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
	public float MovementForce { get => movementForce; }
	public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
}
