//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatAnimation
{
	Eat,
	Idle,
	Jump,
	Sound,
	Walk
}

public class CatAnimationHandler : MonoBehaviour
{
	private Animator animator = null;
	private Dictionary<CatAnimation, string> animations = null;

	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		animations = new Dictionary<CatAnimation, string>();
		animations.Add(CatAnimation.Idle, "Idle");
		animations.Add(CatAnimation.Eat, "Eat");
		animations.Add(CatAnimation.Jump, "Jump");
		animations.Add(CatAnimation.Sound, "Sound");
		animations.Add(CatAnimation.Walk, "Walk");
		PlayAnimation(CatAnimation.Idle);
	}

	/// <summary>
	/// Plays the specific animation, by default disables all other animation values
	/// </summary>
	/// <param name="animation"></param>
	/// <param name="setAllFalse"></param>
	/// <returns></returns>
	public bool PlayAnimation(CatAnimation animation, bool setAllFalse = true, bool resetSpeed = true)
	{
		if (resetSpeed)
			animator.speed = 1;

		if (setAllFalse)
		{
			foreach (string animationName in animations.Values)
			{
				animator.SetBool(animationName, false);
			}
		}

		if (animations.ContainsKey(animation))
		{
			//animator.Play(animations[animation]);
			animator.SetBool(animations[animation], true);

			return true;
		}

		return false;
	}

	public bool PlayAnimation(CatAnimation animation, float animationSpeed, bool setAllFalse = true)
	{
		animator.speed = animationSpeed;
		return PlayAnimation(animation, true, false);
	}


}
