//Written by Jayden Hunter
using UnityEngine;

/// <summary>
/// Handles all the movement controls for the cat
/// </summary>
public class CatMovementHandler : MonoBehaviour
{
	public bool mainScene = false; //Check if we are in the main scene or not
	private Vector3 goToPosition = Vector2.zero; //Target position to go to
	[SerializeField] private Transform followTarget = null; //Target to follow
	private float wanderRadius = 10f;
	private float followSpeed = 10f;
	private float wanderSpeed = 10f;
	private float goToSpeed = 10f;

	[SerializeField] private float hitDistance = 1.5f; //Minimum distance from ball to be able to hit it
	[SerializeField] private float hitForce = 1000f; //Amount of force applied to the ball on hit
	[SerializeField] private float hitCooldown = 2; //Cooldown until the cat can hit the ball again
	private float hitTimer = 0; //Timer used to track the cooldown of the ball hit
	private CatManager manager; //Reference to the manager of the cat

	//Movement Fields
	[SerializeField] private float movementForce = 10f; //Amount of forced applied to cat's movement
	[SerializeField] private float maxSpeed = 2f; //Maximum speed the cat can move at
	[SerializeField] private float stoppingDistance = 0.25f; //Distance from target location until cat stops moving
	[SerializeField] private float animationSpeedMultiplier = 0.25f; //Multiplier on the animation speed for moving
	[SerializeField] private float minVelocityToWalk = 0.25f; //Minimum required velocity until the cat starts moving

	//Rotational Fields
	[SerializeField] private float rotationVelocity = 35f; //How fast the cat rotates to face it's target direction

	private Rigidbody rigidBody = null;

	private void Awake()
	{
		hitTimer = hitCooldown;

		//Get required components
		manager = GetComponent<CatManager>();
		rigidBody = GetComponent<Rigidbody>();

		//If we are in the main scene...
		if (mainScene)
		{
			//...set cat's follow targe to the play ball
			followTarget = GameObject.Find("PlayBall").transform;
		}
	}

	/// <summary>
	/// Moves the cat towards it's target while rotation towards it
	/// </summary>
	/// <param name="hit"></param>
	public void MoveTowardsTargetWithRotation(bool hit = false)
	{

		hitTimer -= Time.deltaTime;

		//If we have a follow target...
		if (followTarget)
		{
			float distToTarget = Vector3.Distance(transform.position, followTarget.position);
			//...And our distance is within stopping distance...
			if (distToTarget > stoppingDistance)
			{
				float currentVelocity = rigidBody.velocity.magnitude;
				//...Check which animation we should be playing based on velocity
				if (currentVelocity > minVelocityToWalk)
					manager.Animation.PlayAnimation(CatAnimation.Walk, currentVelocity * animationSpeedMultiplier);
				else
					manager.Animation.PlayAnimation(CatAnimation.Idle);


				if (currentVelocity > maxSpeed)
					rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;

				//Rotate the Cat to face towards the Target overtime
				Vector3 desiredDir = followTarget.transform.position - transform.position;
				desiredDir.y = transform.position.y;
				desiredDir.Normalize();

				Quaternion lookAtQuat = Quaternion.LookRotation(desiredDir, Vector3.up);
				Quaternion rotation = Quaternion.RotateTowards(transform.rotation, lookAtQuat, rotationVelocity);
				transform.rotation = rotation;

				//Add force to the rigid body to move the cat forward
				rigidBody.AddRelativeForce(Vector3.forward * movementForce * Time.fixedDeltaTime, ForceMode.Force);

			}

			//If we have hit the ball, we are in range and we have a target...
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

						//...Add a force to the ball to send it flying away
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


	//Properties
	public Vector3 GoToPosition { get => goToPosition; set => goToPosition = value; }
	public Transform FollowTarget { get => followTarget; set => followTarget = value; }
	public float WanderRadius { get => wanderRadius; }
	public float FollowSpeed { get => followSpeed; }
	public float WanderSpeed { get => wanderSpeed; }
	public float GoToSpeed { get => goToSpeed; }
	public float MovementForce { get => movementForce; }
	public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
}
