//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatState : MonoBehaviour
{
	protected CatStateMachine stateMachine = null;

	private void Awake()
	{
		stateMachine = GetComponent<CatStateMachine>();
	}

	public virtual void OnEnter()
	{

	}

	public virtual void OnUpdate()
	{

	}
	
	public virtual void OnFixedUpdate()
	{

	}

	public virtual void OnExit()
	{

	}
}
