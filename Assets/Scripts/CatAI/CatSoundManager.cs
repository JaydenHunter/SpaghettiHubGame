using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSoundManager : MonoBehaviour
{
	private AudioSource loopingSound = null;
	private AudioSource singlePlaySound = null;
	[SerializeField] private List<AudioClipData> audioClips = null;

	private float singleDefaultPitch = 1;

	private void Awake()
	{
		loopingSound = GameObject.Find("CatLooping").GetComponent<AudioSource>();
		singlePlaySound = GameObject.Find("CatSingleSound").GetComponent<AudioSource>();

		singleDefaultPitch = singlePlaySound.pitch;
	}

	public void PlayLooping(string clipName)
	{
		if (FindClipByName(clipName, out AudioClip clip))
		{
			loopingSound.clip = clip;
			loopingSound.Play();
		}
	}

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

	public void PlaySingle(string clipName, float minPitch, float maxPitch)
	{
		singlePlaySound.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
		PlaySingle(clipName, false);
	}

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
