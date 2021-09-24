//Written by Jayden Hunter
using UnityEngine;

/// <summary>
/// Generic State for the Cat
/// </summary>
public class CatState : MonoBehaviour
{
	protected CatStateMachine stateMachine = null;
	protected CatManager manager;

	private void Awake()
	{
		try
		{
			stateMachine = GetComponent<CatStateMachine>();
			manager = GetComponent<CatManager>();
		}
		catch
		{
			Debug.LogError("Couldn't Get State Machine or Manager");
		}
	}
	//Virtual Functions

	/// <summary>
	/// Called On Entering a state
	/// </summary>
	public virtual void OnEnter()
	{

	}

	/// <summary>
	/// Called On Each Frame that the state is active
	/// </summary>
	public virtual void OnUpdate()
	{

	}

	/// <summary>
	/// Called on each Fixed Update loop if the state is active
	/// </summary>
	public virtual void OnFixedUpdate()
	{

	}

	/// <summary>
	/// Called when the state exits
	/// </summary>
	public virtual void OnExit()
	{

	}
}
