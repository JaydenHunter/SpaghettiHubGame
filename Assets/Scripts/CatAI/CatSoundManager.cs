//Written by Jayden Hunter
using System;
using System.Collections.Generic;
using UnityEngine;

public class CatSoundManager : MonoBehaviour
{
	private AudioSource loopingSound = null;
	private AudioSource singlePlaySound = null;
	[SerializeField] private List<AudioClipData> audioClips = null;
	public bool mainScene = false;

	private float singleDefaultPitch = 1;

	private void Awake()
	{
		if (mainScene)
		{
			loopingSound = GameObject.Find("CatLooping").GetComponent<AudioSource>();
			singlePlaySound = GameObject.Find("CatSingleSound").GetComponent<AudioSource>();

			singleDefaultPitch = singlePlaySound.pitch;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="clipName"></param>
	public void PlayLooping(string clipName)
	{
		if (FindClipByName(clipName, out AudioClip clip))
		{
			loopingSound.clip = clip;
			loopingSound.Play();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="clipName"></param>
	/// <param name="resetPitch"></param>
	public void PlaySingle(string clipName, bool resetPitch = true)
	{
		if (resetPitch)
			singlePlaySound.pitch = singleDefaultPitch;

		if (FindClipByName(clipName, out AudioClip clip))
		{
			singlePlaySound.clip = clip;
			singlePlaySound.Play();
		}
	}

	/// <summary>
	/// Used to play a sound once
	/// </summary>
	/// <param name="clipName"></param>
	/// <param name="minPitch"></param>
	/// <param name="maxPitch"></param>
	public void PlaySingle(string clipName, float minPitch, float maxPitch)
	{
		singlePlaySound.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
		PlaySingle(clipName, false);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="clipName"></param>
	/// <param name="clip"></param>
	/// <returns></returns>
	private bool FindClipByName(string clipName, out AudioClip clip)
	{
		clip = null;
		if (audioClips != null)
		{
			for (int i = 0; i < audioClips.Count; i++)
			{
				if (audioClips[i].clipName == clipName)
				{
					clip = audioClips[i].audioClip;
					return true;
				}
			}
		}

		return false;
	}

	[Serializable]
	public struct AudioClipData
	{
		public string clipName;
		public AudioClip audioClip;
	}
}
